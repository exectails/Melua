// Copyright (c) Aura development team - Licensed under GNU GPL
// For more information, see license file in the main folder

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace MeluaLib
{
	public static partial class Melua
	{
		private const string Lib = "lua51";

		private static Dictionary<IntPtr, List<LuaNativeFunction>> _functions = new Dictionary<IntPtr, List<LuaNativeFunction>>();

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int LuaNativeFunction(IntPtr L);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int LuaNativeNFunction(IntPtr L, int index);

		// typedef void (*lua_Hook) (lua_State *L, lua_Debug *ar);
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void lua_Hook(IntPtr L, IntPtr ar);

		public struct LuaLib
		{
			public readonly string Name;
			public readonly LuaNativeFunction Func;

			public LuaLib(string name, LuaNativeFunction func)
			{
				this.Name = name;
				this.Func = func;
			}
		}

		/// <summary>
		/// Creates and saves reference to function, so it's not garbage
		/// collected.
		/// </summary>
		/// <param name="function"></param>
		public static LuaNativeFunction CreateFunctionReference(IntPtr L, LuaNativeFunction function)
		{
			List<LuaNativeFunction> list;
			if (!_functions.TryGetValue(L, out list) || list == null)
				list = _functions[L] = new List<LuaNativeFunction>();

			var func = new LuaNativeFunction(function);
			list.Add(func);

			return func;
		}

		/// <summary>
		/// Calls the specified standard library open functions.
		/// </summary>
		/// <param name="L"></param>
		/// <param name="libsToLoad"></param>
		public static void melua_openlib(IntPtr L, params LuaLib[] libsToLoad)
		{
			foreach (var lib in libsToLoad)
			{
				lua_pushcfunction(L, lib.Func);
				lua_pushstring(L, lib.Name);
				lua_call(L, 1, 0);

				GC.KeepAlive(lib.Func);
			}
		}

		/// <summary>
		/// Calls all standard lib open functions.
		/// </summary>
		/// <param name="L"></param>
		public static void melua_openlibs(IntPtr L)
		{
			melua_openlib(L, new[]
			{
				new LuaLib("", luaopen_base),
				new LuaLib("package", luaopen_package),
				new LuaLib("table", luaopen_table),
				new LuaLib("io", luaopen_io),
				new LuaLib("os", luaopen_os),
				new LuaLib("string", luaopen_string),
				new LuaLib("math", luaopen_math),
				new LuaLib("debug", luaopen_debug),
			});
		}

		/// <summary>
		/// C# version of luaL_error.
		/// </summary>
		/// <remarks>
		/// Original: LUALIB_API int luaL_error (lua_State *L, const char *fmt, ...)
		/// </remarks>
		/// <param name="L"></param>
		/// <param name="format"></param>
		/// <param name="args"></param>
		public static int melua_error(IntPtr L, string format, params object[] args)
		{
			luaL_where(L, 1);
			lua_pushstring(L, string.Format(format, args));
			lua_concat(L, 2);
			return lua_error(L);
		}

		/// <summary>
		/// Adds a safe subset of functions from the base library
		/// to the state.
		/// </summary>
		/// <param name="L"></param>
		/// <returns></returns>
		internal static int meluaopen_basesafe(IntPtr L)
		{
			auxopen(L, "ipairs", CreateFunctionReference(L, Melua.luaB_ipairs), CreateFunctionReference(L, Melua.ipairsaux));
			auxopen(L, "pairs", CreateFunctionReference(L, Melua.luaB_pairs), CreateFunctionReference(L, Melua.luaB_next));
			melua_register(L, "pcall", luaB_pcall);
			melua_register(L, "print", luaB_print);
			melua_register(L, "tonumber", luaB_tonumber);
			melua_register(L, "tostring", luaB_tostring);
			melua_register(L, "type", luaB_type);
			melua_register(L, "unpack", luaB_unpack);
			melua_register(L, "xpcall", luaB_xpcall);

			return 0;
		}

		/// <summary>
		/// Registers the given function under the given name and safes a
		/// reference to the function.
		/// </summary>
		/// <param name="L"></param>
		/// <param name="functionName"></param>
		/// <param name="function"></param>
		public static void melua_register(IntPtr L, string functionName, LuaNativeFunction function)
		{
			lua_register(L, functionName, Melua.CreateFunctionReference(L, function));
		}

		/// <summary>
		/// Returns the formatted string after pushing it onto the stack.
		/// </summary>
		/// <param name="L"></param>
		/// <param name="format"></param>
		/// <param name="args"></param>
		/// <returns></returns>
		public static string melua_pushstring(IntPtr L, string format, params object[] args)
		{
			var str = string.Format(format, args);
			lua_pushstring(L, str);

			return str;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#pragma warning disable IDE1006 // Naming rules

namespace MeluaLib
{
	/// <summary>
	/// Wrapper around all Lua functions.
	/// </summary>
	public static partial class Melua
	{
		private const string Lib = "lua51";

		private static readonly Dictionary<IntPtr, List<LuaNativeFunction>> _functions = new Dictionary<IntPtr, List<LuaNativeFunction>>();

		/// <summary>
		/// Delegate for a function that can be registered with Lua.
		/// </summary>
		/// <param name="L"></param>
		/// <returns></returns>
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int LuaNativeFunction(IntPtr L);

		/// <summary>
		/// Delegate for a function that can be registered with Lua.
		/// </summary>
		/// <param name="L"></param>
		/// <returns></returns>
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int LuaNativeNFunction(IntPtr L, int index);

		/// <summary>
		/// Represents a Lua library, with a name and a function that is
		/// used to set up the library.
		/// </summary>
		public struct LuaLib
		{
			/// <summary>
			/// Returns the name of library.
			/// </summary>
			public readonly string Name;

			/// <summary>
			/// Returns the library setup function.
			/// </summary>
			public readonly LuaNativeFunction Func;

			/// <summary>
			/// Creates new instance.
			/// </summary>
			/// <param name="name"></param>
			/// <param name="func"></param>
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
			if (!_functions.TryGetValue(L, out List<LuaNativeFunction> list) || list == null)
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
		/// Opens all standard libraries and overrides functions that
		/// don't work properly under .NET by default (like print).
		/// </summary>
		/// <param name="L"></param>
		public static void melua_openlibs(IntPtr L)
		{
			melua_openlib(L, new[]
			{
				new LuaLib("", meluaopen_base),
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
		/// Calls luaopen_base and registers a custom print function
		/// that works by default under .NET.
		/// </summary>
		/// <param name="L"></param>
		/// <returns></returns>
		public static int meluaopen_base(IntPtr L)
		{
			luaopen_base(L);

			// Register custom print explicitly, since the native version
			// doesn't work by default under .NET.
			melua_register(L, "print", luaB_print);

			return 2;
		}

		/// <summary>
		/// Adds a subset of functions from the base library to the state
		/// that are safe to be included, because they don't allow the
		/// modification of the file system or any global states.
		/// </summary>
		/// <param name="L"></param>
		public static void melua_opensafelibs(IntPtr L)
		{
			melua_openlib(L, new LuaLib("", meluaopen_basesafe));
		}

		/// <summary>
		/// Adds a safe subset of functions from the base library
		/// to the state.
		/// </summary>
		/// <param name="L"></param>
		/// <returns></returns>
		public static int meluaopen_basesafe(IntPtr L)
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
		/// C# version of luaL_error.
		/// </summary>
		/// <param name="L"></param>
		/// <param name="format"></param>
		/// <param name="args"></param>
		public static int melua_error(IntPtr L, string format, params object[] args)
		{
			if (args.Length > 0)
				format = string.Format(format, args);

			luaL_where(L, 1);
			lua_pushstring(L, format);
			lua_concat(L, 2);
			return lua_error(L);
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
			if (args.Length > 0)
				format = string.Format(format, args);

			lua_pushstring(L, format);

			return format;
		}
	}
}

#pragma warning restore IDE1006 // Naming rules

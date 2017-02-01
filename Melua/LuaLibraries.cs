// Copyright (c) Aura development team - Licensed under GNU GPL
// For more information, see license file in the main folder

using System;
using System.Runtime.InteropServices;

namespace MeluaLib
{
	public static partial class Melua
	{
		// static void luaL_openlibs(lua_State*L)
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern void luaL_openlibs(IntPtr L);

		// LUALIB_API int luaopen_base (lua_State *L)
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int luaopen_base(IntPtr L);

		// LUALIB_API int luaopen_package (lua_State *L)
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int luaopen_package(IntPtr L);

		// LUALIB_API int luaopen_table (lua_State *L)
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int luaopen_table(IntPtr L);

		// LUALIB_API int luaopen_io (lua_State *L)
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int luaopen_io(IntPtr L);

		// LUALIB_API int luaopen_os (lua_State *L)
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int luaopen_os(IntPtr L);

		// LUALIB_API int luaopen_string (lua_State *L)
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int luaopen_string(IntPtr L);

		// LUALIB_API int luaopen_math (lua_State *L)
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int luaopen_math(IntPtr L);

		// LUALIB_API int luaopen_debug (lua_State *L)
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int luaopen_debug(IntPtr L);
	}
}

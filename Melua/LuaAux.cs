// Copyright (c) Aura development team - Licensed under GNU GPL
// For more information, see license file in the main folder

using System;
using System.Runtime.InteropServices;

namespace MeluaLib
{
	// Auxiliary Lua API
	public static partial class Melua
	{
		// #define luaL_getn(L,i)          ((int)lua_objlen(L, i))
		public static int luaL_getn(IntPtr L, int i)
		{
			return lua_objlen(L, i);
		}

		// #define luaL_setn(L,i,j)        ((void)0)  /* no op! */
		public static void luaL_setn(IntPtr L, int i, int j)
		{
		}

		// luaL_openlib (use melua_openlib)

		// luaL_register (use melua_openlib)

		// luaL_getmetafield

		// LUALIB_API int luaL_callmeta (lua_State *L, int obj, const char *event)
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern bool luaL_callmeta(IntPtr L, int obj, [MarshalAs(UnmanagedType.LPStr)] string ev);

		// int (luaL_typerror) (lua_State *L, int narg, const char *tname);
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int luaL_typerror(IntPtr L, int narg, [MarshalAs(UnmanagedType.LPStr)] string tname);

		// int (luaL_argerror) (lua_State *L, int numarg, const char *extramsg);
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int luaL_argerror(IntPtr L, int numarg, [MarshalAs(UnmanagedType.LPStr)] string extramsg);

		// static const char* luaL_checklstring(lua_State*L,int numArg,size_t*l)
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern IntPtr luaL_checklstring(IntPtr L, int numArg, IntPtr l);

		// const char *(luaL_optlstring) (lua_State *L, int numArg, const char *def, size_t *l);
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern IntPtr luaL_optlstring(IntPtr L, int numArg, [MarshalAs(UnmanagedType.LPStr)] string def, IntPtr l);

		// static lua_Number luaL_checknumber(lua_State*L,int narg)
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern double luaL_checknumber(IntPtr L, int narg);

		// lua_Number (luaL_optnumber) (lua_State *L, int nArg, lua_Number def);
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern double luaL_optnumber(IntPtr L, int nArg, double def);

		// static lua_Integer luaL_checkinteger(lua_State*L,int numArg)
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int luaL_checkinteger(IntPtr L, int numArg);

		// LUALIB_API lua_Integer (luaL_optinteger) (lua_State *L, int nArg, lua_Integer def);
		public static int luaL_optinteger(IntPtr L, int narg, int def)
		{
			return luaL_opt(L, luaL_checkinteger, narg, def);
		}

		// void (luaL_checkstack) (lua_State *L, int sz, const char *msg);
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern void luaL_checkstack(IntPtr L, int sz, [MarshalAs(UnmanagedType.LPStr)] string msg);

		// LUALIB_API void luaL_checktype (lua_State *L, int narg, int t)
		public static void luaL_checktype(IntPtr L, int narg, int t)
		{
			if (lua_type(L, narg) != t)
				melua_error(L, string.Format("{0} expected, got {1}"), lua_typename(L, t), luaL_typename(L, narg));
		}

		// LUALIB_API void luaL_checkany (lua_State *L, int narg)
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern void luaL_checkany(IntPtr L, int narg);

		// int luaL_newmetatable (lua_State *L, const char *tname);
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int luaL_newmetatable(IntPtr L, [MarshalAs(UnmanagedType.LPStr)] string tname);

		// void *luaL_checkudata (lua_State *L, int narg, const char *tname);
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern void luaL_checkudata(IntPtr L, int narg, [MarshalAs(UnmanagedType.LPStr)] string tname);

		// LUALIB_API void luaL_where (lua_State *L, int level)
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern void luaL_where(IntPtr L, int level);

		// luaL_error (use melua_error)

		// luaL_checkoption

		// int (luaL_ref) (lua_State *L, int t);
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int luaL_ref(IntPtr L, int t);

		// void (luaL_unref) (lua_State *L, int t, int ref);
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern void luaL_unref(IntPtr L, int t, int reference);

		// static int luaL_loadfile(lua_State*L,const char*filename)
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int luaL_loadfile(IntPtr L, [MarshalAs(UnmanagedType.LPStr)] string filename);

		// static int luaL_loadbuffer(lua_State*L,const char*buff,size_t size,const char*name)
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int luaL_loadbuffer(IntPtr L, [MarshalAs(UnmanagedType.LPStr)] string buff, int size, [MarshalAs(UnmanagedType.LPStr)] string name);

		// LUALIB_API int (luaL_loadstring) (lua_State *L, const char *s)
		public static int luaL_loadstring(IntPtr L, string s)
		{
			return luaL_loadbuffer(L, s, s.Length, s);
		}

		// LUALIB_API lua_State *luaL_newstate(void)
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern IntPtr luaL_newstate();

		// const char *(luaL_gsub) (lua_State *L, const char *s, const char *p, const char *r);
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int luaL_gsub(IntPtr L, [MarshalAs(UnmanagedType.LPStr)] string s, [MarshalAs(UnmanagedType.LPStr)] string p, [MarshalAs(UnmanagedType.LPStr)] string r);

		// luaL_findtable

		// Some useful "macros"
		// ------------------------------------------------------------------

		// void luaL_argcheck (lua_State *L, int cond, int narg, const char *extramsg);
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern void luaL_argcheck(IntPtr L, bool cond, int narg, [MarshalAs(UnmanagedType.LPStr)] string extramsg);

		// #define luaL_checkstring(L,n)(luaL_checklstring(L,(n),NULL))
		public static string luaL_checkstring(IntPtr L, int n)
		{
			var ptr = luaL_checklstring(L, n, IntPtr.Zero);
			var val = Marshal.PtrToStringAnsi(ptr);
			return val;
		}

		// #define luaL_optstring(L,n,d)   (luaL_optlstring(L, (n), (d), NULL))
		public static string luaL_optstring(IntPtr L, int n, string d)
		{
			var ptr = luaL_optlstring(L, n, d, IntPtr.Zero);
			var val = Marshal.PtrToStringAnsi(ptr);
			return val;
		}

		// #define luaL_checkint(L,n)      ((int)luaL_checkinteger(L, (n)))
		public static int luaL_checkint(IntPtr L, int n)
		{
			return luaL_checkinteger(L, n);
		}

		// #define luaL_optint(L,n,d)      ((int)luaL_optinteger(L, (n), (d)))
		public static int luaL_optint(IntPtr L, int n, int d)
		{
			return luaL_optinteger(L, n, d);
		}

		// #define luaL_checklong(L,n)      ((int)luaL_checkinteger(L, (n)))
		public static long luaL_checklong(IntPtr L, int n)
		{
			return luaL_checkinteger(L, n);
		}

		// #define luaL_optlong(L,n,d)      ((int)luaL_optinteger(L, (n), (d)))
		public static long luaL_optlong(IntPtr L, int n, long d)
		{
			return luaL_optinteger(L, n, (int)d);
		}

		// #define luaL_typename(L,i)	lua_typename(L, lua_type(L,(i)))
		public static string luaL_typename(IntPtr L, int i)
		{
			var ptr = lua_typename(L, lua_type(L, i));
			var val = Marshal.PtrToStringAnsi(ptr);
			return val;
		}

		// #define luaL_dofile(L, fn) (luaL_loadfile(L, fn) || lua_pcall(L, 0, LUA_MULTRET, 0))
		public static int luaL_dofile(IntPtr L, string fn)
		{
			return (luaL_loadfile(L, fn) != 0 || lua_pcall(L, 0, LUA_MULTRET, 0) != 0) ? 1 : 0;
		}

		// #define luaL_dostring(L, s) (luaL_loadstring(L, s) || lua_pcall(L, 0, LUA_MULTRET, 0))
		public static int luaL_dostring(IntPtr L, string s)
		{
			return (luaL_loadstring(L, s) != 0 || lua_pcall(L, 0, LUA_MULTRET, 0) != 0) ? 1 : 0;
		}

		// void luaL_getmetatable (lua_State *L, const char *tname);
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern void luaL_getmetatable(IntPtr L, [MarshalAs(UnmanagedType.LPStr)] string tname);

		// #define luaL_opt(L,f,n,d)	(lua_isnoneornil(L,(n)) ? (d) : f(L,(n)))
		public static int luaL_opt(IntPtr L, LuaNativeNFunction f, int n, int d)
		{
			return (lua_isnoneornil(L, n) ? d : f(L, n));
		}

		// Generic Buffer manipulation
		// ------------------------------------------------------------------

		// struct lua_Buffer

		// luaL_addchar

		// luaL_addsize

		// luaL_buffinit

		// luaL_prepbuffer

		// luaL_addlstring

		// luaL_addstring

		// luaL_addvalue

		// luaL_pushresult
	}
}

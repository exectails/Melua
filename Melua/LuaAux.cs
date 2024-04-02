﻿using System;
using System.Runtime.InteropServices;

#pragma warning disable IDE1006 // Naming rules

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

		// void luaI_openlib (lua_State *L, const char *libname, const luaL_Reg *l, int nup)
		public static void luaL_openlib(IntPtr L, string libname, LuaLib[] libs, int nup)
		{
			if (libname != null)
			{
				var size = libs.Length;

				luaL_findtable(L, LUA_REGISTRYINDEX, "_LOADED", 1);
				lua_getfield(L, -1, libname);
				if (!lua_istable(L, -1))
				{
					lua_pop(L, 1);

					if (luaL_findtable(L, LUA_GLOBALSINDEX, libname, size) != IntPtr.Zero)
						melua_error(L, "name conflict for module '{0}'", libname);
					lua_pushvalue(L, -1);
					lua_setfield(L, -3, libname);
				}
				lua_remove(L, -2);
				lua_insert(L, -(nup + 1));
			}
			foreach (var l in libs)
			{
				int i;
				for (i = 0; i < nup; i++)
					lua_pushvalue(L, -nup);
				lua_pushcclosure(L, l.Func, nup);
				lua_setfield(L, -(nup + 2), l.Name);
			}
			lua_pop(L, nup);
		}

		// void (luaL_register) (lua_State *L, const char *libname, const luaL_Reg *l)
		public static void luaL_register(IntPtr L, string libname, LuaLib[] libs)
		{
			luaL_openlib(L, libname, libs, 0);
		}

		// int (luaL_getmetafield) (lua_State *L, int obj, const char *e);
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern bool luaL_getmetafield(IntPtr L, int obj, [MarshalAs(UnmanagedType.LPStr)] string e);

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
				melua_error(L, "{0} expected, got {1}", lua_typename(L, t), luaL_typename(L, narg));
		}

		// LUALIB_API void luaL_checkany (lua_State *L, int narg)
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern void luaL_checkany(IntPtr L, int narg);

		// int luaL_newmetatable (lua_State *L, const char *tname);
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int luaL_newmetatable(IntPtr L, [MarshalAs(UnmanagedType.LPStr)] string tname);

		// void *luaL_checkudata (lua_State *L, int narg, const char *tname);
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern IntPtr luaL_checkudata(IntPtr L, int narg, [MarshalAs(UnmanagedType.LPStr)] string tname);

		// LUALIB_API void luaL_where (lua_State *L, int level)
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern void luaL_where(IntPtr L, int level);

		// luaL_error (use melua_error)

		// int (luaL_checkoption) (lua_State *L, int narg, const char *def, const char *const lst[]);
		public static int luaL_checkoption(IntPtr L, int narg, string def, string[] lst)
		{
			var name = (def != null) ? luaL_optstring(L, narg, def) : luaL_checkstring(L, narg);

			for (int i = 0; i < lst.Length; ++i)
			{
				if (lst[i] == name)
					return i;
			}

			return luaL_argerror(L, narg, melua_pushstring(L, "invalid option '{0}'", name));
		}

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

		// const char *luaL_findtable (lua_State *L, int idx, const char *fname, int szhint);
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern IntPtr luaL_findtable(IntPtr L, int idx, [MarshalAs(UnmanagedType.LPStr)] string fname, int szhint);

		// void luaL_traceback (lua_State *L, lua_State *L1, const char *msg, int level);
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern void luaL_traceback(IntPtr L, IntPtr L1, [MarshalAs(UnmanagedType.LPStr)] string msg, int level);

		// Some useful "macros"
		// ------------------------------------------------------------------

		// #define luaL_argcheck(L, cond,numarg,extramsg)  ((void)((cond) || luaL_argerror(L, (numarg), (extramsg))))
		public static void luaL_argcheck(IntPtr L, bool cond, int narg, string extramsg)
		{
			if (!cond)
				luaL_argerror(L, narg, extramsg);
		}

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

		// #define luaL_getmetatable(L,n)  (lua_getfield(L, LUA_REGISTRYINDEX, (n)))
		public static int luaL_getmetatable(IntPtr L, string n)
		{
			return lua_getfield(L, LUA_REGISTRYINDEX, n);
		}

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

#pragma warning restore IDE1006 // Naming rules

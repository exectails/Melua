﻿// Copyright (c) Aura development team - Licensed under GNU GPL
// For more information, see license file in the main folder

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MeluaLib
{
	public static class Melua
	{
		private const string Lib = "lua51";

		public const int LUA_MULTRET = -1;
		public const int LUA_REGISTRYINDEX = -10000;
		public const int LUA_ENVIRONINDEX = -10001;
		public const int LUA_GLOBALSINDEX = -10002;

		public const int LUA_YIELD = 1;

		public const int LUA_TNONE = -1;
		public const int LUA_TNIL = 0;
		public const int LUA_TBOOLEAN = 1;
		public const int LUA_TLIGHTUSERDATA = 2;
		public const int LUA_TNUMBER = 3;
		public const int LUA_TSTRING = 4;
		public const int LUA_TTABLE = 5;
		public const int LUA_TFUNCTION = 6;
		public const int LUA_TUSERDATA = 7;
		public const int LUA_TTHREAD = 8;

		// Event codes
		public const int LUA_HOOKCALL = 0;
		public const int LUA_HOOKRET = 1;
		public const int LUA_HOOKLINE = 2;
		public const int LUA_HOOKCOUNT = 3;
		public const int LUA_HOOKTAILRET = 4;

		// Event masks
		public const int LUA_MASKCALL = (1 << LUA_HOOKCALL);
		public const int LUA_MASKRET = (1 << LUA_HOOKRET);
		public const int LUA_MASKLINE = (1 << LUA_HOOKLINE);
		public const int LUA_MASKCOUNT = (1 << LUA_HOOKCOUNT);



		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int LuaNativeFunction(IntPtr L);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int LuaNativeNFunction(IntPtr L, int index);

		// typedef void (*lua_Hook) (lua_State *L, lua_Debug *ar);
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void lua_Hook(IntPtr L, IntPtr ar);

		// LUALIB_API lua_State *luaL_newstate(void)
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern IntPtr luaL_newstate();

		// static void luaL_openlibs(lua_State*L)
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern void luaL_openlibs(IntPtr L);

		// static int luaL_loadbuffer(lua_State*L,const char*buff,size_t size,const char*name)
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int luaL_loadbuffer(IntPtr L, [MarshalAs(UnmanagedType.LPStr)] string buff, int size, [MarshalAs(UnmanagedType.LPStr)] string name);

		// static int luaL_loadfile(lua_State*L,const char*filename)
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int luaL_loadfile(IntPtr L, [MarshalAs(UnmanagedType.LPStr)] string filename);

		// LUA_API void lua_call (lua_State *L, int nargs, int nresults)
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern void lua_call(IntPtr L, int nargs, int nresults);

		// static int lua_pcall(lua_State*L,int nargs,int nresults,int errfunc)
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int lua_pcall(IntPtr L, int nargs, int nresults, int errfunc);

		// static void lua_pushcclosure(lua_State*L,lua_CFunction fn,int n)
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int lua_pushcclosure(IntPtr L, LuaNativeFunction fn, int n);

		// static void lua_setfield(lua_State*L,int idx,const char*k);
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int lua_setfield(IntPtr L, int idx, [MarshalAs(UnmanagedType.LPStr)] string k);

		// static void lua_getfield(lua_State*L,int idx,const char*k)
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int lua_getfield(IntPtr L, int idx, [MarshalAs(UnmanagedType.LPStr)] string k);

		// static const char*lua_tolstring(lua_State*L,int idx,size_t*len)
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern IntPtr lua_tolstring(IntPtr L, int idx, IntPtr len);

		// int lua_toboolean (lua_State *L, int index);
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern bool lua_toboolean(IntPtr L, int index);

		// lua_Integer lua_tointeger (lua_State *L, int index);
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int lua_tointeger(IntPtr L, int index);

		// lua_Number lua_tonumber (lua_State *L, int index);
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern double lua_tonumber(IntPtr L, int index);

		// static void lua_settop(lua_State*L,int idx)
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern void lua_settop(IntPtr L, int idx);

		// LUA_API lua_State *lua_newthread (lua_State *L)
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern IntPtr lua_newthread(IntPtr L);

		// static void lua_pushstring(lua_State*L,const char*s)
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern void lua_pushstring(IntPtr L, [MarshalAs(UnmanagedType.LPStr)] string s);

		// static void lua_pushinteger(lua_State*L,lua_Integer n)
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern void lua_pushinteger(IntPtr L, int n);

		// static void lua_pushnumber(lua_State*L,lua_Number n)
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern void lua_pushnumber(IntPtr L, double n);

		// void lua_pushnil (lua_State *L);
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern void lua_pushnil(IntPtr L);

		// LUA_API void lua_pushboolean (lua_State *L, int b)
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern void lua_pushboolean(IntPtr L, bool b);

		// static void lua_settable(lua_State*L,int idx)
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern void lua_settable(IntPtr L, int idx);

		// static lua_Number luaL_checknumber(lua_State*L,int narg)
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern double luaL_checknumber(IntPtr L, int narg);

		// static int lua_type(lua_State*L,int idx)
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int lua_type(IntPtr L, int idx);

		// static const char* luaL_checklstring(lua_State*L,int numArg,size_t*l)
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern IntPtr luaL_checklstring(IntPtr L, int numArg, IntPtr l);

		// static int lua_gettop(lua_State*L)
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int lua_gettop(IntPtr L);

		// static lua_Integer luaL_checkinteger(lua_State*L,int numArg)
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int luaL_checkinteger(IntPtr L, int numArg);

		// LUA_API int  (lua_yield) (lua_State *L, int nresults);
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int lua_yield(IntPtr L, int nresults);

		// LUA_API int  (lua_resume) (lua_State *L, int narg);
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int lua_resume(IntPtr L, int narg);

		// LUA_API int  (lua_status) (lua_State *L);
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int lua_status(IntPtr L);

		// LUA_API void lua_pushvalue (lua_State *L, int idx)
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern void lua_pushvalue(IntPtr L, int idx);

		// LUA_API void lua_insert (lua_State *L, int idx)
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern void lua_insert(IntPtr L, int idx);

		// LUA_API void lua_createtable (lua_State *L, int narray, int nrec)
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern void lua_createtable(IntPtr L, int narray, int nrec);

		// int lua_error (lua_State *L)
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int lua_error(IntPtr L);

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

		// LUA_API int lua_sethook (lua_State *L, lua_Hook func, int mask, int count)
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int lua_sethook(IntPtr L, lua_Hook func, int mask, int count);

		// LUALIB_API void luaL_where (lua_State *L, int level)
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern void luaL_where(IntPtr L, int level);

		// LUA_API void lua_concat (lua_State *L, int n)
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern void lua_concat(IntPtr L, int n);

		// lua_CFunction lua_atpanic (lua_State *L, lua_CFunction panicf);
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern void lua_atpanic(IntPtr L, LuaNativeFunction panicf);

		// LUALIB_API void luaL_checkany (lua_State *L, int narg)
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern void luaL_checkany(IntPtr L, int narg);

		// LUALIB_API int luaL_callmeta (lua_State *L, int obj, const char *event)
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern bool luaL_callmeta(IntPtr L, int obj, [MarshalAs(UnmanagedType.LPStr)] string ev);

		// void lua_remove (lua_State *L, int index);
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern void lua_remove(IntPtr L, int index);

		// void luaL_argcheck (lua_State *L, int cond, int narg, const char *extramsg);
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern void luaL_argcheck(IntPtr L, bool cond, int narg, [MarshalAs(UnmanagedType.LPStr)] string extramsg);

		// LUA_API void lua_pushlstring (lua_State *L, const char *s, size_t len)
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern void lua_pushlstring(IntPtr L, [MarshalAs(UnmanagedType.LPStr)] string s, int len);

		// LUA_API const char *lua_typename (lua_State *L, int t)
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		[return: MarshalAs(UnmanagedType.LPStr)]
		public static extern string lua_typename(IntPtr L, int t);

		// LUA_API int lua_checkstack (lua_State *L, int size)
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern bool lua_checkstack(IntPtr L, int size);

		// LUA_API void lua_rawget (lua_State *L, int idx)
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern void lua_rawget(IntPtr L, int idx);

		// LUA_API void lua_rawgeti (lua_State *L, int idx, int n)
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern void lua_rawgeti(IntPtr L, int idx, int n);

		// LUA_API size_t lua_objlen (lua_State *L, int idx)
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int lua_objlen(IntPtr L, int idx);

		// LUA_API int lua_setmetatable (lua_State *L, int objindex)
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int lua_setmetatable(IntPtr L, int objindex);

		// LUA_API int lua_next (lua_State *L, int idx)
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int lua_next(IntPtr L, int idx);

		// LUA_API void lua_replace (lua_State *L, int idx)
		[DllImport(Lib, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern void lua_replace(IntPtr L, int idx);




		// #define luaL_dostring(L, s) (luaL_loadstring(L, s) || lua_pcall(L, 0, LUA_MULTRET, 0))
		public static int luaL_dostring(IntPtr L, string s)
		{
			return (luaL_loadstring(L, s) != 0 || lua_pcall(L, 0, LUA_MULTRET, 0) != 0) ? 1 : 0;
		}

		// #define luaL_dofile(L, fn) (luaL_loadfile(L, fn) || lua_pcall(L, 0, LUA_MULTRET, 0))
		public static int luaL_dofile(IntPtr L, string fn)
		{
			return (luaL_loadfile(L, fn) != 0 || lua_pcall(L, 0, LUA_MULTRET, 0) != 0) ? 1 : 0;
		}

		// #define lua_register(L,n,f) (lua_pushcfunction(L, (f)), lua_setglobal(L, (n)))
		public static void lua_register(IntPtr L, string n, LuaNativeFunction f)
		{
			lua_pushcfunction(L, f);
			lua_setglobal(L, n);
		}

		// static void auxopen (lua_State *L, const char *name, lua_CFunction f, lua_CFunction u)
		internal static void auxopen(IntPtr L, string name, LuaNativeFunction f, LuaNativeFunction u)
		{
			lua_pushcfunction(L, u);
			lua_pushcclosure(L, f, 1);
			lua_setglobal(L, name);
		}

		// static int ipairsaux(lua_State* L)
		internal static int ipairsaux(IntPtr L)
		{
			luaL_checktype(L, 1, LUA_TTABLE);

			var i = luaL_checkinteger(L, 2);
			i++;

			lua_pushinteger(L, i);
			lua_rawgeti(L, 1, i);

			return (lua_isnil(L, -1) ? 0 : 2);
		}

		// static int luaB_next(lua_State* L)
		internal static int luaB_next(IntPtr L)
		{
			luaL_checktype(L, 1, LUA_TTABLE);
			lua_settop(L, 2);

			if (lua_next(L, 1) != 0)
				return 2;

			lua_pushnil(L);
			return 1;
		}

		// #define lua_pushcfunction(L,f)	lua_pushcclosure(L, (f), 0)
		public static void lua_pushcfunction(IntPtr L, LuaNativeFunction f)
		{
			lua_pushcclosure(L, f, 0);
		}

		// #define lua_getglobal(L,s)	lua_getfield(L, LUA_GLOBALSINDEX, (s))
		public static void lua_getglobal(IntPtr L, string s)
		{
			lua_getfield(L, LUA_GLOBALSINDEX, s);
		}

		// #define lua_setglobal(L,s)lua_setfield(L,(-10002),(s))
		public static void lua_setglobal(IntPtr L, string s)
		{
			lua_setfield(L, -10002, s);
		}

		// #define lua_tostring(L,i)lua_tolstring(L,(i),NULL)
		public static string lua_tostring(IntPtr L, int i)
		{
			var ptr = lua_tolstring(L, i, IntPtr.Zero);
			var val = Marshal.PtrToStringAnsi(ptr);
			return val;
		}

		// #define lua_pop(L,n)lua_settop(L,-(n)-1)
		public static void lua_pop(IntPtr L, int n)
		{
			lua_settop(L, -n - 1);
		}

		// #define lua_isnil(L,n)(lua_type(L,(n))==0)
		public static bool lua_isnil(IntPtr L, int n)
		{
			return (lua_type(L, n) == LUA_TNIL);
		}

		// int lua_isstring (lua_State *L, int index);
		public static bool lua_isstring(IntPtr L, int index)
		{
			var type = lua_type(L, index);
			return (type == LUA_TSTRING || type == LUA_TNUMBER);
		}

		// int lua_isnumber (lua_State *L, int index);
		public static bool lua_isnumber(IntPtr L, int index)
		{
			var type = lua_type(L, index);

			switch (type)
			{
				default: return false;
				case LUA_TNUMBER: return true;
				case LUA_TSTRING:
					var s = lua_tostring(L, index);
					double d;
					return double.TryParse(s, out d);
			}
		}

		// #define lua_isboolean(L,n)	(lua_type(L, (n)) == LUA_TBOOLEAN)
		public static bool lua_isboolean(IntPtr L, int n)
		{
			return (lua_type(L, n) == LUA_TBOOLEAN);
		}

		// #define lua_istable(L,n)	(lua_type(L, (n)) == LUA_TTABLE)
		public static bool lua_istable(IntPtr L, int n)
		{
			return (lua_type(L, n) == LUA_TTABLE);
		}

		// #define lua_isfunction(L,n)	(lua_type(L, (n)) == LUA_TFUNCTION)
		public static bool lua_isfunction(IntPtr L, int n)
		{
			return (lua_type(L, n) == LUA_TFUNCTION);
		}

		// #define luaL_checkstring(L,n)(luaL_checklstring(L,(n),NULL))
		public static string luaL_checkstring(IntPtr L, int n)
		{
			var ptr = luaL_checklstring(L, n, IntPtr.Zero);

			// Output 200 bytes of memory, briefly used to debug a problem where
			// Lua apparently wrote other information into the string's memory...
			// bug went away after getting rid of LUAI_EXTRASPACE.
			//unsafe
			//{
			//	var p = (byte*)ptr.ToPointer();
			//	var buf = new byte[200];
			//	for (int i = 0; i < buf.Length; ++i)
			//	{
			//		buf[i] = *(p + i);
			//	}
			//	Console.WriteLine(BitConverter.ToString(buf));
			//}

			var val = Marshal.PtrToStringAnsi(ptr);
			return val;
		}

		// #define lua_newtable(L)		lua_createtable(L, 0, 0)
		public static void lua_newtable(IntPtr L)
		{
			lua_createtable(L, 0, 0);
		}

		// #define lua_isnoneornil(L, n)	(lua_type(L, (n)) <= 0)
		public static bool lua_isnoneornil(IntPtr L, int n)
		{
			return (lua_type(L, n) <= 0);
		}

		// #define luaL_opt(L,f,n,d)	(lua_isnoneornil(L,(n)) ? (d) : f(L,(n)))
		public static int luaL_opt(IntPtr L, LuaNativeNFunction f, int n, int d)
		{
			return (lua_isnoneornil(L, n) ? d : f(L, n));
		}

		// #define lua_pushliteral(L, s)  lua_pushlstring(L, "" s, (sizeof(s)/sizeof(char))-1)
		public static void lua_pushliteral(IntPtr L, string s)
		{
			lua_pushlstring(L, s, s.Length);
		}

		// #define luaL_typename(L,i)	lua_typename(L, lua_type(L,(i)))
		public static string luaL_typename(IntPtr L, int i)
		{
			return lua_typename(L, lua_type(L, i));
		}

		// LUALIB_API lua_Integer (luaL_optinteger) (lua_State *L, int nArg, lua_Integer def);
		public static int luaL_optinteger(IntPtr L, int narg, int def)
		{
			return luaL_opt(L, luaL_checkinteger, narg, def);
		}

		// LUALIB_API int (luaL_loadstring) (lua_State *L, const char *s)
		public static int luaL_loadstring(IntPtr L, string s)
		{
			return luaL_loadbuffer(L, s, s.Length, s);
		}

		// LUALIB_API void luaL_checktype (lua_State *L, int narg, int t)
		public static void luaL_checktype(IntPtr L, int narg, int t)
		{
			if (lua_type(L, narg) != t)
				melua_error(L, string.Format("{0} expected, got {1}"), lua_typename(L, t), luaL_typename(L, narg));
		}

		// #define abs_index(L, i)		((i) > 0 || (i) <= LUA_REGISTRYINDEX ? (i) : lua_gettop(L) + (i) + 1)
		internal static int abs_index(IntPtr L, int i)
		{
			return (i > 0 || i <= LUA_REGISTRYINDEX ? i : lua_gettop(L) + i + 1);
		}

		//static int checkint(lua_State* L, int topop)
		internal static int checkint(IntPtr L, int topop)
		{
			int n = (lua_type(L, -1) == LUA_TNUMBER) ? lua_tointeger(L, -1) : -1;
			lua_pop(L, topop);
			return n;
		}

		// static void getsizes(lua_State* L)
		internal static void getsizes(IntPtr L)
		{
			lua_getfield(L, LUA_REGISTRYINDEX, "LUA_SIZES");
			if (lua_isnil(L, -1))
			{  /* no `size' table? */
				lua_pop(L, 1);  /* remove nil */
				lua_newtable(L);  /* create it */
				lua_pushvalue(L, -1);  /* `size' will be its own metatable */
				lua_setmetatable(L, -2);
				lua_pushliteral(L, "kv");
				lua_setfield(L, -2, "__mode");  /* metatable(N).__mode = "kv" */
				lua_pushvalue(L, -1);
				lua_setfield(L, LUA_REGISTRYINDEX, "LUA_SIZES");  /* store in register */
			}
		}

		// LUALIB_API int luaL_getn (lua_State *L, int t) 
		public static int luaL_getn(IntPtr L, int t)
		{
			int n;
			t = abs_index(L, t);
			lua_pushliteral(L, "n");
			lua_rawget(L, t);

			if ((n = checkint(L, 1)) >= 0)
				return n;

			getsizes(L);
			lua_pushvalue(L, t);
			lua_rawget(L, -2);

			if ((n = checkint(L, 2)) >= 0)
				return n;

			return lua_objlen(L, t);
		}

		// #define lua_upvalueindex(i)	(LUA_GLOBALSINDEX-(i))
		public static int lua_upvalueindex(int i)
		{
			return (LUA_GLOBALSINDEX - i);
		}

		/// <summary>
		/// Custom function, calls the specified standard lib open functions.
		/// </summary>
		/// <param name="L"></param>
		/// <param name="libsToLoad"></param>
		public static void melua_openlib(IntPtr L, params LuaLib[] libsToLoad)
		{
			foreach (var lib in libsToLoad)
			{
				string name;
				LuaNativeFunction func;

				switch (lib)
				{
					case LuaLib.Base:
						name = "";
						func = luaopen_base;
						break;
					case LuaLib.Package:
						name = "package";
						func = luaopen_package;
						break;
					case LuaLib.Table:
						name = "table";
						func = luaopen_table;
						break;
					case LuaLib.IO:
						name = "io";
						func = luaopen_io;
						break;
					case LuaLib.OS:
						name = "os";
						func = luaopen_os;
						break;
					case LuaLib.String:
						name = "string";
						func = luaopen_string;
						break;
					case LuaLib.Math:
						name = "math";
						func = luaopen_math;
						break;
					case LuaLib.Debug:
						name = "debug";
						func = luaopen_debug;
						break;

					case LuaLib.BaseSafe:
						// Safe functions from base lib
						auxopen(L, "ipairs", CreateFunctionReference(L, Melua.luaB_ipairs), CreateFunctionReference(L, Melua.ipairsaux));
						auxopen(L, "pairs", CreateFunctionReference(L, Melua.luaB_pairs), CreateFunctionReference(L, Melua.luaB_next));
						lua_register(L, "pcall", CreateFunctionReference(L, Melua.luaB_pcall));
						lua_register(L, "print", CreateFunctionReference(L, Melua.luaB_print));
						lua_register(L, "tonumber", CreateFunctionReference(L, Melua.luaB_tonumber));
						lua_register(L, "tostring", CreateFunctionReference(L, Melua.luaB_tostring));
						lua_register(L, "type", CreateFunctionReference(L, Melua.luaB_type));
						lua_register(L, "unpack", CreateFunctionReference(L, Melua.luaB_unpack));
						lua_register(L, "xpcall", CreateFunctionReference(L, Melua.luaB_xpcall));
						continue;

					default:
						throw new Exception("Unknown lua lib '" + lib + "'.");
				}

				lua_pushcfunction(L, func);
				lua_pushstring(L, name);
				lua_call(L, 1, 0);
			}
		}

		/// <summary>
		/// Custom function, calls all standard lib open functions.
		/// </summary>
		/// <param name="L"></param>
		public static void melua_openlibs(IntPtr L)
		{
			melua_openlib(L, LuaLib.Base, LuaLib.Package, LuaLib.Table, LuaLib.IO, LuaLib.OS, LuaLib.String, LuaLib.Math, LuaLib.Debug);
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

		// static int luaB_tostring (lua_State *L)
		internal static int luaB_tostring(IntPtr L)
		{
			luaL_checkany(L, 1);
			if (luaL_callmeta(L, 1, "__tostring"))
				return 1;

			switch (lua_type(L, 1))
			{
				case LUA_TNUMBER:
					lua_pushstring(L, lua_tostring(L, 1));
					break;

				case LUA_TSTRING:
					lua_pushvalue(L, 1);
					break;

				case LUA_TBOOLEAN:
					lua_pushstring(L, (lua_toboolean(L, 1) ? "true" : "false"));
					break;

				case LUA_TNIL:
					lua_pushliteral(L, "nil");
					break;

				default:
					//lua_pushstring(L, string.Format("{0}: {1}", luaL_typename(L, 1), lua_topointer(L, 1)));
					lua_pushstring(L, "unknown");
					break;
			}

			return 1;
		}

		// static int luaB_print (lua_State *L)
		internal static int luaB_print(IntPtr L)
		{
			var n = lua_gettop(L);

			lua_getglobal(L, "tostring");
			for (int i = 1; i <= n; i++)
			{
				lua_pushvalue(L, -1);
				lua_pushvalue(L, i);
				lua_call(L, 1, 1);

				var s = lua_tostring(L, -1);
				if (s == null)
					return melua_error(L, "tostring must return a string to print");

				if (i > 1)
					Console.Write("\t");

				Console.Write(s);
				lua_pop(L, 1);
			}
			Console.Write("\n");
			return 0;
		}

		// static int luaB_tonumber (lua_State *L)
		internal static int luaB_tonumber(IntPtr L)
		{
			var nBase = luaL_optinteger(L, 2, 10);
			if (nBase != 2 && nBase != 8 && nBase != 10 && nBase != 16)
				melua_error(L, "base out of range");

			if (nBase == 10)
			{
				luaL_checkany(L, 1);
				if (lua_isnumber(L, 1))
				{
					lua_pushnumber(L, lua_tonumber(L, 1));
					return 1;
				}
			}
			else
			{
				var s1 = luaL_checkstring(L, 1).Trim();

				try
				{
					lua_pushnumber(L, Convert.ToInt32(s1, nBase));
					return 1;
				}
				catch
				{
				}
			}

			lua_pushnil(L);

			return 1;
		}

		// static int luaB_type (lua_State *L) {
		internal static int luaB_type(IntPtr L)
		{
			luaL_checkany(L, 1);
			lua_pushstring(L, luaL_typename(L, 1));
			return 1;
		}

		// static int luaB_unpack(lua_State* L)
		internal static int luaB_unpack(IntPtr L)
		{
			luaL_checktype(L, 1, LUA_TTABLE);

			var i = luaL_optinteger(L, 2, 1);
			var e = luaL_opt(L, luaL_checkinteger, 3, luaL_getn(L, 1));

			if (i > e)
				return 0;

			var n = e - i + 1;
			if (n <= 0 || !lua_checkstack(L, n))
				return melua_error(L, "too many results to unpack");

			lua_rawgeti(L, 1, i);
			while (i++ < e)
				lua_rawgeti(L, 1, i);

			return n;
		}

		// static int luaB_pairs(lua_State* L)
		internal static int luaB_pairs(IntPtr L)
		{
			luaL_checktype(L, 1, LUA_TTABLE);
			lua_pushvalue(L, lua_upvalueindex(1));
			lua_pushvalue(L, 1);
			lua_pushnil(L);

			return 3;
		}

		// static int luaB_ipairs(lua_State* L)
		internal static int luaB_ipairs(IntPtr L)
		{
			luaL_checktype(L, 1, LUA_TTABLE);
			lua_pushvalue(L, lua_upvalueindex(1));
			lua_pushvalue(L, 1);
			lua_pushinteger(L, 0);

			return 3;
		}

		// static int luaB_pcall(lua_State* L)
		internal static int luaB_pcall(IntPtr L)
		{
			luaL_checkany(L, 1);

			var status = lua_pcall(L, lua_gettop(L) - 1, LUA_MULTRET, 0);
			lua_pushboolean(L, (status == 0));
			lua_insert(L, 1);

			return lua_gettop(L);
		}

		// static int luaB_xpcall(lua_State* L)
		internal static int luaB_xpcall(IntPtr L)
		{
			luaL_checkany(L, 2);

			lua_settop(L, 2);
			lua_insert(L, 1);

			var status = lua_pcall(L, 0, LUA_MULTRET, 1);
			lua_pushboolean(L, (status == 0));
			lua_replace(L, 1);

			return lua_gettop(L);
		}

		private static Dictionary<IntPtr, List<LuaNativeFunction>> _functions = new Dictionary<IntPtr, List<LuaNativeFunction>>();

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
	}
}

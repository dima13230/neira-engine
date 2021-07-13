﻿









using System.Numerics;

namespace ShaderGen
{
    public static class ShaderSwizzle1
    {
	
	public static float X(this float v) => v;
	
	public static Vector2 XX(this float v) => new Vector2(v);
	
	public static Vector3 XXX(this float v) => new Vector3(v);
	
	public static Vector4 XXXX(this float v) => new Vector4(v);
	
	public static float X(this Vector2 v) => v.X;
	
	public static float Y(this Vector2 v) => v.Y;
	
	public static Vector2 XX(this Vector2 v) => new Vector2(v.X, v.X);
	
	public static Vector2 XY(this Vector2 v) => new Vector2(v.X, v.Y);
	
	public static Vector2 YX(this Vector2 v) => new Vector2(v.Y, v.X);
	
	public static Vector2 YY(this Vector2 v) => new Vector2(v.Y, v.Y);
	
	public static Vector3 XXX(this Vector2 v) => new Vector3(v.X, v.X, v.X);
	
	public static Vector3 XXY(this Vector2 v) => new Vector3(v.X, v.X, v.Y);
	
	public static Vector3 XYX(this Vector2 v) => new Vector3(v.X, v.Y, v.X);
	
	public static Vector3 XYY(this Vector2 v) => new Vector3(v.X, v.Y, v.Y);
	
	public static Vector3 YXX(this Vector2 v) => new Vector3(v.Y, v.X, v.X);
	
	public static Vector3 YXY(this Vector2 v) => new Vector3(v.Y, v.X, v.Y);
	
	public static Vector3 YYX(this Vector2 v) => new Vector3(v.Y, v.Y, v.X);
	
	public static Vector3 YYY(this Vector2 v) => new Vector3(v.Y, v.Y, v.Y);
	
	public static Vector4 XXXX(this Vector2 v) => new Vector4(v.X, v.X, v.X, v.X);
	
	public static Vector4 XXXY(this Vector2 v) => new Vector4(v.X, v.X, v.X, v.Y);
	
	public static Vector4 XXYX(this Vector2 v) => new Vector4(v.X, v.X, v.Y, v.X);
	
	public static Vector4 XXYY(this Vector2 v) => new Vector4(v.X, v.X, v.Y, v.Y);
	
	public static Vector4 XYXX(this Vector2 v) => new Vector4(v.X, v.Y, v.X, v.X);
	
	public static Vector4 XYXY(this Vector2 v) => new Vector4(v.X, v.Y, v.X, v.Y);
	
	public static Vector4 XYYX(this Vector2 v) => new Vector4(v.X, v.Y, v.Y, v.X);
	
	public static Vector4 XYYY(this Vector2 v) => new Vector4(v.X, v.Y, v.Y, v.Y);
	
	public static Vector4 YXXX(this Vector2 v) => new Vector4(v.Y, v.X, v.X, v.X);
	
	public static Vector4 YXXY(this Vector2 v) => new Vector4(v.Y, v.X, v.X, v.Y);
	
	public static Vector4 YXYX(this Vector2 v) => new Vector4(v.Y, v.X, v.Y, v.X);
	
	public static Vector4 YXYY(this Vector2 v) => new Vector4(v.Y, v.X, v.Y, v.Y);
	
	public static Vector4 YYXX(this Vector2 v) => new Vector4(v.Y, v.Y, v.X, v.X);
	
	public static Vector4 YYXY(this Vector2 v) => new Vector4(v.Y, v.Y, v.X, v.Y);
	
	public static Vector4 YYYX(this Vector2 v) => new Vector4(v.Y, v.Y, v.Y, v.X);
	
	public static Vector4 YYYY(this Vector2 v) => new Vector4(v.Y, v.Y, v.Y, v.Y);
	
	public static float X(this Vector3 v) => v.X;
	
	public static float Y(this Vector3 v) => v.Y;
	
	public static float Z(this Vector3 v) => v.Z;
	
	public static Vector2 XX(this Vector3 v) => new Vector2(v.X, v.X);
	
	public static Vector2 XY(this Vector3 v) => new Vector2(v.X, v.Y);
	
	public static Vector2 XZ(this Vector3 v) => new Vector2(v.X, v.Z);
	
	public static Vector2 YX(this Vector3 v) => new Vector2(v.Y, v.X);
	
	public static Vector2 YY(this Vector3 v) => new Vector2(v.Y, v.Y);
	
	public static Vector2 YZ(this Vector3 v) => new Vector2(v.Y, v.Z);
	
	public static Vector2 ZX(this Vector3 v) => new Vector2(v.Z, v.X);
	
	public static Vector2 ZY(this Vector3 v) => new Vector2(v.Z, v.Y);
	
	public static Vector2 ZZ(this Vector3 v) => new Vector2(v.Z, v.Z);
	
	public static Vector3 XXX(this Vector3 v) => new Vector3(v.X, v.X, v.X);
	
	public static Vector3 XXY(this Vector3 v) => new Vector3(v.X, v.X, v.Y);
	
	public static Vector3 XXZ(this Vector3 v) => new Vector3(v.X, v.X, v.Z);
	
	public static Vector3 XYX(this Vector3 v) => new Vector3(v.X, v.Y, v.X);
	
	public static Vector3 XYY(this Vector3 v) => new Vector3(v.X, v.Y, v.Y);
	
	public static Vector3 XYZ(this Vector3 v) => new Vector3(v.X, v.Y, v.Z);
	
	public static Vector3 XZX(this Vector3 v) => new Vector3(v.X, v.Z, v.X);
	
	public static Vector3 XZY(this Vector3 v) => new Vector3(v.X, v.Z, v.Y);
	
	public static Vector3 XZZ(this Vector3 v) => new Vector3(v.X, v.Z, v.Z);
	
	public static Vector3 YXX(this Vector3 v) => new Vector3(v.Y, v.X, v.X);
	
	public static Vector3 YXY(this Vector3 v) => new Vector3(v.Y, v.X, v.Y);
	
	public static Vector3 YXZ(this Vector3 v) => new Vector3(v.Y, v.X, v.Z);
	
	public static Vector3 YYX(this Vector3 v) => new Vector3(v.Y, v.Y, v.X);
	
	public static Vector3 YYY(this Vector3 v) => new Vector3(v.Y, v.Y, v.Y);
	
	public static Vector3 YYZ(this Vector3 v) => new Vector3(v.Y, v.Y, v.Z);
	
	public static Vector3 YZX(this Vector3 v) => new Vector3(v.Y, v.Z, v.X);
	
	public static Vector3 YZY(this Vector3 v) => new Vector3(v.Y, v.Z, v.Y);
	
	public static Vector3 YZZ(this Vector3 v) => new Vector3(v.Y, v.Z, v.Z);
	
	public static Vector3 ZXX(this Vector3 v) => new Vector3(v.Z, v.X, v.X);
	
	public static Vector3 ZXY(this Vector3 v) => new Vector3(v.Z, v.X, v.Y);
	
	public static Vector3 ZXZ(this Vector3 v) => new Vector3(v.Z, v.X, v.Z);
	
	public static Vector3 ZYX(this Vector3 v) => new Vector3(v.Z, v.Y, v.X);
	
	public static Vector3 ZYY(this Vector3 v) => new Vector3(v.Z, v.Y, v.Y);
	
	public static Vector3 ZYZ(this Vector3 v) => new Vector3(v.Z, v.Y, v.Z);
	
	public static Vector3 ZZX(this Vector3 v) => new Vector3(v.Z, v.Z, v.X);
	
	public static Vector3 ZZY(this Vector3 v) => new Vector3(v.Z, v.Z, v.Y);
	
	public static Vector3 ZZZ(this Vector3 v) => new Vector3(v.Z, v.Z, v.Z);
	
	public static Vector4 XXXX(this Vector3 v) => new Vector4(v.X, v.X, v.X, v.X);
	
	public static Vector4 XXXY(this Vector3 v) => new Vector4(v.X, v.X, v.X, v.Y);
	
	public static Vector4 XXXZ(this Vector3 v) => new Vector4(v.X, v.X, v.X, v.Z);
	
	public static Vector4 XXYX(this Vector3 v) => new Vector4(v.X, v.X, v.Y, v.X);
	
	public static Vector4 XXYY(this Vector3 v) => new Vector4(v.X, v.X, v.Y, v.Y);
	
	public static Vector4 XXYZ(this Vector3 v) => new Vector4(v.X, v.X, v.Y, v.Z);
	
	public static Vector4 XXZX(this Vector3 v) => new Vector4(v.X, v.X, v.Z, v.X);
	
	public static Vector4 XXZY(this Vector3 v) => new Vector4(v.X, v.X, v.Z, v.Y);
	
	public static Vector4 XXZZ(this Vector3 v) => new Vector4(v.X, v.X, v.Z, v.Z);
	
	public static Vector4 XYXX(this Vector3 v) => new Vector4(v.X, v.Y, v.X, v.X);
	
	public static Vector4 XYXY(this Vector3 v) => new Vector4(v.X, v.Y, v.X, v.Y);
	
	public static Vector4 XYXZ(this Vector3 v) => new Vector4(v.X, v.Y, v.X, v.Z);
	
	public static Vector4 XYYX(this Vector3 v) => new Vector4(v.X, v.Y, v.Y, v.X);
	
	public static Vector4 XYYY(this Vector3 v) => new Vector4(v.X, v.Y, v.Y, v.Y);
	
	public static Vector4 XYYZ(this Vector3 v) => new Vector4(v.X, v.Y, v.Y, v.Z);
	
	public static Vector4 XYZX(this Vector3 v) => new Vector4(v.X, v.Y, v.Z, v.X);
	
	public static Vector4 XYZY(this Vector3 v) => new Vector4(v.X, v.Y, v.Z, v.Y);
	
	public static Vector4 XYZZ(this Vector3 v) => new Vector4(v.X, v.Y, v.Z, v.Z);
	
	public static Vector4 XZXX(this Vector3 v) => new Vector4(v.X, v.Z, v.X, v.X);
	
	public static Vector4 XZXY(this Vector3 v) => new Vector4(v.X, v.Z, v.X, v.Y);
	
	public static Vector4 XZXZ(this Vector3 v) => new Vector4(v.X, v.Z, v.X, v.Z);
	
	public static Vector4 XZYX(this Vector3 v) => new Vector4(v.X, v.Z, v.Y, v.X);
	
	public static Vector4 XZYY(this Vector3 v) => new Vector4(v.X, v.Z, v.Y, v.Y);
	
	public static Vector4 XZYZ(this Vector3 v) => new Vector4(v.X, v.Z, v.Y, v.Z);
	
	public static Vector4 XZZX(this Vector3 v) => new Vector4(v.X, v.Z, v.Z, v.X);
	
	public static Vector4 XZZY(this Vector3 v) => new Vector4(v.X, v.Z, v.Z, v.Y);
	
	public static Vector4 XZZZ(this Vector3 v) => new Vector4(v.X, v.Z, v.Z, v.Z);
	
	public static Vector4 YXXX(this Vector3 v) => new Vector4(v.Y, v.X, v.X, v.X);
	
	public static Vector4 YXXY(this Vector3 v) => new Vector4(v.Y, v.X, v.X, v.Y);
	
	public static Vector4 YXXZ(this Vector3 v) => new Vector4(v.Y, v.X, v.X, v.Z);
	
	public static Vector4 YXYX(this Vector3 v) => new Vector4(v.Y, v.X, v.Y, v.X);
	
	public static Vector4 YXYY(this Vector3 v) => new Vector4(v.Y, v.X, v.Y, v.Y);
	
	public static Vector4 YXYZ(this Vector3 v) => new Vector4(v.Y, v.X, v.Y, v.Z);
	
	public static Vector4 YXZX(this Vector3 v) => new Vector4(v.Y, v.X, v.Z, v.X);
	
	public static Vector4 YXZY(this Vector3 v) => new Vector4(v.Y, v.X, v.Z, v.Y);
	
	public static Vector4 YXZZ(this Vector3 v) => new Vector4(v.Y, v.X, v.Z, v.Z);
	
	public static Vector4 YYXX(this Vector3 v) => new Vector4(v.Y, v.Y, v.X, v.X);
	
	public static Vector4 YYXY(this Vector3 v) => new Vector4(v.Y, v.Y, v.X, v.Y);
	
	public static Vector4 YYXZ(this Vector3 v) => new Vector4(v.Y, v.Y, v.X, v.Z);
	
	public static Vector4 YYYX(this Vector3 v) => new Vector4(v.Y, v.Y, v.Y, v.X);
	
	public static Vector4 YYYY(this Vector3 v) => new Vector4(v.Y, v.Y, v.Y, v.Y);
	
	public static Vector4 YYYZ(this Vector3 v) => new Vector4(v.Y, v.Y, v.Y, v.Z);
	
	public static Vector4 YYZX(this Vector3 v) => new Vector4(v.Y, v.Y, v.Z, v.X);
	
	public static Vector4 YYZY(this Vector3 v) => new Vector4(v.Y, v.Y, v.Z, v.Y);
	
	public static Vector4 YYZZ(this Vector3 v) => new Vector4(v.Y, v.Y, v.Z, v.Z);
	
	public static Vector4 YZXX(this Vector3 v) => new Vector4(v.Y, v.Z, v.X, v.X);
	
	public static Vector4 YZXY(this Vector3 v) => new Vector4(v.Y, v.Z, v.X, v.Y);
	
	public static Vector4 YZXZ(this Vector3 v) => new Vector4(v.Y, v.Z, v.X, v.Z);
	
	public static Vector4 YZYX(this Vector3 v) => new Vector4(v.Y, v.Z, v.Y, v.X);
	
	public static Vector4 YZYY(this Vector3 v) => new Vector4(v.Y, v.Z, v.Y, v.Y);
	
	public static Vector4 YZYZ(this Vector3 v) => new Vector4(v.Y, v.Z, v.Y, v.Z);
	
	public static Vector4 YZZX(this Vector3 v) => new Vector4(v.Y, v.Z, v.Z, v.X);
	
	public static Vector4 YZZY(this Vector3 v) => new Vector4(v.Y, v.Z, v.Z, v.Y);
	
	public static Vector4 YZZZ(this Vector3 v) => new Vector4(v.Y, v.Z, v.Z, v.Z);
	
	public static Vector4 ZXXX(this Vector3 v) => new Vector4(v.Z, v.X, v.X, v.X);
	
	public static Vector4 ZXXY(this Vector3 v) => new Vector4(v.Z, v.X, v.X, v.Y);
	
	public static Vector4 ZXXZ(this Vector3 v) => new Vector4(v.Z, v.X, v.X, v.Z);
	
	public static Vector4 ZXYX(this Vector3 v) => new Vector4(v.Z, v.X, v.Y, v.X);
	
	public static Vector4 ZXYY(this Vector3 v) => new Vector4(v.Z, v.X, v.Y, v.Y);
	
	public static Vector4 ZXYZ(this Vector3 v) => new Vector4(v.Z, v.X, v.Y, v.Z);
	
	public static Vector4 ZXZX(this Vector3 v) => new Vector4(v.Z, v.X, v.Z, v.X);
	
	public static Vector4 ZXZY(this Vector3 v) => new Vector4(v.Z, v.X, v.Z, v.Y);
	
	public static Vector4 ZXZZ(this Vector3 v) => new Vector4(v.Z, v.X, v.Z, v.Z);
	
	public static Vector4 ZYXX(this Vector3 v) => new Vector4(v.Z, v.Y, v.X, v.X);
	
	public static Vector4 ZYXY(this Vector3 v) => new Vector4(v.Z, v.Y, v.X, v.Y);
	
	public static Vector4 ZYXZ(this Vector3 v) => new Vector4(v.Z, v.Y, v.X, v.Z);
	
	public static Vector4 ZYYX(this Vector3 v) => new Vector4(v.Z, v.Y, v.Y, v.X);
	
	public static Vector4 ZYYY(this Vector3 v) => new Vector4(v.Z, v.Y, v.Y, v.Y);
	
	public static Vector4 ZYYZ(this Vector3 v) => new Vector4(v.Z, v.Y, v.Y, v.Z);
	
	public static Vector4 ZYZX(this Vector3 v) => new Vector4(v.Z, v.Y, v.Z, v.X);
	
	public static Vector4 ZYZY(this Vector3 v) => new Vector4(v.Z, v.Y, v.Z, v.Y);
	
	public static Vector4 ZYZZ(this Vector3 v) => new Vector4(v.Z, v.Y, v.Z, v.Z);
	
	public static Vector4 ZZXX(this Vector3 v) => new Vector4(v.Z, v.Z, v.X, v.X);
	
	public static Vector4 ZZXY(this Vector3 v) => new Vector4(v.Z, v.Z, v.X, v.Y);
	
	public static Vector4 ZZXZ(this Vector3 v) => new Vector4(v.Z, v.Z, v.X, v.Z);
	
	public static Vector4 ZZYX(this Vector3 v) => new Vector4(v.Z, v.Z, v.Y, v.X);
	
	public static Vector4 ZZYY(this Vector3 v) => new Vector4(v.Z, v.Z, v.Y, v.Y);
	
	public static Vector4 ZZYZ(this Vector3 v) => new Vector4(v.Z, v.Z, v.Y, v.Z);
	
	public static Vector4 ZZZX(this Vector3 v) => new Vector4(v.Z, v.Z, v.Z, v.X);
	
	public static Vector4 ZZZY(this Vector3 v) => new Vector4(v.Z, v.Z, v.Z, v.Y);
	
	public static Vector4 ZZZZ(this Vector3 v) => new Vector4(v.Z, v.Z, v.Z, v.Z);
	
	public static float X(this Vector4 v) => v.X;
	
	public static float Y(this Vector4 v) => v.Y;
	
	public static float Z(this Vector4 v) => v.Z;
	
	public static float W(this Vector4 v) => v.W;
	
	public static Vector2 XX(this Vector4 v) => new Vector2(v.X, v.X);
	
	public static Vector2 XY(this Vector4 v) => new Vector2(v.X, v.Y);
	
	public static Vector2 XZ(this Vector4 v) => new Vector2(v.X, v.Z);
	
	public static Vector2 XW(this Vector4 v) => new Vector2(v.X, v.W);
	
	public static Vector2 YX(this Vector4 v) => new Vector2(v.Y, v.X);
	
	public static Vector2 YY(this Vector4 v) => new Vector2(v.Y, v.Y);
	
	public static Vector2 YZ(this Vector4 v) => new Vector2(v.Y, v.Z);
	
	public static Vector2 YW(this Vector4 v) => new Vector2(v.Y, v.W);
	
	public static Vector2 ZX(this Vector4 v) => new Vector2(v.Z, v.X);
	
	public static Vector2 ZY(this Vector4 v) => new Vector2(v.Z, v.Y);
	
	public static Vector2 ZZ(this Vector4 v) => new Vector2(v.Z, v.Z);
	
	public static Vector2 ZW(this Vector4 v) => new Vector2(v.Z, v.W);
	
	public static Vector2 WX(this Vector4 v) => new Vector2(v.W, v.X);
	
	public static Vector2 WY(this Vector4 v) => new Vector2(v.W, v.Y);
	
	public static Vector2 WZ(this Vector4 v) => new Vector2(v.W, v.Z);
	
	public static Vector2 WW(this Vector4 v) => new Vector2(v.W, v.W);
	
	public static Vector3 XXX(this Vector4 v) => new Vector3(v.X, v.X, v.X);
	
	public static Vector3 XXY(this Vector4 v) => new Vector3(v.X, v.X, v.Y);
	
	public static Vector3 XXZ(this Vector4 v) => new Vector3(v.X, v.X, v.Z);
	
	public static Vector3 XXW(this Vector4 v) => new Vector3(v.X, v.X, v.W);
	
	public static Vector3 XYX(this Vector4 v) => new Vector3(v.X, v.Y, v.X);
	
	public static Vector3 XYY(this Vector4 v) => new Vector3(v.X, v.Y, v.Y);
	
	public static Vector3 XYZ(this Vector4 v) => new Vector3(v.X, v.Y, v.Z);
	
	public static Vector3 XYW(this Vector4 v) => new Vector3(v.X, v.Y, v.W);
	
	public static Vector3 XZX(this Vector4 v) => new Vector3(v.X, v.Z, v.X);
	
	public static Vector3 XZY(this Vector4 v) => new Vector3(v.X, v.Z, v.Y);
	
	public static Vector3 XZZ(this Vector4 v) => new Vector3(v.X, v.Z, v.Z);
	
	public static Vector3 XZW(this Vector4 v) => new Vector3(v.X, v.Z, v.W);
	
	public static Vector3 XWX(this Vector4 v) => new Vector3(v.X, v.W, v.X);
	
	public static Vector3 XWY(this Vector4 v) => new Vector3(v.X, v.W, v.Y);
	
	public static Vector3 XWZ(this Vector4 v) => new Vector3(v.X, v.W, v.Z);
	
	public static Vector3 XWW(this Vector4 v) => new Vector3(v.X, v.W, v.W);
	
	public static Vector3 YXX(this Vector4 v) => new Vector3(v.Y, v.X, v.X);
	
	public static Vector3 YXY(this Vector4 v) => new Vector3(v.Y, v.X, v.Y);
	
	public static Vector3 YXZ(this Vector4 v) => new Vector3(v.Y, v.X, v.Z);
	
	public static Vector3 YXW(this Vector4 v) => new Vector3(v.Y, v.X, v.W);
	
	public static Vector3 YYX(this Vector4 v) => new Vector3(v.Y, v.Y, v.X);
	
	public static Vector3 YYY(this Vector4 v) => new Vector3(v.Y, v.Y, v.Y);
	
	public static Vector3 YYZ(this Vector4 v) => new Vector3(v.Y, v.Y, v.Z);
	
	public static Vector3 YYW(this Vector4 v) => new Vector3(v.Y, v.Y, v.W);
	
	public static Vector3 YZX(this Vector4 v) => new Vector3(v.Y, v.Z, v.X);
	
	public static Vector3 YZY(this Vector4 v) => new Vector3(v.Y, v.Z, v.Y);
	
	public static Vector3 YZZ(this Vector4 v) => new Vector3(v.Y, v.Z, v.Z);
	
	public static Vector3 YZW(this Vector4 v) => new Vector3(v.Y, v.Z, v.W);
	
	public static Vector3 YWX(this Vector4 v) => new Vector3(v.Y, v.W, v.X);
	
	public static Vector3 YWY(this Vector4 v) => new Vector3(v.Y, v.W, v.Y);
	
	public static Vector3 YWZ(this Vector4 v) => new Vector3(v.Y, v.W, v.Z);
	
	public static Vector3 YWW(this Vector4 v) => new Vector3(v.Y, v.W, v.W);
	
	public static Vector3 ZXX(this Vector4 v) => new Vector3(v.Z, v.X, v.X);
	
	public static Vector3 ZXY(this Vector4 v) => new Vector3(v.Z, v.X, v.Y);
	
	public static Vector3 ZXZ(this Vector4 v) => new Vector3(v.Z, v.X, v.Z);
	
	public static Vector3 ZXW(this Vector4 v) => new Vector3(v.Z, v.X, v.W);
	
	public static Vector3 ZYX(this Vector4 v) => new Vector3(v.Z, v.Y, v.X);
	
	public static Vector3 ZYY(this Vector4 v) => new Vector3(v.Z, v.Y, v.Y);
	
	public static Vector3 ZYZ(this Vector4 v) => new Vector3(v.Z, v.Y, v.Z);
	
	public static Vector3 ZYW(this Vector4 v) => new Vector3(v.Z, v.Y, v.W);
	
	public static Vector3 ZZX(this Vector4 v) => new Vector3(v.Z, v.Z, v.X);
	
	public static Vector3 ZZY(this Vector4 v) => new Vector3(v.Z, v.Z, v.Y);
	
	public static Vector3 ZZZ(this Vector4 v) => new Vector3(v.Z, v.Z, v.Z);
	
	public static Vector3 ZZW(this Vector4 v) => new Vector3(v.Z, v.Z, v.W);
	
	public static Vector3 ZWX(this Vector4 v) => new Vector3(v.Z, v.W, v.X);
	
	public static Vector3 ZWY(this Vector4 v) => new Vector3(v.Z, v.W, v.Y);
	
	public static Vector3 ZWZ(this Vector4 v) => new Vector3(v.Z, v.W, v.Z);
	
	public static Vector3 ZWW(this Vector4 v) => new Vector3(v.Z, v.W, v.W);
	
	public static Vector3 WXX(this Vector4 v) => new Vector3(v.W, v.X, v.X);
	
	public static Vector3 WXY(this Vector4 v) => new Vector3(v.W, v.X, v.Y);
	
	public static Vector3 WXZ(this Vector4 v) => new Vector3(v.W, v.X, v.Z);
	
	public static Vector3 WXW(this Vector4 v) => new Vector3(v.W, v.X, v.W);
	
	public static Vector3 WYX(this Vector4 v) => new Vector3(v.W, v.Y, v.X);
	
	public static Vector3 WYY(this Vector4 v) => new Vector3(v.W, v.Y, v.Y);
	
	public static Vector3 WYZ(this Vector4 v) => new Vector3(v.W, v.Y, v.Z);
	
	public static Vector3 WYW(this Vector4 v) => new Vector3(v.W, v.Y, v.W);
	
	public static Vector3 WZX(this Vector4 v) => new Vector3(v.W, v.Z, v.X);
	
	public static Vector3 WZY(this Vector4 v) => new Vector3(v.W, v.Z, v.Y);
	
	public static Vector3 WZZ(this Vector4 v) => new Vector3(v.W, v.Z, v.Z);
	
	public static Vector3 WZW(this Vector4 v) => new Vector3(v.W, v.Z, v.W);
	
	public static Vector3 WWX(this Vector4 v) => new Vector3(v.W, v.W, v.X);
	
	public static Vector3 WWY(this Vector4 v) => new Vector3(v.W, v.W, v.Y);
	
	public static Vector3 WWZ(this Vector4 v) => new Vector3(v.W, v.W, v.Z);
	
	public static Vector3 WWW(this Vector4 v) => new Vector3(v.W, v.W, v.W);
	
	public static Vector4 XXXX(this Vector4 v) => new Vector4(v.X, v.X, v.X, v.X);
	
	public static Vector4 XXXY(this Vector4 v) => new Vector4(v.X, v.X, v.X, v.Y);
	
	public static Vector4 XXXZ(this Vector4 v) => new Vector4(v.X, v.X, v.X, v.Z);
	
	public static Vector4 XXXW(this Vector4 v) => new Vector4(v.X, v.X, v.X, v.W);
	
	public static Vector4 XXYX(this Vector4 v) => new Vector4(v.X, v.X, v.Y, v.X);
	
	public static Vector4 XXYY(this Vector4 v) => new Vector4(v.X, v.X, v.Y, v.Y);
	
	public static Vector4 XXYZ(this Vector4 v) => new Vector4(v.X, v.X, v.Y, v.Z);
	
	public static Vector4 XXYW(this Vector4 v) => new Vector4(v.X, v.X, v.Y, v.W);
	
	public static Vector4 XXZX(this Vector4 v) => new Vector4(v.X, v.X, v.Z, v.X);
	
	public static Vector4 XXZY(this Vector4 v) => new Vector4(v.X, v.X, v.Z, v.Y);
	
	public static Vector4 XXZZ(this Vector4 v) => new Vector4(v.X, v.X, v.Z, v.Z);
	
	public static Vector4 XXZW(this Vector4 v) => new Vector4(v.X, v.X, v.Z, v.W);
	
	public static Vector4 XXWX(this Vector4 v) => new Vector4(v.X, v.X, v.W, v.X);
	
	public static Vector4 XXWY(this Vector4 v) => new Vector4(v.X, v.X, v.W, v.Y);
	
	public static Vector4 XXWZ(this Vector4 v) => new Vector4(v.X, v.X, v.W, v.Z);
	
	public static Vector4 XXWW(this Vector4 v) => new Vector4(v.X, v.X, v.W, v.W);
	
	public static Vector4 XYXX(this Vector4 v) => new Vector4(v.X, v.Y, v.X, v.X);
	
	public static Vector4 XYXY(this Vector4 v) => new Vector4(v.X, v.Y, v.X, v.Y);
	
	public static Vector4 XYXZ(this Vector4 v) => new Vector4(v.X, v.Y, v.X, v.Z);
	
	public static Vector4 XYXW(this Vector4 v) => new Vector4(v.X, v.Y, v.X, v.W);
	
	public static Vector4 XYYX(this Vector4 v) => new Vector4(v.X, v.Y, v.Y, v.X);
	
	public static Vector4 XYYY(this Vector4 v) => new Vector4(v.X, v.Y, v.Y, v.Y);
	
	public static Vector4 XYYZ(this Vector4 v) => new Vector4(v.X, v.Y, v.Y, v.Z);
	
	public static Vector4 XYYW(this Vector4 v) => new Vector4(v.X, v.Y, v.Y, v.W);
	
	public static Vector4 XYZX(this Vector4 v) => new Vector4(v.X, v.Y, v.Z, v.X);
	
	public static Vector4 XYZY(this Vector4 v) => new Vector4(v.X, v.Y, v.Z, v.Y);
	
	public static Vector4 XYZZ(this Vector4 v) => new Vector4(v.X, v.Y, v.Z, v.Z);
	
	public static Vector4 XYZW(this Vector4 v) => new Vector4(v.X, v.Y, v.Z, v.W);
	
	public static Vector4 XYWX(this Vector4 v) => new Vector4(v.X, v.Y, v.W, v.X);
	
	public static Vector4 XYWY(this Vector4 v) => new Vector4(v.X, v.Y, v.W, v.Y);
	
	public static Vector4 XYWZ(this Vector4 v) => new Vector4(v.X, v.Y, v.W, v.Z);
	
	public static Vector4 XYWW(this Vector4 v) => new Vector4(v.X, v.Y, v.W, v.W);
	
	public static Vector4 XZXX(this Vector4 v) => new Vector4(v.X, v.Z, v.X, v.X);
	
	public static Vector4 XZXY(this Vector4 v) => new Vector4(v.X, v.Z, v.X, v.Y);
	
	public static Vector4 XZXZ(this Vector4 v) => new Vector4(v.X, v.Z, v.X, v.Z);
	
	public static Vector4 XZXW(this Vector4 v) => new Vector4(v.X, v.Z, v.X, v.W);
	
	public static Vector4 XZYX(this Vector4 v) => new Vector4(v.X, v.Z, v.Y, v.X);
	
	public static Vector4 XZYY(this Vector4 v) => new Vector4(v.X, v.Z, v.Y, v.Y);
	
	public static Vector4 XZYZ(this Vector4 v) => new Vector4(v.X, v.Z, v.Y, v.Z);
	
	public static Vector4 XZYW(this Vector4 v) => new Vector4(v.X, v.Z, v.Y, v.W);
	
	public static Vector4 XZZX(this Vector4 v) => new Vector4(v.X, v.Z, v.Z, v.X);
	
	public static Vector4 XZZY(this Vector4 v) => new Vector4(v.X, v.Z, v.Z, v.Y);
	
	public static Vector4 XZZZ(this Vector4 v) => new Vector4(v.X, v.Z, v.Z, v.Z);
	
	public static Vector4 XZZW(this Vector4 v) => new Vector4(v.X, v.Z, v.Z, v.W);
	
	public static Vector4 XZWX(this Vector4 v) => new Vector4(v.X, v.Z, v.W, v.X);
	
	public static Vector4 XZWY(this Vector4 v) => new Vector4(v.X, v.Z, v.W, v.Y);
	
	public static Vector4 XZWZ(this Vector4 v) => new Vector4(v.X, v.Z, v.W, v.Z);
	
	public static Vector4 XZWW(this Vector4 v) => new Vector4(v.X, v.Z, v.W, v.W);
	
	public static Vector4 XWXX(this Vector4 v) => new Vector4(v.X, v.W, v.X, v.X);
	
	public static Vector4 XWXY(this Vector4 v) => new Vector4(v.X, v.W, v.X, v.Y);
	
	public static Vector4 XWXZ(this Vector4 v) => new Vector4(v.X, v.W, v.X, v.Z);
	
	public static Vector4 XWXW(this Vector4 v) => new Vector4(v.X, v.W, v.X, v.W);
	
	public static Vector4 XWYX(this Vector4 v) => new Vector4(v.X, v.W, v.Y, v.X);
	
	public static Vector4 XWYY(this Vector4 v) => new Vector4(v.X, v.W, v.Y, v.Y);
	
	public static Vector4 XWYZ(this Vector4 v) => new Vector4(v.X, v.W, v.Y, v.Z);
	
	public static Vector4 XWYW(this Vector4 v) => new Vector4(v.X, v.W, v.Y, v.W);
	
	public static Vector4 XWZX(this Vector4 v) => new Vector4(v.X, v.W, v.Z, v.X);
	
	public static Vector4 XWZY(this Vector4 v) => new Vector4(v.X, v.W, v.Z, v.Y);
	
	public static Vector4 XWZZ(this Vector4 v) => new Vector4(v.X, v.W, v.Z, v.Z);
	
	public static Vector4 XWZW(this Vector4 v) => new Vector4(v.X, v.W, v.Z, v.W);
	
	public static Vector4 XWWX(this Vector4 v) => new Vector4(v.X, v.W, v.W, v.X);
	
	public static Vector4 XWWY(this Vector4 v) => new Vector4(v.X, v.W, v.W, v.Y);
	
	public static Vector4 XWWZ(this Vector4 v) => new Vector4(v.X, v.W, v.W, v.Z);
	
	public static Vector4 XWWW(this Vector4 v) => new Vector4(v.X, v.W, v.W, v.W);
	
	public static Vector4 YXXX(this Vector4 v) => new Vector4(v.Y, v.X, v.X, v.X);
	
	public static Vector4 YXXY(this Vector4 v) => new Vector4(v.Y, v.X, v.X, v.Y);
	
	public static Vector4 YXXZ(this Vector4 v) => new Vector4(v.Y, v.X, v.X, v.Z);
	
	public static Vector4 YXXW(this Vector4 v) => new Vector4(v.Y, v.X, v.X, v.W);
	
	public static Vector4 YXYX(this Vector4 v) => new Vector4(v.Y, v.X, v.Y, v.X);
	
	public static Vector4 YXYY(this Vector4 v) => new Vector4(v.Y, v.X, v.Y, v.Y);
	
	public static Vector4 YXYZ(this Vector4 v) => new Vector4(v.Y, v.X, v.Y, v.Z);
	
	public static Vector4 YXYW(this Vector4 v) => new Vector4(v.Y, v.X, v.Y, v.W);
	
	public static Vector4 YXZX(this Vector4 v) => new Vector4(v.Y, v.X, v.Z, v.X);
	
	public static Vector4 YXZY(this Vector4 v) => new Vector4(v.Y, v.X, v.Z, v.Y);
	
	public static Vector4 YXZZ(this Vector4 v) => new Vector4(v.Y, v.X, v.Z, v.Z);
	
	public static Vector4 YXZW(this Vector4 v) => new Vector4(v.Y, v.X, v.Z, v.W);
	
	public static Vector4 YXWX(this Vector4 v) => new Vector4(v.Y, v.X, v.W, v.X);
	
	public static Vector4 YXWY(this Vector4 v) => new Vector4(v.Y, v.X, v.W, v.Y);
	
	public static Vector4 YXWZ(this Vector4 v) => new Vector4(v.Y, v.X, v.W, v.Z);
	
	public static Vector4 YXWW(this Vector4 v) => new Vector4(v.Y, v.X, v.W, v.W);
	
	public static Vector4 YYXX(this Vector4 v) => new Vector4(v.Y, v.Y, v.X, v.X);
	
	public static Vector4 YYXY(this Vector4 v) => new Vector4(v.Y, v.Y, v.X, v.Y);
	
	public static Vector4 YYXZ(this Vector4 v) => new Vector4(v.Y, v.Y, v.X, v.Z);
	
	public static Vector4 YYXW(this Vector4 v) => new Vector4(v.Y, v.Y, v.X, v.W);
	
	public static Vector4 YYYX(this Vector4 v) => new Vector4(v.Y, v.Y, v.Y, v.X);
	
	public static Vector4 YYYY(this Vector4 v) => new Vector4(v.Y, v.Y, v.Y, v.Y);
	
	public static Vector4 YYYZ(this Vector4 v) => new Vector4(v.Y, v.Y, v.Y, v.Z);
	
	public static Vector4 YYYW(this Vector4 v) => new Vector4(v.Y, v.Y, v.Y, v.W);
	
	public static Vector4 YYZX(this Vector4 v) => new Vector4(v.Y, v.Y, v.Z, v.X);
	
	public static Vector4 YYZY(this Vector4 v) => new Vector4(v.Y, v.Y, v.Z, v.Y);
	
	public static Vector4 YYZZ(this Vector4 v) => new Vector4(v.Y, v.Y, v.Z, v.Z);
	
	public static Vector4 YYZW(this Vector4 v) => new Vector4(v.Y, v.Y, v.Z, v.W);
	
	public static Vector4 YYWX(this Vector4 v) => new Vector4(v.Y, v.Y, v.W, v.X);
	
	public static Vector4 YYWY(this Vector4 v) => new Vector4(v.Y, v.Y, v.W, v.Y);
	
	public static Vector4 YYWZ(this Vector4 v) => new Vector4(v.Y, v.Y, v.W, v.Z);
	
	public static Vector4 YYWW(this Vector4 v) => new Vector4(v.Y, v.Y, v.W, v.W);
	
	public static Vector4 YZXX(this Vector4 v) => new Vector4(v.Y, v.Z, v.X, v.X);
	
	public static Vector4 YZXY(this Vector4 v) => new Vector4(v.Y, v.Z, v.X, v.Y);
	
	public static Vector4 YZXZ(this Vector4 v) => new Vector4(v.Y, v.Z, v.X, v.Z);
	
	public static Vector4 YZXW(this Vector4 v) => new Vector4(v.Y, v.Z, v.X, v.W);
	
	public static Vector4 YZYX(this Vector4 v) => new Vector4(v.Y, v.Z, v.Y, v.X);
	
	public static Vector4 YZYY(this Vector4 v) => new Vector4(v.Y, v.Z, v.Y, v.Y);
	
	public static Vector4 YZYZ(this Vector4 v) => new Vector4(v.Y, v.Z, v.Y, v.Z);
	
	public static Vector4 YZYW(this Vector4 v) => new Vector4(v.Y, v.Z, v.Y, v.W);
	
	public static Vector4 YZZX(this Vector4 v) => new Vector4(v.Y, v.Z, v.Z, v.X);
	
	public static Vector4 YZZY(this Vector4 v) => new Vector4(v.Y, v.Z, v.Z, v.Y);
	
	public static Vector4 YZZZ(this Vector4 v) => new Vector4(v.Y, v.Z, v.Z, v.Z);
	
	public static Vector4 YZZW(this Vector4 v) => new Vector4(v.Y, v.Z, v.Z, v.W);
	
	public static Vector4 YZWX(this Vector4 v) => new Vector4(v.Y, v.Z, v.W, v.X);
	
	public static Vector4 YZWY(this Vector4 v) => new Vector4(v.Y, v.Z, v.W, v.Y);
	
	public static Vector4 YZWZ(this Vector4 v) => new Vector4(v.Y, v.Z, v.W, v.Z);
	
	public static Vector4 YZWW(this Vector4 v) => new Vector4(v.Y, v.Z, v.W, v.W);
	
	public static Vector4 YWXX(this Vector4 v) => new Vector4(v.Y, v.W, v.X, v.X);
	
	public static Vector4 YWXY(this Vector4 v) => new Vector4(v.Y, v.W, v.X, v.Y);
	
	public static Vector4 YWXZ(this Vector4 v) => new Vector4(v.Y, v.W, v.X, v.Z);
	
	public static Vector4 YWXW(this Vector4 v) => new Vector4(v.Y, v.W, v.X, v.W);
	
	public static Vector4 YWYX(this Vector4 v) => new Vector4(v.Y, v.W, v.Y, v.X);
	
	public static Vector4 YWYY(this Vector4 v) => new Vector4(v.Y, v.W, v.Y, v.Y);
	
	public static Vector4 YWYZ(this Vector4 v) => new Vector4(v.Y, v.W, v.Y, v.Z);
	
	public static Vector4 YWYW(this Vector4 v) => new Vector4(v.Y, v.W, v.Y, v.W);
	
	public static Vector4 YWZX(this Vector4 v) => new Vector4(v.Y, v.W, v.Z, v.X);
	
	public static Vector4 YWZY(this Vector4 v) => new Vector4(v.Y, v.W, v.Z, v.Y);
	
	public static Vector4 YWZZ(this Vector4 v) => new Vector4(v.Y, v.W, v.Z, v.Z);
	
	public static Vector4 YWZW(this Vector4 v) => new Vector4(v.Y, v.W, v.Z, v.W);
	
	public static Vector4 YWWX(this Vector4 v) => new Vector4(v.Y, v.W, v.W, v.X);
	
	public static Vector4 YWWY(this Vector4 v) => new Vector4(v.Y, v.W, v.W, v.Y);
	
	public static Vector4 YWWZ(this Vector4 v) => new Vector4(v.Y, v.W, v.W, v.Z);
	
	public static Vector4 YWWW(this Vector4 v) => new Vector4(v.Y, v.W, v.W, v.W);
	
	public static Vector4 ZXXX(this Vector4 v) => new Vector4(v.Z, v.X, v.X, v.X);
	
	public static Vector4 ZXXY(this Vector4 v) => new Vector4(v.Z, v.X, v.X, v.Y);
	
	public static Vector4 ZXXZ(this Vector4 v) => new Vector4(v.Z, v.X, v.X, v.Z);
	
	public static Vector4 ZXXW(this Vector4 v) => new Vector4(v.Z, v.X, v.X, v.W);
	
	public static Vector4 ZXYX(this Vector4 v) => new Vector4(v.Z, v.X, v.Y, v.X);
	
	public static Vector4 ZXYY(this Vector4 v) => new Vector4(v.Z, v.X, v.Y, v.Y);
	
	public static Vector4 ZXYZ(this Vector4 v) => new Vector4(v.Z, v.X, v.Y, v.Z);
	
	public static Vector4 ZXYW(this Vector4 v) => new Vector4(v.Z, v.X, v.Y, v.W);
	
	public static Vector4 ZXZX(this Vector4 v) => new Vector4(v.Z, v.X, v.Z, v.X);
	
	public static Vector4 ZXZY(this Vector4 v) => new Vector4(v.Z, v.X, v.Z, v.Y);
	
	public static Vector4 ZXZZ(this Vector4 v) => new Vector4(v.Z, v.X, v.Z, v.Z);
	
	public static Vector4 ZXZW(this Vector4 v) => new Vector4(v.Z, v.X, v.Z, v.W);
	
	public static Vector4 ZXWX(this Vector4 v) => new Vector4(v.Z, v.X, v.W, v.X);
	
	public static Vector4 ZXWY(this Vector4 v) => new Vector4(v.Z, v.X, v.W, v.Y);
	
	public static Vector4 ZXWZ(this Vector4 v) => new Vector4(v.Z, v.X, v.W, v.Z);
	
	public static Vector4 ZXWW(this Vector4 v) => new Vector4(v.Z, v.X, v.W, v.W);
	
	public static Vector4 ZYXX(this Vector4 v) => new Vector4(v.Z, v.Y, v.X, v.X);
	
	public static Vector4 ZYXY(this Vector4 v) => new Vector4(v.Z, v.Y, v.X, v.Y);
	
	public static Vector4 ZYXZ(this Vector4 v) => new Vector4(v.Z, v.Y, v.X, v.Z);
	
	public static Vector4 ZYXW(this Vector4 v) => new Vector4(v.Z, v.Y, v.X, v.W);
	
	public static Vector4 ZYYX(this Vector4 v) => new Vector4(v.Z, v.Y, v.Y, v.X);
	
	public static Vector4 ZYYY(this Vector4 v) => new Vector4(v.Z, v.Y, v.Y, v.Y);
	
	public static Vector4 ZYYZ(this Vector4 v) => new Vector4(v.Z, v.Y, v.Y, v.Z);
	
	public static Vector4 ZYYW(this Vector4 v) => new Vector4(v.Z, v.Y, v.Y, v.W);
	
	public static Vector4 ZYZX(this Vector4 v) => new Vector4(v.Z, v.Y, v.Z, v.X);
	
	public static Vector4 ZYZY(this Vector4 v) => new Vector4(v.Z, v.Y, v.Z, v.Y);
	
	public static Vector4 ZYZZ(this Vector4 v) => new Vector4(v.Z, v.Y, v.Z, v.Z);
	
	public static Vector4 ZYZW(this Vector4 v) => new Vector4(v.Z, v.Y, v.Z, v.W);
	
	public static Vector4 ZYWX(this Vector4 v) => new Vector4(v.Z, v.Y, v.W, v.X);
	
	public static Vector4 ZYWY(this Vector4 v) => new Vector4(v.Z, v.Y, v.W, v.Y);
	
	public static Vector4 ZYWZ(this Vector4 v) => new Vector4(v.Z, v.Y, v.W, v.Z);
	
	public static Vector4 ZYWW(this Vector4 v) => new Vector4(v.Z, v.Y, v.W, v.W);
	
	public static Vector4 ZZXX(this Vector4 v) => new Vector4(v.Z, v.Z, v.X, v.X);
	
	public static Vector4 ZZXY(this Vector4 v) => new Vector4(v.Z, v.Z, v.X, v.Y);
	
	public static Vector4 ZZXZ(this Vector4 v) => new Vector4(v.Z, v.Z, v.X, v.Z);
	
	public static Vector4 ZZXW(this Vector4 v) => new Vector4(v.Z, v.Z, v.X, v.W);
	
	public static Vector4 ZZYX(this Vector4 v) => new Vector4(v.Z, v.Z, v.Y, v.X);
	
	public static Vector4 ZZYY(this Vector4 v) => new Vector4(v.Z, v.Z, v.Y, v.Y);
	
	public static Vector4 ZZYZ(this Vector4 v) => new Vector4(v.Z, v.Z, v.Y, v.Z);
	
	public static Vector4 ZZYW(this Vector4 v) => new Vector4(v.Z, v.Z, v.Y, v.W);
	
	public static Vector4 ZZZX(this Vector4 v) => new Vector4(v.Z, v.Z, v.Z, v.X);
	
	public static Vector4 ZZZY(this Vector4 v) => new Vector4(v.Z, v.Z, v.Z, v.Y);
	
	public static Vector4 ZZZZ(this Vector4 v) => new Vector4(v.Z, v.Z, v.Z, v.Z);
	
	public static Vector4 ZZZW(this Vector4 v) => new Vector4(v.Z, v.Z, v.Z, v.W);
	
	public static Vector4 ZZWX(this Vector4 v) => new Vector4(v.Z, v.Z, v.W, v.X);
	
	public static Vector4 ZZWY(this Vector4 v) => new Vector4(v.Z, v.Z, v.W, v.Y);
	
	public static Vector4 ZZWZ(this Vector4 v) => new Vector4(v.Z, v.Z, v.W, v.Z);
	
	public static Vector4 ZZWW(this Vector4 v) => new Vector4(v.Z, v.Z, v.W, v.W);
	
	public static Vector4 ZWXX(this Vector4 v) => new Vector4(v.Z, v.W, v.X, v.X);
	
	public static Vector4 ZWXY(this Vector4 v) => new Vector4(v.Z, v.W, v.X, v.Y);
	
	public static Vector4 ZWXZ(this Vector4 v) => new Vector4(v.Z, v.W, v.X, v.Z);
	
	public static Vector4 ZWXW(this Vector4 v) => new Vector4(v.Z, v.W, v.X, v.W);
	
	public static Vector4 ZWYX(this Vector4 v) => new Vector4(v.Z, v.W, v.Y, v.X);
	
	public static Vector4 ZWYY(this Vector4 v) => new Vector4(v.Z, v.W, v.Y, v.Y);
	
	public static Vector4 ZWYZ(this Vector4 v) => new Vector4(v.Z, v.W, v.Y, v.Z);
	
	public static Vector4 ZWYW(this Vector4 v) => new Vector4(v.Z, v.W, v.Y, v.W);
	
	public static Vector4 ZWZX(this Vector4 v) => new Vector4(v.Z, v.W, v.Z, v.X);
	
	public static Vector4 ZWZY(this Vector4 v) => new Vector4(v.Z, v.W, v.Z, v.Y);
	
	public static Vector4 ZWZZ(this Vector4 v) => new Vector4(v.Z, v.W, v.Z, v.Z);
	
	public static Vector4 ZWZW(this Vector4 v) => new Vector4(v.Z, v.W, v.Z, v.W);
	
	public static Vector4 ZWWX(this Vector4 v) => new Vector4(v.Z, v.W, v.W, v.X);
	
	public static Vector4 ZWWY(this Vector4 v) => new Vector4(v.Z, v.W, v.W, v.Y);
	
	public static Vector4 ZWWZ(this Vector4 v) => new Vector4(v.Z, v.W, v.W, v.Z);
	
	public static Vector4 ZWWW(this Vector4 v) => new Vector4(v.Z, v.W, v.W, v.W);
	
	public static Vector4 WXXX(this Vector4 v) => new Vector4(v.W, v.X, v.X, v.X);
	
	public static Vector4 WXXY(this Vector4 v) => new Vector4(v.W, v.X, v.X, v.Y);
	
	public static Vector4 WXXZ(this Vector4 v) => new Vector4(v.W, v.X, v.X, v.Z);
	
	public static Vector4 WXXW(this Vector4 v) => new Vector4(v.W, v.X, v.X, v.W);
	
	public static Vector4 WXYX(this Vector4 v) => new Vector4(v.W, v.X, v.Y, v.X);
	
	public static Vector4 WXYY(this Vector4 v) => new Vector4(v.W, v.X, v.Y, v.Y);
	
	public static Vector4 WXYZ(this Vector4 v) => new Vector4(v.W, v.X, v.Y, v.Z);
	
	public static Vector4 WXYW(this Vector4 v) => new Vector4(v.W, v.X, v.Y, v.W);
	
	public static Vector4 WXZX(this Vector4 v) => new Vector4(v.W, v.X, v.Z, v.X);
	
	public static Vector4 WXZY(this Vector4 v) => new Vector4(v.W, v.X, v.Z, v.Y);
	
	public static Vector4 WXZZ(this Vector4 v) => new Vector4(v.W, v.X, v.Z, v.Z);
	
	public static Vector4 WXZW(this Vector4 v) => new Vector4(v.W, v.X, v.Z, v.W);
	
	public static Vector4 WXWX(this Vector4 v) => new Vector4(v.W, v.X, v.W, v.X);
	
	public static Vector4 WXWY(this Vector4 v) => new Vector4(v.W, v.X, v.W, v.Y);
	
	public static Vector4 WXWZ(this Vector4 v) => new Vector4(v.W, v.X, v.W, v.Z);
	
	public static Vector4 WXWW(this Vector4 v) => new Vector4(v.W, v.X, v.W, v.W);
	
	public static Vector4 WYXX(this Vector4 v) => new Vector4(v.W, v.Y, v.X, v.X);
	
	public static Vector4 WYXY(this Vector4 v) => new Vector4(v.W, v.Y, v.X, v.Y);
	
	public static Vector4 WYXZ(this Vector4 v) => new Vector4(v.W, v.Y, v.X, v.Z);
	
	public static Vector4 WYXW(this Vector4 v) => new Vector4(v.W, v.Y, v.X, v.W);
	
	public static Vector4 WYYX(this Vector4 v) => new Vector4(v.W, v.Y, v.Y, v.X);
	
	public static Vector4 WYYY(this Vector4 v) => new Vector4(v.W, v.Y, v.Y, v.Y);
	
	public static Vector4 WYYZ(this Vector4 v) => new Vector4(v.W, v.Y, v.Y, v.Z);
	
	public static Vector4 WYYW(this Vector4 v) => new Vector4(v.W, v.Y, v.Y, v.W);
	
	public static Vector4 WYZX(this Vector4 v) => new Vector4(v.W, v.Y, v.Z, v.X);
	
	public static Vector4 WYZY(this Vector4 v) => new Vector4(v.W, v.Y, v.Z, v.Y);
	
	public static Vector4 WYZZ(this Vector4 v) => new Vector4(v.W, v.Y, v.Z, v.Z);
	
	public static Vector4 WYZW(this Vector4 v) => new Vector4(v.W, v.Y, v.Z, v.W);
	
	public static Vector4 WYWX(this Vector4 v) => new Vector4(v.W, v.Y, v.W, v.X);
	
	public static Vector4 WYWY(this Vector4 v) => new Vector4(v.W, v.Y, v.W, v.Y);
	
	public static Vector4 WYWZ(this Vector4 v) => new Vector4(v.W, v.Y, v.W, v.Z);
	
	public static Vector4 WYWW(this Vector4 v) => new Vector4(v.W, v.Y, v.W, v.W);
	
	public static Vector4 WZXX(this Vector4 v) => new Vector4(v.W, v.Z, v.X, v.X);
	
	public static Vector4 WZXY(this Vector4 v) => new Vector4(v.W, v.Z, v.X, v.Y);
	
	public static Vector4 WZXZ(this Vector4 v) => new Vector4(v.W, v.Z, v.X, v.Z);
	
	public static Vector4 WZXW(this Vector4 v) => new Vector4(v.W, v.Z, v.X, v.W);
	
	public static Vector4 WZYX(this Vector4 v) => new Vector4(v.W, v.Z, v.Y, v.X);
	
	public static Vector4 WZYY(this Vector4 v) => new Vector4(v.W, v.Z, v.Y, v.Y);
	
	public static Vector4 WZYZ(this Vector4 v) => new Vector4(v.W, v.Z, v.Y, v.Z);
	
	public static Vector4 WZYW(this Vector4 v) => new Vector4(v.W, v.Z, v.Y, v.W);
	
	public static Vector4 WZZX(this Vector4 v) => new Vector4(v.W, v.Z, v.Z, v.X);
	
	public static Vector4 WZZY(this Vector4 v) => new Vector4(v.W, v.Z, v.Z, v.Y);
	
	public static Vector4 WZZZ(this Vector4 v) => new Vector4(v.W, v.Z, v.Z, v.Z);
	
	public static Vector4 WZZW(this Vector4 v) => new Vector4(v.W, v.Z, v.Z, v.W);
	
	public static Vector4 WZWX(this Vector4 v) => new Vector4(v.W, v.Z, v.W, v.X);
	
	public static Vector4 WZWY(this Vector4 v) => new Vector4(v.W, v.Z, v.W, v.Y);
	
	public static Vector4 WZWZ(this Vector4 v) => new Vector4(v.W, v.Z, v.W, v.Z);
	
	public static Vector4 WZWW(this Vector4 v) => new Vector4(v.W, v.Z, v.W, v.W);
	
	public static Vector4 WWXX(this Vector4 v) => new Vector4(v.W, v.W, v.X, v.X);
	
	public static Vector4 WWXY(this Vector4 v) => new Vector4(v.W, v.W, v.X, v.Y);
	
	public static Vector4 WWXZ(this Vector4 v) => new Vector4(v.W, v.W, v.X, v.Z);
	
	public static Vector4 WWXW(this Vector4 v) => new Vector4(v.W, v.W, v.X, v.W);
	
	public static Vector4 WWYX(this Vector4 v) => new Vector4(v.W, v.W, v.Y, v.X);
	
	public static Vector4 WWYY(this Vector4 v) => new Vector4(v.W, v.W, v.Y, v.Y);
	
	public static Vector4 WWYZ(this Vector4 v) => new Vector4(v.W, v.W, v.Y, v.Z);
	
	public static Vector4 WWYW(this Vector4 v) => new Vector4(v.W, v.W, v.Y, v.W);
	
	public static Vector4 WWZX(this Vector4 v) => new Vector4(v.W, v.W, v.Z, v.X);
	
	public static Vector4 WWZY(this Vector4 v) => new Vector4(v.W, v.W, v.Z, v.Y);
	
	public static Vector4 WWZZ(this Vector4 v) => new Vector4(v.W, v.W, v.Z, v.Z);
	
	public static Vector4 WWZW(this Vector4 v) => new Vector4(v.W, v.W, v.Z, v.W);
	
	public static Vector4 WWWX(this Vector4 v) => new Vector4(v.W, v.W, v.W, v.X);
	
	public static Vector4 WWWY(this Vector4 v) => new Vector4(v.W, v.W, v.W, v.Y);
	
	public static Vector4 WWWZ(this Vector4 v) => new Vector4(v.W, v.W, v.W, v.Z);
	
	public static Vector4 WWWW(this Vector4 v) => new Vector4(v.W, v.W, v.W, v.W);
	

		
	}
}
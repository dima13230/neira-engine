using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using Gl = OpenTK.Graphics.OpenGL;
using GL = OpenTK.Graphics.OpenGL.GL;

using NeiraEngine.Render.OpenGL;

namespace NeiraEngine.Render
{
    public enum TextureTarget
    {
        Texture1D = 3552,
        Texture2D = 3553,
        ProxyTexture1D = 32867,
        ProxyTexture1DExt = 32867,
        ProxyTexture2D = 32868,
        ProxyTexture2DExt = 32868,
        Texture3D = 32879,
        Texture3DExt = 32879,
        Texture3DOes = 32879,
        ProxyTexture3D = 32880,
        ProxyTexture3DExt = 32880,
        DetailTexture2DSgis = 32917,
        Texture4DSgis = 33076,

        ProxyTexture4DSgis = 33077,
        TextureMinLod = 33082,
        TextureMinLodSgis = 33082,
        TextureMaxLod = 33083,
        TextureMaxLodSgis = 33083,
        TextureBaseLevel = 33084,
        TextureBaseLevelSgis = 33084,
        TextureMaxLevel = 33085,
        TextureMaxLevelSgis = 33085,
        TextureRectangle = 34037,
        TextureRectangleArb = 34037,
        TextureRectangleNv = 34037,
        ProxyTextureRectangle = 34039,
        TextureCubeMap = 34067,
        TextureBindingCubeMap = 34068,
        TextureCubeMapPositiveX = 34069,
        TextureCubeMapNegativeX = 34070,
        TextureCubeMapPositiveY = 34071,
        TextureCubeMapNegativeY = 34072,
        TextureCubeMapPositiveZ = 34073,
        TextureCubeMapNegativeZ = 34074,
        ProxyTextureCubeMap = 34075,
        Texture1DArray = 35864,
        ProxyTexture1DArray = 35865,
        Texture2DArray = 35866,
        ProxyTexture2DArray = 35867,
        TextureBuffer = 35882,
        TextureCubeMapArray = 36873,
        ProxyTextureCubeMapArray = 36875,
        Texture2DMultisample = 37120,
        ProxyTexture2DMultisample = 37121,
        Texture2DMultisampleArray = 37122,
        ProxyTexture2DMultisampleArray = 37123
    }
    public enum TextureWrapMode
    {
        Clamp = 10496,
        Repeat = 10497,
        ClampToBorder = 33069,
        ClampToBorderArb = 33069,
        ClampToBorderNv = 33069,
        ClampToBorderSgis = 33069,
        ClampToEdge = 33071,
        ClampToEdgeSgis = 33071,
        MirroredRepeat = 33648
    }
    public enum PixelInternalFormat
    {
        //
        // Сводка:
        //     Original was GL_ONE = 1
        One = 1,
        //
        // Сводка:
        //     Original was GL_TWO = 2
        Two = 2,
        //
        // Сводка:
        //     Original was GL_THREE = 3
        Three = 3,
        //
        // Сводка:
        //     Original was GL_FOUR = 4
        Four = 4,
        //
        // Сводка:
        //     Original was GL_DEPTH_COMPONENT = 0x1902
        DepthComponent = 6402,
        //
        // Сводка:
        //     Original was GL_ALPHA = 0x1906
        Alpha = 6406,
        //
        // Сводка:
        //     Original was GL_RGB = 0x1907
        Rgb = 6407,
        //
        // Сводка:
        //     Original was GL_RGBA = 0x1908
        Rgba = 6408,
        //
        // Сводка:
        //     Original was GL_LUMINANCE = 0x1909
        Luminance = 6409,
        //
        // Сводка:
        //     Original was GL_LUMINANCE_ALPHA = 0x190A
        LuminanceAlpha = 6410,
        //
        // Сводка:
        //     Original was GL_R3_G3_B2 = 0x2A10
        R3G3B2 = 10768,
        //
        // Сводка:
        //     Original was GL_ALPHA4 = 0x803B
        Alpha4 = 32827,
        //
        // Сводка:
        //     Original was GL_ALPHA8 = 0x803C
        Alpha8 = 32828,
        //
        // Сводка:
        //     Original was GL_ALPHA12 = 0x803D
        Alpha12 = 32829,
        //
        // Сводка:
        //     Original was GL_ALPHA16 = 0x803E
        Alpha16 = 32830,
        //
        // Сводка:
        //     Original was GL_LUMINANCE4 = 0x803F
        Luminance4 = 32831,
        //
        // Сводка:
        //     Original was GL_LUMINANCE8 = 0x8040
        Luminance8 = 32832,
        //
        // Сводка:
        //     Original was GL_LUMINANCE12 = 0x8041
        Luminance12 = 32833,
        //
        // Сводка:
        //     Original was GL_LUMINANCE16 = 0x8042
        Luminance16 = 32834,
        //
        // Сводка:
        //     Original was GL_LUMINANCE4_ALPHA4 = 0x8043
        Luminance4Alpha4 = 32835,
        //
        // Сводка:
        //     Original was GL_LUMINANCE6_ALPHA2 = 0x8044
        Luminance6Alpha2 = 32836,
        //
        // Сводка:
        //     Original was GL_LUMINANCE8_ALPHA8 = 0x8045
        Luminance8Alpha8 = 32837,
        //
        // Сводка:
        //     Original was GL_LUMINANCE12_ALPHA4 = 0x8046
        Luminance12Alpha4 = 32838,
        //
        // Сводка:
        //     Original was GL_LUMINANCE12_ALPHA12 = 0x8047
        Luminance12Alpha12 = 32839,
        //
        // Сводка:
        //     Original was GL_LUMINANCE16_ALPHA16 = 0x8048
        Luminance16Alpha16 = 32840,
        //
        // Сводка:
        //     Original was GL_INTENSITY = 0x8049
        Intensity = 32841,
        //
        // Сводка:
        //     Original was GL_INTENSITY4 = 0x804A
        Intensity4 = 32842,
        //
        // Сводка:
        //     Original was GL_INTENSITY8 = 0x804B
        Intensity8 = 32843,
        //
        // Сводка:
        //     Original was GL_INTENSITY12 = 0x804C
        Intensity12 = 32844,
        //
        // Сводка:
        //     Original was GL_INTENSITY16 = 0x804D
        Intensity16 = 32845,
        //
        // Сводка:
        //     Original was GL_RGB2_EXT = 0x804E
        Rgb2Ext = 32846,
        //
        // Сводка:
        //     Original was GL_RGB4 = 0x804F
        Rgb4 = 32847,
        //
        // Сводка:
        //     Original was GL_RGB5 = 0x8050
        Rgb5 = 32848,
        //
        // Сводка:
        //     Original was GL_RGB8 = 0x8051
        Rgb8 = 32849,
        //
        // Сводка:
        //     Original was GL_RGB10 = 0x8052
        Rgb10 = 32850,
        //
        // Сводка:
        //     Original was GL_RGB12 = 0x8053
        Rgb12 = 32851,
        //
        // Сводка:
        //     Original was GL_RGB16 = 0x8054
        Rgb16 = 32852,
        //
        // Сводка:
        //     Original was GL_RGBA2 = 0x8055
        Rgba2 = 32853,
        //
        // Сводка:
        //     Original was GL_RGBA4 = 0x8056
        Rgba4 = 32854,
        //
        // Сводка:
        //     Original was GL_RGB5_A1 = 0x8057
        Rgb5A1 = 32855,
        //
        // Сводка:
        //     Original was GL_RGBA8 = 0x8058
        Rgba8 = 32856,
        //
        // Сводка:
        //     Original was GL_RGB10_A2 = 0x8059
        Rgb10A2 = 32857,
        //
        // Сводка:
        //     Original was GL_RGBA12 = 0x805A
        Rgba12 = 32858,
        //
        // Сводка:
        //     Original was GL_RGBA16 = 0x805B
        Rgba16 = 32859,
        //
        // Сводка:
        //     Original was GL_DUAL_ALPHA4_SGIS = 0x8110
        DualAlpha4Sgis = 33040,
        //
        // Сводка:
        //     Original was GL_DUAL_ALPHA8_SGIS = 0x8111
        DualAlpha8Sgis = 33041,
        //
        // Сводка:
        //     Original was GL_DUAL_ALPHA12_SGIS = 0x8112
        DualAlpha12Sgis = 33042,
        //
        // Сводка:
        //     Original was GL_DUAL_ALPHA16_SGIS = 0x8113
        DualAlpha16Sgis = 33043,
        //
        // Сводка:
        //     Original was GL_DUAL_LUMINANCE4_SGIS = 0x8114
        DualLuminance4Sgis = 33044,
        //
        // Сводка:
        //     Original was GL_DUAL_LUMINANCE8_SGIS = 0x8115
        DualLuminance8Sgis = 33045,
        //
        // Сводка:
        //     Original was GL_DUAL_LUMINANCE12_SGIS = 0x8116
        DualLuminance12Sgis = 33046,
        //
        // Сводка:
        //     Original was GL_DUAL_LUMINANCE16_SGIS = 0x8117
        DualLuminance16Sgis = 33047,
        //
        // Сводка:
        //     Original was GL_DUAL_INTENSITY4_SGIS = 0x8118
        DualIntensity4Sgis = 33048,
        //
        // Сводка:
        //     Original was GL_DUAL_INTENSITY8_SGIS = 0x8119
        DualIntensity8Sgis = 33049,
        //
        // Сводка:
        //     Original was GL_DUAL_INTENSITY12_SGIS = 0x811A
        DualIntensity12Sgis = 33050,
        //
        // Сводка:
        //     Original was GL_DUAL_INTENSITY16_SGIS = 0x811B
        DualIntensity16Sgis = 33051,
        //
        // Сводка:
        //     Original was GL_DUAL_LUMINANCE_ALPHA4_SGIS = 0x811C
        DualLuminanceAlpha4Sgis = 33052,
        //
        // Сводка:
        //     Original was GL_DUAL_LUMINANCE_ALPHA8_SGIS = 0x811D
        DualLuminanceAlpha8Sgis = 33053,
        //
        // Сводка:
        //     Original was GL_QUAD_ALPHA4_SGIS = 0x811E
        QuadAlpha4Sgis = 33054,
        //
        // Сводка:
        //     Original was GL_QUAD_ALPHA8_SGIS = 0x811F
        QuadAlpha8Sgis = 33055,
        //
        // Сводка:
        //     Original was GL_QUAD_LUMINANCE4_SGIS = 0x8120
        QuadLuminance4Sgis = 33056,
        //
        // Сводка:
        //     Original was GL_QUAD_LUMINANCE8_SGIS = 0x8121
        QuadLuminance8Sgis = 33057,
        //
        // Сводка:
        //     Original was GL_QUAD_INTENSITY4_SGIS = 0x8122
        QuadIntensity4Sgis = 33058,
        //
        // Сводка:
        //     Original was GL_QUAD_INTENSITY8_SGIS = 0x8123
        QuadIntensity8Sgis = 33059,
        //
        // Сводка:
        //     Original was GL_DEPTH_COMPONENT16 = 0x81a5
        DepthComponent16 = 33189,
        //
        // Сводка:
        //     Original was GL_DEPTH_COMPONENT16_SGIX = 0x81A5
        DepthComponent16Sgix = 33189,
        //
        // Сводка:
        //     Original was GL_DEPTH_COMPONENT24 = 0x81a6
        DepthComponent24 = 33190,
        //
        // Сводка:
        //     Original was GL_DEPTH_COMPONENT24_SGIX = 0x81A6
        DepthComponent24Sgix = 33190,
        //
        // Сводка:
        //     Original was GL_DEPTH_COMPONENT32 = 0x81a7
        DepthComponent32 = 33191,
        //
        // Сводка:
        //     Original was GL_DEPTH_COMPONENT32_SGIX = 0x81A7
        DepthComponent32Sgix = 33191,
        //
        // Сводка:
        //     Original was GL_COMPRESSED_RED = 0x8225
        CompressedRed = 33317,
        //
        // Сводка:
        //     Original was GL_COMPRESSED_RG = 0x8226
        CompressedRg = 33318,
        //
        // Сводка:
        //     Original was GL_R8 = 0x8229
        R8 = 33321,
        //
        // Сводка:
        //     Original was GL_R16 = 0x822A
        R16 = 33322,
        //
        // Сводка:
        //     Original was GL_RG8 = 0x822B
        Rg8 = 33323,
        //
        // Сводка:
        //     Original was GL_RG16 = 0x822C
        Rg16 = 33324,
        //
        // Сводка:
        //     Original was GL_R16F = 0x822D
        R16f = 33325,
        //
        // Сводка:
        //     Original was GL_R32F = 0x822E
        R32f = 33326,
        //
        // Сводка:
        //     Original was GL_RG16F = 0x822F
        Rg16f = 33327,
        //
        // Сводка:
        //     Original was GL_RG32F = 0x8230
        Rg32f = 33328,
        //
        // Сводка:
        //     Original was GL_R8I = 0x8231
        R8i = 33329,
        //
        // Сводка:
        //     Original was GL_R8UI = 0x8232
        R8ui = 33330,
        //
        // Сводка:
        //     Original was GL_R16I = 0x8233
        R16i = 33331,
        //
        // Сводка:
        //     Original was GL_R16UI = 0x8234
        R16ui = 33332,
        //
        // Сводка:
        //     Original was GL_R32I = 0x8235
        R32i = 33333,
        //
        // Сводка:
        //     Original was GL_R32UI = 0x8236
        R32ui = 33334,
        //
        // Сводка:
        //     Original was GL_RG8I = 0x8237
        Rg8i = 33335,
        //
        // Сводка:
        //     Original was GL_RG8UI = 0x8238
        Rg8ui = 33336,
        //
        // Сводка:
        //     Original was GL_RG16I = 0x8239
        Rg16i = 33337,
        //
        // Сводка:
        //     Original was GL_RG16UI = 0x823A
        Rg16ui = 33338,
        //
        // Сводка:
        //     Original was GL_RG32I = 0x823B
        Rg32i = 33339,
        Rg32ui = 33340,
        CompressedRgbS3tcDxt1Ext = 33776,
        CompressedRgbaS3tcDxt1Ext = 33777,
        CompressedRgbaS3tcDxt3Ext = 33778,
        CompressedRgbaS3tcDxt5Ext = 33779,
        RgbIccSgix = 33888,
        RgbaIccSgix = 33889,
        AlphaIccSgix = 33890,
        LuminanceIccSgix = 33891,
        IntensityIccSgix = 33892,
        LuminanceAlphaIccSgix = 33893,
        R5G6B5IccSgix = 33894,
        R5G6B5A8IccSgix = 33895,
        Alpha16IccSgix = 33896,
        Luminance16IccSgix = 33897,
        Intensity16IccSgix = 33898,
        Luminance16Alpha8IccSgix = 33899,
        CompressedAlpha = 34025,
        CompressedLuminance = 34026,
        CompressedLuminanceAlpha = 34027,
        CompressedIntensity = 34028,
        CompressedRgb = 34029,
        CompressedRgba = 34030,
        DepthStencil = 34041,
        Rgba32f = 34836,
        Rgb32f = 34837,
        Rgba16f = 34842,
        Rgb16f = 34843,
        Depth24Stencil8 = 35056,
        R11fG11fB10f = 35898,
        Rgb9E5 = 35901,
        Srgb = 35904,
        Srgb8 = 35905,
        SrgbAlpha = 35906,
        Srgb8Alpha8 = 35907,
        SluminanceAlpha = 35908,
        Sluminance8Alpha8 = 35909,
        Sluminance = 35910,
        Sluminance8 = 35911,
        CompressedSrgb = 35912,
        CompressedSrgbAlpha = 35913,
        CompressedSluminance = 35914,
        CompressedSluminanceAlpha = 35915,
        CompressedSrgbS3tcDxt1Ext = 35916,
        CompressedSrgbAlphaS3tcDxt1Ext = 35917,
        CompressedSrgbAlphaS3tcDxt3Ext = 35918,
        CompressedSrgbAlphaS3tcDxt5Ext = 35919,
        DepthComponent32f = 36012,
        Depth32fStencil8 = 36013,
        Rgba32ui = 36208,
        Rgb32ui = 36209,
        Rgba16ui = 36214,
        Rgb16ui = 36215,
        Rgba8ui = 36220,
        Rgb8ui = 36221,
        Rgba32i = 36226,
        Rgb32i = 36227,
        Rgba16i = 36232,
        Rgb16i = 36233,
        Rgba8i = 36238,
        Rgb8i = 36239,
        Float32UnsignedInt248Rev = 36269,
        CompressedRedRgtc1 = 36283,
        CompressedSignedRedRgtc1 = 36284,
        CompressedRgRgtc2 = 36285,
        CompressedSignedRgRgtc2 = 36286,
        CompressedRgbaBptcUnorm = 36492,
        CompressedSrgbAlphaBptcUnorm = 36493,
        CompressedRgbBptcSignedFloat = 36494,
        CompressedRgbBptcUnsignedFloat = 36495,
        R8Snorm = 36756,
        Rg8Snorm = 36757,
        Rgb8Snorm = 36758,
        Rgba8Snorm = 36759,
        R16Snorm = 36760,
        Rg16Snorm = 36761,
        Rgb16Snorm = 36762,
        Rgba16Snorm = 36763,
        Rgb10A2ui = 36975
    }
    public enum PixelFormat
    {
        //
        // Сводка:
        //     Original was GL_UNSIGNED_SHORT = 0x1403
        UnsignedShort = 5123,
        //
        // Сводка:
        //     Original was GL_UNSIGNED_INT = 0x1405
        UnsignedInt = 5125,
        //
        // Сводка:
        //     Original was GL_COLOR_INDEX = 0x1900
        ColorIndex = 6400,
        //
        // Сводка:
        //     Original was GL_STENCIL_INDEX = 0x1901
        StencilIndex = 6401,
        //
        // Сводка:
        //     Original was GL_DEPTH_COMPONENT = 0x1902
        DepthComponent = 6402,
        //
        // Сводка:
        //     Original was GL_RED = 0x1903
        Red = 6403,
        //
        // Сводка:
        //     Original was GL_RED_EXT = 0x1903
        RedExt = 6403,
        //
        // Сводка:
        //     Original was GL_GREEN = 0x1904
        Green = 6404,
        //
        // Сводка:
        //     Original was GL_BLUE = 0x1905
        Blue = 6405,
        //
        // Сводка:
        //     Original was GL_ALPHA = 0x1906
        Alpha = 6406,
        //
        // Сводка:
        //     Original was GL_RGB = 0x1907
        Rgb = 6407,
        //
        // Сводка:
        //     Original was GL_RGBA = 0x1908
        Rgba = 6408,
        //
        // Сводка:
        //     Original was GL_LUMINANCE = 0x1909
        Luminance = 6409,
        //
        // Сводка:
        //     Original was GL_LUMINANCE_ALPHA = 0x190A
        LuminanceAlpha = 6410,
        //
        // Сводка:
        //     Original was GL_ABGR_EXT = 0x8000
        AbgrExt = 32768,
        //
        // Сводка:
        //     Original was GL_CMYK_EXT = 0x800C
        CmykExt = 32780,
        //
        // Сводка:
        //     Original was GL_CMYKA_EXT = 0x800D
        CmykaExt = 32781,
        //
        // Сводка:
        //     Original was GL_BGR = 0x80E0
        Bgr = 32992,
        //
        // Сводка:
        //     Original was GL_BGRA = 0x80E1
        Bgra = 32993,
        //
        // Сводка:
        //     Original was GL_YCRCB_422_SGIX = 0x81BB
        Ycrcb422Sgix = 33211,
        //
        // Сводка:
        //     Original was GL_YCRCB_444_SGIX = 0x81BC
        Ycrcb444Sgix = 33212,
        //
        // Сводка:
        //     Original was GL_RG = 0x8227
        Rg = 33319,
        //
        // Сводка:
        //     Original was GL_RG_INTEGER = 0x8228
        RgInteger = 33320,
        //
        // Сводка:
        //     Original was GL_R5_G6_B5_ICC_SGIX = 0x8466
        R5G6B5IccSgix = 33894,
        //
        // Сводка:
        //     Original was GL_R5_G6_B5_A8_ICC_SGIX = 0x8467
        R5G6B5A8IccSgix = 33895,
        //
        // Сводка:
        //     Original was GL_ALPHA16_ICC_SGIX = 0x8468
        Alpha16IccSgix = 33896,
        //
        // Сводка:
        //     Original was GL_LUMINANCE16_ICC_SGIX = 0x8469
        Luminance16IccSgix = 33897,
        //
        // Сводка:
        //     Original was GL_LUMINANCE16_ALPHA8_ICC_SGIX = 0x846B
        Luminance16Alpha8IccSgix = 33899,
        //
        // Сводка:
        //     Original was GL_DEPTH_STENCIL = 0x84F9
        DepthStencil = 34041,
        //
        // Сводка:
        //     Original was GL_RED_INTEGER = 0x8D94
        RedInteger = 36244,
        //
        // Сводка:
        //     Original was GL_GREEN_INTEGER = 0x8D95
        GreenInteger = 36245,
        //
        // Сводка:
        //     Original was GL_BLUE_INTEGER = 0x8D96
        BlueInteger = 36246,
        //
        // Сводка:
        //     Original was GL_ALPHA_INTEGER = 0x8D97
        AlphaInteger = 36247,
        //
        // Сводка:
        //     Original was GL_RGB_INTEGER = 0x8D98
        RgbInteger = 36248,
        //
        // Сводка:
        //     Original was GL_RGBA_INTEGER = 0x8D99
        RgbaInteger = 36249,
        //
        // Сводка:
        //     Original was GL_BGR_INTEGER = 0x8D9A
        BgrInteger = 36250,
        //
        // Сводка:
        //     Original was GL_BGRA_INTEGER = 0x8D9B
        BgraInteger = 36251
    }
    public enum PixelType
    {
        //
        // Сводка:
        //     Original was GL_BYTE = 0x1400
        Byte = 5120,
        //
        // Сводка:
        //     Original was GL_UNSIGNED_BYTE = 0x1401
        UnsignedByte = 5121,
        //
        // Сводка:
        //     Original was GL_SHORT = 0x1402
        Short = 5122,
        //
        // Сводка:
        //     Original was GL_UNSIGNED_SHORT = 0x1403
        UnsignedShort = 5123,
        //
        // Сводка:
        //     Original was GL_INT = 0x1404
        Int = 5124,
        //
        // Сводка:
        //     Original was GL_UNSIGNED_INT = 0x1405
        UnsignedInt = 5125,
        //
        // Сводка:
        //     Original was GL_FLOAT = 0x1406
        Float = 5126,
        //
        // Сводка:
        //     Original was GL_HALF_FLOAT = 0x140B
        HalfFloat = 5131,
        //
        // Сводка:
        //     Original was GL_BITMAP = 0x1A00
        Bitmap = 6656,
        //
        // Сводка:
        //     Original was GL_UNSIGNED_BYTE_3_3_2 = 0x8032
        UnsignedByte332 = 32818,
        //
        // Сводка:
        //     Original was GL_UNSIGNED_BYTE_3_3_2_EXT = 0x8032
        UnsignedByte332Ext = 32818,
        //
        // Сводка:
        //     Original was GL_UNSIGNED_SHORT_4_4_4_4 = 0x8033
        UnsignedShort4444 = 32819,
        //
        // Сводка:
        //     Original was GL_UNSIGNED_SHORT_4_4_4_4_EXT = 0x8033
        UnsignedShort4444Ext = 32819,
        //
        // Сводка:
        //     Original was GL_UNSIGNED_SHORT_5_5_5_1 = 0x8034
        UnsignedShort5551 = 32820,
        //
        // Сводка:
        //     Original was GL_UNSIGNED_SHORT_5_5_5_1_EXT = 0x8034
        UnsignedShort5551Ext = 32820,
        //
        // Сводка:
        //     Original was GL_UNSIGNED_INT_8_8_8_8 = 0x8035
        UnsignedInt8888 = 32821,
        //
        // Сводка:
        //     Original was GL_UNSIGNED_INT_8_8_8_8_EXT = 0x8035
        UnsignedInt8888Ext = 32821,
        //
        // Сводка:
        //     Original was GL_UNSIGNED_INT_10_10_10_2 = 0x8036
        UnsignedInt1010102 = 32822,
        //
        // Сводка:
        //     Original was GL_UNSIGNED_INT_10_10_10_2_EXT = 0x8036
        UnsignedInt1010102Ext = 32822,
        //
        // Сводка:
        //     Original was GL_UNSIGNED_BYTE_2_3_3_REVERSED = 0x8362
        UnsignedByte233Reversed = 33634,
        //
        // Сводка:
        //     Original was GL_UNSIGNED_SHORT_5_6_5 = 0x8363
        UnsignedShort565 = 33635,
        //
        // Сводка:
        //     Original was GL_UNSIGNED_SHORT_5_6_5_REVERSED = 0x8364
        UnsignedShort565Reversed = 33636,
        //
        // Сводка:
        //     Original was GL_UNSIGNED_SHORT_4_4_4_4_REVERSED = 0x8365
        UnsignedShort4444Reversed = 33637,
        //
        // Сводка:
        //     Original was GL_UNSIGNED_SHORT_1_5_5_5_REVERSED = 0x8366
        UnsignedShort1555Reversed = 33638,
        //
        // Сводка:
        //     Original was GL_UNSIGNED_INT_8_8_8_8_REVERSED = 0x8367
        UnsignedInt8888Reversed = 33639,
        //
        // Сводка:
        //     Original was GL_UNSIGNED_INT_2_10_10_10_REVERSED = 0x8368
        UnsignedInt2101010Reversed = 33640,
        //
        // Сводка:
        //     Original was GL_UNSIGNED_INT_24_8 = 0x84FA
        UnsignedInt248 = 34042,
        //
        // Сводка:
        //     Original was GL_UNSIGNED_INT_10F_11F_11F_REV = 0x8C3B
        UnsignedInt10F11F11FRev = 35899,
        //
        // Сводка:
        //     Original was GL_UNSIGNED_INT_5_9_9_9_REV = 0x8C3E
        UnsignedInt5999Rev = 35902,
        //
        // Сводка:
        //     Original was GL_FLOAT_32_UNSIGNED_INT_24_8_REV = 0x8DAD
        Float32UnsignedInt248Rev = 36269
    }
    public enum TextureMinFilter
    {
        //
        // Сводка:
        //     Original was GL_NEAREST = 0x2600
        Nearest = 9728,
        //
        // Сводка:
        //     Original was GL_LINEAR = 0x2601
        Linear = 9729,
        //
        // Сводка:
        //     Original was GL_NEAREST_MIPMAP_NEAREST = 0x2700
        NearestMipmapNearest = 9984,
        //
        // Сводка:
        //     Original was GL_LINEAR_MIPMAP_NEAREST = 0x2701
        LinearMipmapNearest = 9985,
        //
        // Сводка:
        //     Original was GL_NEAREST_MIPMAP_LINEAR = 0x2702
        NearestMipmapLinear = 9986,
        //
        // Сводка:
        //     Original was GL_LINEAR_MIPMAP_LINEAR = 0x2703
        LinearMipmapLinear = 9987,
        //
        // Сводка:
        //     Original was GL_FILTER4_SGIS = 0x8146
        Filter4Sgis = 33094,
        //
        // Сводка:
        //     Original was GL_LINEAR_CLIPMAP_LINEAR_SGIX = 0x8170
        LinearClipmapLinearSgix = 33136,
        //
        // Сводка:
        //     Original was GL_PIXEL_TEX_GEN_Q_CEILING_SGIX = 0x8184
        PixelTexGenQCeilingSgix = 33156,
        //
        // Сводка:
        //     Original was GL_PIXEL_TEX_GEN_Q_ROUND_SGIX = 0x8185
        PixelTexGenQRoundSgix = 33157,
        //
        // Сводка:
        //     Original was GL_PIXEL_TEX_GEN_Q_FLOOR_SGIX = 0x8186
        PixelTexGenQFloorSgix = 33158,
        //
        // Сводка:
        //     Original was GL_NEAREST_CLIPMAP_NEAREST_SGIX = 0x844D
        NearestClipmapNearestSgix = 33869,
        //
        // Сводка:
        //     Original was GL_NEAREST_CLIPMAP_LINEAR_SGIX = 0x844E
        NearestClipmapLinearSgix = 33870,
        //
        // Сводка:
        //     Original was GL_LINEAR_CLIPMAP_NEAREST_SGIX = 0x844F
        LinearClipmapNearestSgix = 33871
    }
    public enum TextureMagFilter
    {
        //
        // Сводка:
        //     Original was GL_NEAREST = 0x2600
        Nearest = 9728,
        //
        // Сводка:
        //     Original was GL_LINEAR = 0x2601
        Linear = 9729,
        //
        // Сводка:
        //     Original was GL_LINEAR_DETAIL_SGIS = 0x8097
        LinearDetailSgis = 32919,
        //
        // Сводка:
        //     Original was GL_LINEAR_DETAIL_ALPHA_SGIS = 0x8098
        LinearDetailAlphaSgis = 32920,
        //
        // Сводка:
        //     Original was GL_LINEAR_DETAIL_COLOR_SGIS = 0x8099
        LinearDetailColorSgis = 32921,
        //
        // Сводка:
        //     Original was GL_LINEAR_SHARPEN_SGIS = 0x80AD
        LinearSharpenSgis = 32941,
        //
        // Сводка:
        //     Original was GL_LINEAR_SHARPEN_ALPHA_SGIS = 0x80AE
        LinearSharpenAlphaSgis = 32942,
        //
        // Сводка:
        //     Original was GL_LINEAR_SHARPEN_COLOR_SGIS = 0x80AF
        LinearSharpenColorSgis = 32943,
        //
        // Сводка:
        //     Original was GL_FILTER4_SGIS = 0x8146
        Filter4Sgis = 33094,
        //
        // Сводка:
        //     Original was GL_PIXEL_TEX_GEN_Q_CEILING_SGIX = 0x8184
        PixelTexGenQCeilingSgix = 33156,
        //
        // Сводка:
        //     Original was GL_PIXEL_TEX_GEN_Q_ROUND_SGIX = 0x8185
        PixelTexGenQRoundSgix = 33157,
        //
        // Сводка:
        //     Original was GL_PIXEL_TEX_GEN_Q_FLOOR_SGIX = 0x8186
        PixelTexGenQFloorSgix = 33158
    }

    public class Texture
    {
        //------------------------------------------------------
        // IDs
        //------------------------------------------------------

        private int _id;
        public int id
        {
            get { return _id; }

        }

        public TextureTarget target { get; }

        public long handle { get; private set; }
        public bool enable_mipmap { get; set; }

        private int _max_mipmap_levels;
        public bool enable_aniso { get; set; }

        private float _max_anisotropy;
        public bool bindless { get; private set; } = false;

        public PixelInternalFormat pif { get; }
        public PixelFormat pf { get; }

        public PixelType pt { get; }

        public int width { get; }
        public int height { get; }

        public int depth { get; }

        public Vector3 dimensions
        {
            get
            {
                return new Vector3(width, height, depth);
            }
        }
        public TextureMinFilter min_filter { get; private set; }

        public TextureMagFilter mag_filter { get; }

        public TextureWrapMode wrap_mode { get; }

        public Vector4 border_color { get; }


        //------------------------------------------------------
        // Constructor
        //------------------------------------------------------

        public Texture(TextureTarget target,
            int width, int height, int depth,
            bool enable_mipmap, bool enable_aniso)
            : this(target, width, height, depth, enable_mipmap, enable_aniso, PixelInternalFormat.Rgba16f, PixelFormat.Rgba, PixelType.Float, TextureMinFilter.Linear, TextureMagFilter.Linear, TextureWrapMode.Clamp, Vector4.Zero)
        { }

        public Texture(TextureTarget target,
            int width, int height, int depth,
            bool enable_mipmap, bool enable_aniso,
            PixelInternalFormat pif, PixelFormat pf, PixelType pt,
            TextureMinFilter min_filter, TextureMagFilter mag_filter, TextureWrapMode wrap_mode)
            : this(target, width, height, depth, enable_mipmap, enable_aniso, pif, pf, pt, min_filter, mag_filter, wrap_mode, Vector4.Zero)
        { }

        public Texture(TextureTarget target,
            int width, int height, int depth,
            bool enable_mipmap, bool enable_aniso,
            PixelInternalFormat pif, PixelFormat pf, PixelType pt,
            TextureMinFilter min_filter, TextureMagFilter mag_filter, TextureWrapMode wrap_mode, Vector4 border_color)
        {
            // Set texture configuration
            this.target = target;

            this.width = width;
            this.height = height;
            this.depth = (target == TextureTarget.TextureCubeMap) ? 6 : depth;

            this.enable_mipmap = enable_mipmap;
            this.enable_aniso = enable_aniso;

            this.pif = pif;
            this.pf = pf;
            this.pt = pt;

            this.min_filter = min_filter;
            this.mag_filter = mag_filter;
            this.wrap_mode = wrap_mode;

            this.border_color = border_color;

            if (this.enable_mipmap)
            {
                _max_mipmap_levels = getMaxMipMap(this.width, this.height);
                this.min_filter = TextureMinFilter.LinearMipmapLinear;
            }

            if (this.enable_aniso)
            {
                _max_anisotropy = GL.GetFloat((Gl.GetPName)Gl.All.MaxTextureMaxAnisotropyExt);
            }

            // Finally, Generate a texture object
            GL.GenTextures(1, out _id);

        }


        //------------------------------------------------------
        // Helpers
        //------------------------------------------------------

        public static int getMaxMipMap(int width, int height)
        {
            int largest_dimension = Math.Max(width, height);

            return (int)Math.Log(largest_dimension, 2) - 1;
        }

        public int getMaxMipMap()
        {
            return getMaxMipMap(width, height);
        }

        public void setMaxMipMap(int max_mipmap_levels)
        {
            _max_mipmap_levels = Math.Min(getMaxMipMap(width, height), max_mipmap_levels);
            min_filter = TextureMinFilter.LinearMipmapLinear;
        }

        public void generateMipMap()
        {
            GL.GenerateTextureMipmap(_id);
        }


        //------------------------------------------------------
        // Loading
        //------------------------------------------------------

        private void setTextureParameters()
        {

            GL.TexParameter(
                (OpenTK.Graphics.OpenGL.TextureTarget)target,
                Gl.TextureParameterName.TextureMinFilter,
                (float)min_filter);
            GL.TexParameter(
                (OpenTK.Graphics.OpenGL.TextureTarget)target,
                Gl.TextureParameterName.TextureMagFilter,
                (float)mag_filter);
            GL.TexParameter(
                (OpenTK.Graphics.OpenGL.TextureTarget)target,
                Gl.TextureParameterName.TextureWrapS,
                (float)wrap_mode);
            GL.TexParameter(
                (OpenTK.Graphics.OpenGL.TextureTarget)target,
                Gl.TextureParameterName.TextureWrapT,
                (float)wrap_mode);
            GL.TexParameter(
                (OpenTK.Graphics.OpenGL.TextureTarget)target,
                Gl.TextureParameterName.TextureWrapR,
                (float)wrap_mode);

            if (border_color != Vector4.Zero)
            {
                GL.TexParameter(
                    (OpenTK.Graphics.OpenGL.TextureTarget)target,
                    Gl.TextureParameterName.TextureBorderColor,
                    EngineHelper.createArray(border_color));
            }

            if (enable_mipmap)
            {
                GL.TexParameter((OpenTK.Graphics.OpenGL.TextureTarget)target, Gl.TextureParameterName.TextureBaseLevel, 0);
                GL.TexParameter((OpenTK.Graphics.OpenGL.TextureTarget)target, Gl.TextureParameterName.TextureMaxLevel, _max_mipmap_levels);
                generateMipMap();
            }

            if (enable_aniso)
            {
                GL.TexParameter((OpenTK.Graphics.OpenGL.TextureTarget)target, (Gl.TextureParameterName)Gl.All.TextureMaxAnisotropyExt, _max_anisotropy);
            }
        }

        public void load()
        {
            load((IntPtr)0);
        }

        public void load(IntPtr data)
        {

            GL.BindTexture((OpenTK.Graphics.OpenGL.TextureTarget)target, _id);

            switch (target)
            {
                case TextureTarget.Texture1D:
                    GL.TexImage1D((OpenTK.Graphics.OpenGL.TextureTarget)target, 0, (Gl.PixelInternalFormat)pif, width, 0, (Gl.PixelFormat)pf, (Gl.PixelType)pt, data);
                    break;
                case TextureTarget.Texture2D:
                    GL.TexImage2D((OpenTK.Graphics.OpenGL.TextureTarget)target, 0, (Gl.PixelInternalFormat)pif, width, height, 0, (Gl.PixelFormat)pf, (Gl.PixelType)pt, data);
                    break;
                case TextureTarget.Texture2DArray:
                    GL.TexImage3D((Gl.TextureTarget)target, 0, (Gl.PixelInternalFormat)pif, width, height, depth, 0, (Gl.PixelFormat)pf, (Gl.PixelType)pt, (IntPtr)0);
                    break;
                case TextureTarget.TextureCubeMap:
                    for (int face = 0; face < depth; face++)
                    {
                        GL.TexImage2D((Gl.TextureTarget)TextureTarget.TextureCubeMapPositiveX + face, 0, (Gl.PixelInternalFormat)pif, width, height, 0, (Gl.PixelFormat)pf, (Gl.PixelType)pt, data);
                    }
                    break;
                case TextureTarget.TextureCubeMapArray:
                    //GL.TexImage3D(_target, 0, pif, _width, _depth, _depth * 6, 0, _pf, _pt, data);
                    GL.TexStorage3D((Gl.TextureTarget3d)target, _max_mipmap_levels + 1, (Gl.SizedInternalFormat)pif, width, height, depth * 6);
                    //for (int layer = 0; layer < _depth; layer++)
                    //{
                    //    for (int face = 0; face < 6; face++)
                    //    {
                    //        GL.TexSubImage2D(TextureTarget.TextureCubeMapPositiveX + face, 0, 0, 0, _width, _height, _pf, _pt, data);
                    //        //GL.TexImage2D(TextureTarget.TextureCubeMapPositiveX + face, layer, _pif, _width, _height, 0, _pf, _pt, data);
                    //    }
                    //}
                    break;
                case TextureTarget.Texture3D:
                    GL.TexImage3D((Gl.TextureTarget)target, 0, (Gl.PixelInternalFormat)pif, width, height, depth, 0, (Gl.PixelFormat)pf, (Gl.PixelType)pt, data);
                    break;
                default:
                    throw new Exception($"Load Texture: Unsupported Texture Target [ {target.ToString()} ]");
            }

            setTextureParameters();

        }

        public void load(IntPtr[] data)
        {
            if (data.Length != depth)
            {
                throw new Exception("Load Texture: Length of data array does not match texture's depth");
            }

            GL.BindTexture((Gl.TextureTarget)target, _id);

            switch (target)
            {
                case TextureTarget.Texture1D:
                    throw new Exception("Load Texture: Cannot load data array into Texture1D");
                case TextureTarget.Texture2D:
                    throw new Exception("Load Texture: Cannot load data array into Texture2D");
                case TextureTarget.Texture2DArray:
                    GL.TexImage3D((Gl.TextureTarget)target, 0, (Gl.PixelInternalFormat)pif, width, height, depth, 0, (Gl.PixelFormat)pf, (Gl.PixelType)pt, (IntPtr)0);
                    for (int slice = 0; slice < depth; slice++)
                    {
                        GL.TexSubImage3D((Gl.TextureTarget)target, 0, 0, 0, slice, width, height, 1, (Gl.PixelFormat)pf, (Gl.PixelType)pt, data[slice]);
                    }
                    break;
                case TextureTarget.TextureCubeMap:
                    for (int face = 0; face < 6; face++)
                    {
                        GL.TexImage2D((OpenTK.Graphics.OpenGL.TextureTarget)TextureTarget.TextureCubeMapPositiveX + face, 0, (Gl.PixelInternalFormat)pif, width, height, 0, (Gl.PixelFormat)pf, (Gl.PixelType)pt, data[face]);
                    }
                    break;
                case TextureTarget.Texture3D:
                    GL.TexImage3D((OpenTK.Graphics.OpenGL.TextureTarget)target, 0, (Gl.PixelInternalFormat)pif, width, height, depth, 0, (Gl.PixelFormat)pf, (Gl.PixelType)pt, (IntPtr)0);
                    for (int slice = 0; slice < depth; slice++)
                    {
                        GL.TexSubImage3D((OpenTK.Graphics.OpenGL.TextureTarget)target, 0, 0, 0, slice, width, height, 1, (Gl.PixelFormat)pf, (Gl.PixelType)pt, data[slice]);
                    }
                    break;
                default:
                    throw new Exception($"Load Texture: Unsupported Texture Target [ {target.ToString()} ]");
            }

            setTextureParameters();
        }


        public void loadBindless()
        {
            handle = GL.Arb.GetTextureHandle(_id);
            bindless = true;
        }


        //------------------------------------------------------
        // Unloading
        //------------------------------------------------------
        public void clear()
        {
            Vector4 clear_value = new Vector4(0.0f);
            for (int i = 0; i < _max_mipmap_levels + 1; i++)
            {
                GL.ClearTexImage(_id, i, (Gl.PixelFormat)pf, (Gl.PixelType)pt, ref clear_value);
            }
        }


        //------------------------------------------------------
        // Binding
        //------------------------------------------------------

        public void bind(int texture_uniform, int index)
        {
            GL.ActiveTexture(Gl.TextureUnit.Texture0 + index);
            GL.BindTexture((OpenTK.Graphics.OpenGL.TextureTarget)target, _id);
            OGL.Uniform(texture_uniform, index);
        }

        public void bindImageUnit(int texture_uniform, int index, TextureAccess access)
        {
            bindImageUnit(texture_uniform, index, access, 0, 0);
        }

        public void bindImageUnit(int texture_uniform, int index, TextureAccess access, int level)
        {
            bindImageUnit(texture_uniform, index, access, level, 0);
        }

        public void bindImageUnit(int texture_uniform, int index, TextureAccess access, int level, int layer)
        {
            bool layered = false;
            switch (target)
            {
                case TextureTarget.Texture2DArray:
                case TextureTarget.TextureCubeMap:
                case TextureTarget.TextureCubeMapArray:
                case TextureTarget.Texture3D:
                    layered = true;
                    break;
            }

            GL.ActiveTexture(Gl.TextureUnit.Texture0 + index);
            GL.BindImageTexture(
                index,
                _id,
                level,
                layered,
                layer,
                (Gl.TextureAccess)access,
                (Gl.SizedInternalFormat)pif);
            OGL.Uniform(texture_uniform, index);
        }

        public void makeResident()
        {
            GL.Arb.MakeTextureHandleResident(handle);
        }

        public void makeNonResident()
        {
            GL.Arb.MakeTextureHandleNonResident(handle);
        }

    }
}
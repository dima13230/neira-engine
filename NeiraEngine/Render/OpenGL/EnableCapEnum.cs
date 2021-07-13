using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiraEngine.Render.OpenGL
{
    public enum EnableCap
    {
        //
        // Сводка:
        //     Original was GL_POINT_SMOOTH = 0x0B10
        PointSmooth = 2832,
        //
        // Сводка:
        //     Original was GL_LINE_SMOOTH = 0x0B20
        LineSmooth = 2848,
        //
        // Сводка:
        //     Original was GL_LINE_STIPPLE = 0x0B24
        LineStipple = 2852,
        //
        // Сводка:
        //     Original was GL_POLYGON_SMOOTH = 0x0B41
        PolygonSmooth = 2881,
        //
        // Сводка:
        //     Original was GL_POLYGON_STIPPLE = 0x0B42
        PolygonStipple = 2882,
        //
        // Сводка:
        //     Original was GL_CULL_FACE = 0x0B44
        CullFace = 2884,
        //
        // Сводка:
        //     Original was GL_LIGHTING = 0x0B50
        Lighting = 2896,
        //
        // Сводка:
        //     Original was GL_COLOR_MATERIAL = 0x0B57
        ColorMaterial = 2903,
        //
        // Сводка:
        //     Original was GL_FOG = 0x0B60
        Fog = 2912,
        //
        // Сводка:
        //     Original was GL_DEPTH_TEST = 0x0B71
        DepthTest = 2929,
        //
        // Сводка:
        //     Original was GL_STENCIL_TEST = 0x0B90
        StencilTest = 2960,
        //
        // Сводка:
        //     Original was GL_NORMALIZE = 0x0BA1
        Normalize = 2977,
        //
        // Сводка:
        //     Original was GL_ALPHA_TEST = 0x0BC0
        AlphaTest = 3008,
        //
        // Сводка:
        //     Original was GL_DITHER = 0x0BD0
        Dither = 3024,
        //
        // Сводка:
        //     Original was GL_BLEND = 0x0BE2
        Blend = 3042,
        //
        // Сводка:
        //     Original was GL_INDEX_LOGIC_OP = 0x0BF1
        IndexLogicOp = 3057,
        //
        // Сводка:
        //     Original was GL_COLOR_LOGIC_OP = 0x0BF2
        ColorLogicOp = 3058,
        //
        // Сводка:
        //     Original was GL_SCISSOR_TEST = 0x0C11
        ScissorTest = 3089,
        //
        // Сводка:
        //     Original was GL_TEXTURE_GEN_S = 0x0C60
        TextureGenS = 3168,
        //
        // Сводка:
        //     Original was GL_TEXTURE_GEN_T = 0x0C61
        TextureGenT = 3169,
        //
        // Сводка:
        //     Original was GL_TEXTURE_GEN_R = 0x0C62
        TextureGenR = 3170,
        //
        // Сводка:
        //     Original was GL_TEXTURE_GEN_Q = 0x0C63
        TextureGenQ = 3171,
        //
        // Сводка:
        //     Original was GL_AUTO_NORMAL = 0x0D80
        AutoNormal = 3456,
        //
        // Сводка:
        //     Original was GL_MAP1_COLOR_4 = 0x0D90
        Map1Color4 = 3472,
        //
        // Сводка:
        //     Original was GL_MAP1_INDEX = 0x0D91
        Map1Index = 3473,
        //
        // Сводка:
        //     Original was GL_MAP1_NORMAL = 0x0D92
        Map1Normal = 3474,
        //
        // Сводка:
        //     Original was GL_MAP1_TEXTURE_COORD_1 = 0x0D93
        Map1TextureCoord1 = 3475,
        //
        // Сводка:
        //     Original was GL_MAP1_TEXTURE_COORD_2 = 0x0D94
        Map1TextureCoord2 = 3476,
        //
        // Сводка:
        //     Original was GL_MAP1_TEXTURE_COORD_3 = 0x0D95
        Map1TextureCoord3 = 3477,
        //
        // Сводка:
        //     Original was GL_MAP1_TEXTURE_COORD_4 = 0x0D96
        Map1TextureCoord4 = 3478,
        //
        // Сводка:
        //     Original was GL_MAP1_VERTEX_3 = 0x0D97
        Map1Vertex3 = 3479,
        //
        // Сводка:
        //     Original was GL_MAP1_VERTEX_4 = 0x0D98
        Map1Vertex4 = 3480,
        //
        // Сводка:
        //     Original was GL_MAP2_COLOR_4 = 0x0DB0
        Map2Color4 = 3504,
        //
        // Сводка:
        //     Original was GL_MAP2_INDEX = 0x0DB1
        Map2Index = 3505,
        //
        // Сводка:
        //     Original was GL_MAP2_NORMAL = 0x0DB2
        Map2Normal = 3506,
        //
        // Сводка:
        //     Original was GL_MAP2_TEXTURE_COORD_1 = 0x0DB3
        Map2TextureCoord1 = 3507,
        //
        // Сводка:
        //     Original was GL_MAP2_TEXTURE_COORD_2 = 0x0DB4
        Map2TextureCoord2 = 3508,
        //
        // Сводка:
        //     Original was GL_MAP2_TEXTURE_COORD_3 = 0x0DB5
        Map2TextureCoord3 = 3509,
        //
        // Сводка:
        //     Original was GL_MAP2_TEXTURE_COORD_4 = 0x0DB6
        Map2TextureCoord4 = 3510,
        //
        // Сводка:
        //     Original was GL_MAP2_VERTEX_3 = 0x0DB7
        Map2Vertex3 = 3511,
        //
        // Сводка:
        //     Original was GL_MAP2_VERTEX_4 = 0x0DB8
        Map2Vertex4 = 3512,
        //
        // Сводка:
        //     Original was GL_TEXTURE_1D = 0x0DE0
        Texture1D = 3552,
        //
        // Сводка:
        //     Original was GL_TEXTURE_2D = 0x0DE1
        Texture2D = 3553,
        //
        // Сводка:
        //     Original was GL_POLYGON_OFFSET_POINT = 0x2A01
        PolygonOffsetPoint = 10753,
        //
        // Сводка:
        //     Original was GL_POLYGON_OFFSET_LINE = 0x2A02
        PolygonOffsetLine = 10754,
        //
        // Сводка:
        //     Original was GL_CLIP_DISTANCE0 = 0x3000
        ClipDistance0 = 12288,
        //
        // Сводка:
        //     Original was GL_CLIP_PLANE0 = 0x3000
        ClipPlane0 = 12288,
        //
        // Сводка:
        //     Original was GL_CLIP_DISTANCE1 = 0x3001
        ClipDistance1 = 12289,
        //
        // Сводка:
        //     Original was GL_CLIP_PLANE1 = 0x3001
        ClipPlane1 = 12289,
        //
        // Сводка:
        //     Original was GL_CLIP_DISTANCE2 = 0x3002
        ClipDistance2 = 12290,
        //
        // Сводка:
        //     Original was GL_CLIP_PLANE2 = 0x3002
        ClipPlane2 = 12290,
        //
        // Сводка:
        //     Original was GL_CLIP_DISTANCE3 = 0x3003
        ClipDistance3 = 12291,
        //
        // Сводка:
        //     Original was GL_CLIP_PLANE3 = 0x3003
        ClipPlane3 = 12291,
        //
        // Сводка:
        //     Original was GL_CLIP_DISTANCE4 = 0x3004
        ClipDistance4 = 12292,
        //
        // Сводка:
        //     Original was GL_CLIP_PLANE4 = 0x3004
        ClipPlane4 = 12292,
        //
        // Сводка:
        //     Original was GL_CLIP_DISTANCE5 = 0x3005
        ClipDistance5 = 12293,
        //
        // Сводка:
        //     Original was GL_CLIP_PLANE5 = 0x3005
        ClipPlane5 = 12293,
        //
        // Сводка:
        //     Original was GL_CLIP_DISTANCE6 = 0x3006
        ClipDistance6 = 12294,
        //
        // Сводка:
        //     Original was GL_CLIP_DISTANCE7 = 0x3007
        ClipDistance7 = 12295,
        //
        // Сводка:
        //     Original was GL_LIGHT0 = 0x4000
        Light0 = 16384,
        //
        // Сводка:
        //     Original was GL_LIGHT1 = 0x4001
        Light1 = 16385,
        //
        // Сводка:
        //     Original was GL_LIGHT2 = 0x4002
        Light2 = 16386,
        //
        // Сводка:
        //     Original was GL_LIGHT3 = 0x4003
        Light3 = 16387,
        //
        // Сводка:
        //     Original was GL_LIGHT4 = 0x4004
        Light4 = 16388,
        //
        // Сводка:
        //     Original was GL_LIGHT5 = 0x4005
        Light5 = 16389,
        //
        // Сводка:
        //     Original was GL_LIGHT6 = 0x4006
        Light6 = 16390,
        //
        // Сводка:
        //     Original was GL_LIGHT7 = 0x4007
        Light7 = 16391,
        //
        // Сводка:
        //     Original was GL_CONVOLUTION_1D = 0x8010
        Convolution1D = 32784,
        //
        // Сводка:
        //     Original was GL_CONVOLUTION_1D_EXT = 0x8010
        Convolution1DExt = 32784,
        //
        // Сводка:
        //     Original was GL_CONVOLUTION_2D = 0x8011
        Convolution2D = 32785,
        //
        // Сводка:
        //     Original was GL_CONVOLUTION_2D_EXT = 0x8011
        Convolution2DExt = 32785,
        //
        // Сводка:
        //     Original was GL_SEPARABLE_2D = 0x8012
        Separable2D = 32786,
        //
        // Сводка:
        //     Original was GL_SEPARABLE_2D_EXT = 0x8012
        Separable2DExt = 32786,
        //
        // Сводка:
        //     Original was GL_HISTOGRAM = 0x8024
        Histogram = 32804,
        //
        // Сводка:
        //     Original was GL_HISTOGRAM_EXT = 0x8024
        HistogramExt = 32804,
        //
        // Сводка:
        //     Original was GL_MINMAX_EXT = 0x802E
        MinmaxExt = 32814,
        //
        // Сводка:
        //     Original was GL_POLYGON_OFFSET_FILL = 0x8037
        PolygonOffsetFill = 32823,
        //
        // Сводка:
        //     Original was GL_RESCALE_NORMAL = 0x803A
        RescaleNormal = 32826,
        //
        // Сводка:
        //     Original was GL_RESCALE_NORMAL_EXT = 0x803A
        RescaleNormalExt = 32826,
        //
        // Сводка:
        //     Original was GL_TEXTURE_3D_EXT = 0x806F
        Texture3DExt = 32879,
        //
        // Сводка:
        //     Original was GL_VERTEX_ARRAY = 0x8074
        VertexArray = 32884,
        //
        // Сводка:
        //     Original was GL_NORMAL_ARRAY = 0x8075
        NormalArray = 32885,
        //
        // Сводка:
        //     Original was GL_COLOR_ARRAY = 0x8076
        ColorArray = 32886,
        //
        // Сводка:
        //     Original was GL_INDEX_ARRAY = 0x8077
        IndexArray = 32887,
        //
        // Сводка:
        //     Original was GL_TEXTURE_COORD_ARRAY = 0x8078
        TextureCoordArray = 32888,
        //
        // Сводка:
        //     Original was GL_EDGE_FLAG_ARRAY = 0x8079
        EdgeFlagArray = 32889,
        //
        // Сводка:
        //     Original was GL_INTERLACE_SGIX = 0x8094
        InterlaceSgix = 32916,
        //
        // Сводка:
        //     Original was GL_MULTISAMPLE = 0x809D
        Multisample = 32925,
        //
        // Сводка:
        //     Original was GL_MULTISAMPLE_SGIS = 0x809D
        MultisampleSgis = 32925,
        //
        // Сводка:
        //     Original was GL_SAMPLE_ALPHA_TO_COVERAGE = 0x809E
        SampleAlphaToCoverage = 32926,
        //
        // Сводка:
        //     Original was GL_SAMPLE_ALPHA_TO_MASK_SGIS = 0x809E
        SampleAlphaToMaskSgis = 32926,
        //
        // Сводка:
        //     Original was GL_SAMPLE_ALPHA_TO_ONE = 0x809F
        SampleAlphaToOne = 32927,
        //
        // Сводка:
        //     Original was GL_SAMPLE_ALPHA_TO_ONE_SGIS = 0x809F
        SampleAlphaToOneSgis = 32927,
        //
        // Сводка:
        //     Original was GL_SAMPLE_COVERAGE = 0x80A0
        SampleCoverage = 32928,
        //
        // Сводка:
        //     Original was GL_SAMPLE_MASK_SGIS = 0x80A0
        SampleMaskSgis = 32928,
        //
        // Сводка:
        //     Original was GL_TEXTURE_COLOR_TABLE_SGI = 0x80BC
        TextureColorTableSgi = 32956,
        //
        // Сводка:
        //     Original was GL_COLOR_TABLE = 0x80D0
        ColorTable = 32976,
        //
        // Сводка:
        //     Original was GL_COLOR_TABLE_SGI = 0x80D0
        ColorTableSgi = 32976,
        //
        // Сводка:
        //     Original was GL_POST_CONVOLUTION_COLOR_TABLE = 0x80D1
        PostConvolutionColorTable = 32977,
        //
        // Сводка:
        //     Original was GL_POST_CONVOLUTION_COLOR_TABLE_SGI = 0x80D1
        PostConvolutionColorTableSgi = 32977,
        //
        // Сводка:
        //     Original was GL_POST_COLOR_MATRIX_COLOR_TABLE = 0x80D2
        PostColorMatrixColorTable = 32978,
        //
        // Сводка:
        //     Original was GL_POST_COLOR_MATRIX_COLOR_TABLE_SGI = 0x80D2
        PostColorMatrixColorTableSgi = 32978,
        //
        // Сводка:
        //     Original was GL_TEXTURE_4D_SGIS = 0x8134
        Texture4DSgis = 33076,
        //
        // Сводка:
        //     Original was GL_PIXEL_TEX_GEN_SGIX = 0x8139
        PixelTexGenSgix = 33081,
        //
        // Сводка:
        //     Original was GL_SPRITE_SGIX = 0x8148
        SpriteSgix = 33096,
        //
        // Сводка:
        //     Original was GL_REFERENCE_PLANE_SGIX = 0x817D
        ReferencePlaneSgix = 33149,
        //
        // Сводка:
        //     Original was GL_IR_INSTRUMENT1_SGIX = 0x817F
        IrInstrument1Sgix = 33151,
        //
        // Сводка:
        //     Original was GL_CALLIGRAPHIC_FRAGMENT_SGIX = 0x8183
        CalligraphicFragmentSgix = 33155,
        //
        // Сводка:
        //     Original was GL_FRAMEZOOM_SGIX = 0x818B
        FramezoomSgix = 33163,
        //
        // Сводка:
        //     Original was GL_FOG_OFFSET_SGIX = 0x8198
        FogOffsetSgix = 33176,
        //
        // Сводка:
        //     Original was GL_SHARED_TEXTURE_PALETTE_EXT = 0x81FB
        SharedTexturePaletteExt = 33275,
        //
        // Сводка:
        //     Original was GL_DEBUG_OUTPUT_SYNCHRONOUS = 0x8242
        DebugOutputSynchronous = 33346,
        //
        // Сводка:
        //     Original was GL_ASYNC_HISTOGRAM_SGIX = 0x832C
        AsyncHistogramSgix = 33580,
        //
        // Сводка:
        //     Original was GL_PIXEL_TEXTURE_SGIS = 0x8353
        PixelTextureSgis = 33619,
        //
        // Сводка:
        //     Original was GL_ASYNC_TEX_IMAGE_SGIX = 0x835C
        AsyncTexImageSgix = 33628,
        //
        // Сводка:
        //     Original was GL_ASYNC_DRAW_PIXELS_SGIX = 0x835D
        AsyncDrawPixelsSgix = 33629,
        //
        // Сводка:
        //     Original was GL_ASYNC_READ_PIXELS_SGIX = 0x835E
        AsyncReadPixelsSgix = 33630,
        //
        // Сводка:
        //     Original was GL_FRAGMENT_LIGHTING_SGIX = 0x8400
        FragmentLightingSgix = 33792,
        //
        // Сводка:
        //     Original was GL_FRAGMENT_COLOR_MATERIAL_SGIX = 0x8401
        FragmentColorMaterialSgix = 33793,
        //
        // Сводка:
        //     Original was GL_FRAGMENT_LIGHT0_SGIX = 0x840C
        FragmentLight0Sgix = 33804,
        //
        // Сводка:
        //     Original was GL_FRAGMENT_LIGHT1_SGIX = 0x840D
        FragmentLight1Sgix = 33805,
        //
        // Сводка:
        //     Original was GL_FRAGMENT_LIGHT2_SGIX = 0x840E
        FragmentLight2Sgix = 33806,
        //
        // Сводка:
        //     Original was GL_FRAGMENT_LIGHT3_SGIX = 0x840F
        FragmentLight3Sgix = 33807,
        //
        // Сводка:
        //     Original was GL_FRAGMENT_LIGHT4_SGIX = 0x8410
        FragmentLight4Sgix = 33808,
        //
        // Сводка:
        //     Original was GL_FRAGMENT_LIGHT5_SGIX = 0x8411
        FragmentLight5Sgix = 33809,
        //
        // Сводка:
        //     Original was GL_FRAGMENT_LIGHT6_SGIX = 0x8412
        FragmentLight6Sgix = 33810,
        //
        // Сводка:
        //     Original was GL_FRAGMENT_LIGHT7_SGIX = 0x8413
        FragmentLight7Sgix = 33811,
        //
        // Сводка:
        //     Original was GL_FOG_COORD_ARRAY = 0x8457
        FogCoordArray = 33879,
        //
        // Сводка:
        //     Original was GL_COLOR_SUM = 0x8458
        ColorSum = 33880,
        //
        // Сводка:
        //     Original was GL_SECONDARY_COLOR_ARRAY = 0x845E
        SecondaryColorArray = 33886,
        //
        // Сводка:
        //     Original was GL_TEXTURE_RECTANGLE = 0x84F5
        TextureRectangle = 34037,
        //
        // Сводка:
        //     Original was GL_TEXTURE_CUBE_MAP = 0x8513
        TextureCubeMap = 34067,
        //
        // Сводка:
        //     Original was GL_PROGRAM_POINT_SIZE = 0x8642
        ProgramPointSize = 34370,
        //
        // Сводка:
        //     Original was GL_VERTEX_PROGRAM_POINT_SIZE = 0x8642
        VertexProgramPointSize = 34370,
        //
        // Сводка:
        //     Original was GL_VERTEX_PROGRAM_TWO_SIDE = 0x8643
        VertexProgramTwoSide = 34371,
        //
        // Сводка:
        //     Original was GL_DEPTH_CLAMP = 0x864F
        DepthClamp = 34383,
        //
        // Сводка:
        //     Original was GL_TEXTURE_CUBE_MAP_SEAMLESS = 0x884F
        TextureCubeMapSeamless = 34895,
        //
        // Сводка:
        //     Original was GL_POINT_SPRITE = 0x8861
        PointSprite = 34913,
        //
        // Сводка:
        //     Original was GL_SAMPLE_SHADING = 0x8C36
        SampleShading = 35894,
        //
        // Сводка:
        //     Original was GL_RASTERIZER_DISCARD = 0x8C89
        RasterizerDiscard = 35977,
        //
        // Сводка:
        //     Original was GL_PRIMITIVE_RESTART_FIXED_INDEX = 0x8D69
        PrimitiveRestartFixedIndex = 36201,
        //
        // Сводка:
        //     Original was GL_FRAMEBUFFER_SRGB = 0x8DB9
        FramebufferSrgb = 36281,
        //
        // Сводка:
        //     Original was GL_SAMPLE_MASK = 0x8E51
        SampleMask = 36433,
        //
        // Сводка:
        //     Original was GL_PRIMITIVE_RESTART = 0x8F9D
        PrimitiveRestart = 36765,
        //
        // Сводка:
        //     Original was GL_DEBUG_OUTPUT = 0x92E0
        DebugOutput = 37600
    }
}

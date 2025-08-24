using Blazor.WebGPU.Matrix.Internal;
using SpawnDev.BlazorJS.JSObjects;

namespace Blazor.WebGPU.Matrix;

public class Mat4 : BaseArray<float>
{
    public Mat4() : base(16) { }

    /// <summary>
    /// Returns a JavaScript Float32Array
    /// </summary>
    public override TypedArray<float> Array => new Float32Array(_elements);

    /// <summary>
    /// Create a Mat4 from values
    /// </summary>
    public static Mat4 Create(
        float v0 = 0, float v1 = 0, float v2 = 0, float v3 = 0,
        float v4 = 0, float v5 = 0, float v6 = 0, float v7 = 0,
        float v8 = 0, float v9 = 0, float v10 = 0, float v11 = 0,
        float v12 = 0, float v13 = 0, float v14 = 0, float v15 = 0)
    {
        var dst = new Mat4();
        dst[0] = v0;    dst[1] = v1;    dst[2] = v2;    dst[3] = v3;
        dst[4] = v4;    dst[5] = v5;    dst[6] = v6;    dst[7] = v7;
        dst[8] = v8;    dst[9] = v9;    dst[10] = v10;  dst[11] = v11;
        dst[12] = v12;  dst[13] = v13;  dst[14] = v14;  dst[15] = v15;
        return dst;
    }

    /// <summary>
    /// Sets the values of a Mat4
    /// </summary>
    public static Mat4 Set(
        float v0, float v1, float v2, float v3,
        float v4, float v5, float v6, float v7,
        float v8, float v9, float v10, float v11,
        float v12, float v13, float v14, float v15,
        Mat4? dst = default)
    {
        dst = dst ?? new Mat4();
        dst[0] = v0; dst[1] = v1; dst[2] = v2; dst[3] = v3;
        dst[4] = v4; dst[5] = v5; dst[6] = v6; dst[7] = v7;
        dst[8] = v8; dst[9] = v9; dst[10] = v10; dst[11] = v11;
        dst[12] = v12; dst[13] = v13; dst[14] = v14; dst[15] = v15;
        return dst;
    }

    /// <summary>
    /// Creates a Mat4 from a Mat3
    /// </summary>
    public static Mat4 FromMat3(Mat3 m3, Mat4? dst = default)
    {
        dst = dst ?? new Mat4();
        dst[0] = m3[0]; dst[1] = m3[1]; dst[2] = m3[2]; dst[3] = 0;
        dst[4] = m3[4]; dst[5] = m3[5]; dst[6] = m3[6]; dst[7] = 0;
        dst[8] = m3[8]; dst[9] = m3[9]; dst[10] = m3[10]; dst[11] = 0;
        dst[12] = 0; dst[13] = 0; dst[14] = 0; dst[15] = 1;
        return dst;
    }

    /// <summary>
    /// Creates a Mat4 rotation matrix from a quaternion
    /// </summary>
    public static Mat4 FromQuat(Quat q, Mat4? dst = default)
    {
        dst = dst ?? new Mat4();
        var x = q[0]; var y = q[1]; var z = q[2]; var w = q[3];
        var x2 = x + x; var y2 = y + y; var z2 = z + z;

        var xx = x * x2;
        var yx = y * x2;
        var yy = y * y2;
        var zx = z * x2;
        var zy = z * y2;
        var zz = z * z2;
        var wx = w * x2;
        var wy = w * y2;
        var wz = w * z2;

        dst[0] = 1 - yy - zz; dst[1] = yx + wz; dst[2] = zx - wy; dst[3] = 0;
        dst[4] = yx - wz; dst[5] = 1 - xx - zz; dst[6] = zy + wx; dst[7] = 0;
        dst[8] = zx + wy; dst[9] = zy - wx; dst[10] = 1 - xx - yy; dst[11] = 0;
        dst[12] = 0; dst[13] = 0; dst[14] = 0; dst[15] = 1;
        return dst;
    }

    /// <summary>
    /// Negates a matrix.
    /// </summary>
    public static Mat4 Negate(Mat4 m, Mat4? dst = default)
    {
        dst = dst ?? new Mat4();
        dst[0] = -m[0]; dst[1] = -m[1]; dst[2] = -m[2]; dst[3] = -m[3];
        dst[4] = -m[4]; dst[5] = -m[5]; dst[6] = -m[6]; dst[7] = -m[7];
        dst[8] = -m[8]; dst[9] = -m[9]; dst[10] = -m[10]; dst[11] = -m[11];
        dst[12] = -m[12]; dst[13] = -m[13]; dst[14] = -m[14]; dst[15] = -m[15];
        return dst;
    }

    /// <summary>
    /// add 2 matrices.
    /// </summary>
    public static Mat4 Add(Mat4 a, Mat4 b, Mat4? dst = default)
    {
        dst = dst ?? new Mat4();
        dst[0] = a[0] + b[0]; dst[1] = a[1] + b[1]; dst[2] = a[2] + b[2]; dst[3] = a[3] + b[3];
        dst[4] = a[4] + b[4]; dst[5] = a[5] + b[5]; dst[6] = a[6] + b[6]; dst[7] = a[7] + b[7];
        dst[8] = a[8] + b[8]; dst[9] = a[9] + b[9]; dst[10] = a[10] + b[10]; dst[11] = a[11] + b[11];
        dst[12] = a[12] + b[12]; dst[13] = a[13] + b[13]; dst[14] = a[14] + b[14]; dst[15] = a[15] + b[15];
        return dst;
    }

    /// <summary>
    /// Multiplies a matrix by a scalar
    /// </summary>
    public static Mat4 MultiplyScalar(Mat4 m, float s, Mat4? dst = default)
    {
        dst = dst ?? new Mat4();
        dst[0] = m[0] * s; dst[1] = m[1] * s; dst[2] = m[2] * s; dst[3] = m[3] * s;
        dst[4] = m[4] * s; dst[5] = m[5] * s; dst[6] = m[6] * s; dst[7] = m[7] * s;
        dst[8] = m[8] * s; dst[9] = m[9] * s; dst[10] = m[10] * s; dst[11] = m[11] * s;
        dst[12] = m[12] * s; dst[13] = m[13] * s; dst[14] = m[14] * s; dst[15] = m[15] * s;
        return dst;
    }

    /// <summary>
    /// Multiplies a matrix by a scalar (same as multiplyScalar)
    /// </summary>
    public static Mat4 MulScalar(Mat4 m, float s, Mat4? dst = default) => MultiplyScalar(m, s, dst);

    /// <summary>
    /// Copies a matrix. (same as clone)
    /// </summary>
    public static Mat4 Copy(Mat4 m, Mat4? dst = default)
    {
        dst = dst ?? new Mat4();
        dst[0] = m[0]; dst[1] = m[1]; dst[2] = m[2]; dst[3] = m[3];
        dst[4] = m[4]; dst[5] = m[5]; dst[6] = m[6]; dst[7] = m[7];
        dst[8] = m[8]; dst[9] = m[9]; dst[10] = m[10]; dst[11] = m[11];
        dst[12] = m[12]; dst[13] = m[13]; dst[14] = m[14]; dst[15] = m[15];
        return dst;
    }

    /// <summary>
    /// Copies a matrix (same as copy)
    /// </summary>
    public static Mat4 Clone(Mat4 m, Mat4? dst = default) => Copy(m, dst);

    /// <summary>
    /// Check if 2 matrices are approximately equal
    /// </summary>
    public static bool EqualsApproximately(Mat4 a, Mat4 b)
    {
        return MathF.Abs(a[0] - b[0]) < Utils.EPSILON &&
               MathF.Abs(a[1] - b[1]) < Utils.EPSILON &&
               MathF.Abs(a[2] - b[2]) < Utils.EPSILON &&
               MathF.Abs(a[3] - b[3]) < Utils.EPSILON &&
               MathF.Abs(a[4] - b[4]) < Utils.EPSILON &&
               MathF.Abs(a[5] - b[5]) < Utils.EPSILON &&
               MathF.Abs(a[6] - b[6]) < Utils.EPSILON &&
               MathF.Abs(a[7] - b[7]) < Utils.EPSILON &&
               MathF.Abs(a[8] - b[8]) < Utils.EPSILON &&
               MathF.Abs(a[9] - b[9]) < Utils.EPSILON &&
               MathF.Abs(a[10] - b[10]) < Utils.EPSILON &&
               MathF.Abs(a[11] - b[11]) < Utils.EPSILON &&
               MathF.Abs(a[12] - b[12]) < Utils.EPSILON &&
               MathF.Abs(a[13] - b[13]) < Utils.EPSILON &&
               MathF.Abs(a[14] - b[14]) < Utils.EPSILON &&
               MathF.Abs(a[15] - b[15]) < Utils.EPSILON;
    }

    /// <summary>
    /// Check if 2 matrices are exactly equal
    /// </summary>
    public static bool Equals(Mat4 a, Mat4 b)
    {
        return a[0] == b[0] &&
               a[1] == b[1] &&
               a[2] == b[2] &&
               a[3] == b[3] &&
               a[4] == b[4] &&
               a[5] == b[5] &&
               a[6] == b[6] &&
               a[7] == b[7] &&
               a[8] == b[8] &&
               a[9] == b[9] &&
               a[10] == b[10] &&
               a[11] == b[11] &&
               a[12] == b[12] &&
               a[13] == b[13] &&
               a[14] == b[14] &&
               a[15] == b[15];
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj))
            return true;

        if (obj is null) return false;

        if (obj is Mat4 m4)
            return Equals(this, m4);

        return false;
    }

    /// <summary>
    /// Creates a 4-by-4 identity matrix.
    /// </summary>
    public static Mat4 Identity(Mat4? dst = default)
    {
        dst = dst ?? new Mat4();
        dst[0] = 1; dst[1] = 0; dst[2] = 0; dst[3] = 0;
        dst[4] = 0; dst[5] = 1; dst[6] = 0; dst[7] = 0;
        dst[8] = 0; dst[9] = 0; dst[10] = 1; dst[11] = 0;
        dst[12] = 0; dst[13] = 0; dst[14] = 0; dst[15] = 1;
        return dst;
    }

    /// <summary>
    /// Takes the transpose of a matrix.
    /// </summary>
    public static Mat4 Transpose(Mat4 m, Mat4? dst = default)
    {
        dst = dst ?? new Mat4();
        if (dst == m)
        {
            float t;
            t = m[1];
            m[1] = m[4];
            m[4] = t;
            t = m[2];
            m[2] = m[8];
            m[8] = t;
            t = m[3];
            m[3] = m[12];
            m[12] = t;
            t = m[6];
            m[6] = m[9];
            m[9] = t;
            t = m[7];
            m[7] = m[13];
            m[13] = t;
            t = m[11];
            m[11] = m[14];
            m[14] = t;
            return dst;
        }

        var m00 = m[0 * 4 + 0];
        var m01 = m[0 * 4 + 1];
        var m02 = m[0 * 4 + 2];
        var m03 = m[0 * 4 + 3];
        var m10 = m[1 * 4 + 0];
        var m11 = m[1 * 4 + 1];
        var m12 = m[1 * 4 + 2];
        var m13 = m[1 * 4 + 3];
        var m20 = m[2 * 4 + 0];
        var m21 = m[2 * 4 + 1];
        var m22 = m[2 * 4 + 2];
        var m23 = m[2 * 4 + 3];
        var m30 = m[3 * 4 + 0];
        var m31 = m[3 * 4 + 1];
        var m32 = m[3 * 4 + 2];
        var m33 = m[3 * 4 + 3];

        dst[0] = m00; dst[1] = m10; dst[2] = m20; dst[3] = m30;
        dst[4] = m01; dst[5] = m11; dst[6] = m21; dst[7] = m31;
        dst[8] = m02; dst[9] = m12; dst[10] = m22; dst[11] = m32;
        dst[12] = m03; dst[13] = m13; dst[14] = m23; dst[15] = m33;
        return dst;
    }

    /// <summary>
    /// Computes the inverse of a 4-by-4 matrix.
    /// </summary>
    public static Mat4 Inverse(Mat4 m, Mat4? dst = default)
    {
        dst = dst ?? new Mat4();
        var m00 = m[0 * 4 + 0];
        var m01 = m[0 * 4 + 1];
        var m02 = m[0 * 4 + 2];
        var m03 = m[0 * 4 + 3];
        var m10 = m[1 * 4 + 0];
        var m11 = m[1 * 4 + 1];
        var m12 = m[1 * 4 + 2];
        var m13 = m[1 * 4 + 3];
        var m20 = m[2 * 4 + 0];
        var m21 = m[2 * 4 + 1];
        var m22 = m[2 * 4 + 2];
        var m23 = m[2 * 4 + 3];
        var m30 = m[3 * 4 + 0];
        var m31 = m[3 * 4 + 1];
        var m32 = m[3 * 4 + 2];
        var m33 = m[3 * 4 + 3];

        var tmp0 = m22 * m33;
        var tmp1 = m32 * m23;
        var tmp2 = m12 * m33;
        var tmp3 = m32 * m13;
        var tmp4 = m12 * m23;
        var tmp5 = m22 * m13;
        var tmp6 = m02 * m33;
        var tmp7 = m32 * m03;
        var tmp8 = m02 * m23;
        var tmp9 = m22 * m03;
        var tmp10 = m02 * m13;
        var tmp11 = m12 * m03;

        var t0 = tmp0 * m11 + tmp3 * m21 + tmp4 * m31 -
                 (tmp1 * m11 + tmp2 * m21 + tmp5 * m31);
        var t1 = tmp1 * m01 + tmp6 * m21 + tmp9 * m31 -
                 (tmp0 * m01 + tmp7 * m21 + tmp8 * m31);
        var t2 = tmp2 * m01 + tmp7 * m11 + tmp10 * m31 -
                 (tmp3 * m01 + tmp6 * m11 + tmp11 * m31);
        var t3 = tmp5 * m01 + tmp8 * m11 + tmp11 * m21 -
                 (tmp4 * m01 + tmp9 * m11 + tmp10 * m21);

        var d = 1.0f / (m00 * t0 + m10 * t1 + m20 * t2 + m30 * t3);

        dst[0] = d * t0;
        dst[1] = d * t1;
        dst[2] = d * t2;
        dst[3] = d * t3;
        dst[4] = d * (tmp1 * m10 + tmp2 * m20 + tmp5 * m30 -
                      (tmp0 * m10 + tmp3 * m20 + tmp4 * m30));
        dst[5] = d * (tmp0 * m00 + tmp7 * m20 + tmp8 * m30 -
                      (tmp1 * m00 + tmp6 * m20 + tmp9 * m30));
        dst[6] = d * (tmp3 * m00 + tmp6 * m10 + tmp11 * m30 -
                      (tmp2 * m00 + tmp7 * m10 + tmp10 * m30));
        dst[7] = d * (tmp4 * m00 + tmp9 * m10 + tmp10 * m20 -
                      (tmp5 * m00 + tmp8 * m10 + tmp11 * m20));
        dst[8] = d * (m20 * m31 * m13 + m30 * m11 * m23 + m10 * m21 * m33 -
                      (m30 * m21 * m13 + m20 * m11 * m33 + m10 * m31 * m23));
        dst[9] = d * (m30 * m01 * m23 + m00 * m31 * m23 + m20 * m01 * m33 -
                      (m00 * m21 * m33 + m30 * m21 * m03 + m20 * m31 * m03));
        dst[10] = d * (m10 * m01 * m33 + m00 * m11 * m33 + m30 * m11 * m03 -
                       (m00 * m31 * m13 + m10 * m31 * m03 + m30 * m01 * m13));
        dst[11] = d * (m00 * m21 * m13 + m20 * m01 * m13 + m10 * m01 * m23 -
                       (m20 * m11 * m03 + m00 * m11 * m23 + m10 * m21 * m03));
        dst[12] = d * (m20 * m31 * m12 + m30 * m11 * m22 + m10 * m21 * m32 -
                       (m30 * m21 * m12 + m20 * m11 * m32 + m10 * m31 * m22));
        dst[13] = d * (m30 * m01 * m22 + m00 * m31 * m22 + m20 * m01 * m32 -
                       (m00 * m21 * m32 + m30 * m21 * m02 + m20 * m31 * m02));
        dst[14] = d * (m10 * m01 * m32 + m00 * m11 * m32 + m30 * m11 * m02 -
                       (m00 * m31 * m12 + m10 * m31 * m02 + m30 * m01 * m12));
        dst[15] = d * (m00 * m21 * m12 + m20 * m01 * m12 + m10 * m01 * m22 -
                       (m20 * m11 * m02 + m00 * m11 * m22 + m10 * m21 * m02));
        return dst;
    }

    /// <summary>
    /// Compute the determinant of a matrix
    /// </summary>
    public static float Determinant(Mat4 m)
    {
        var m00 = m[0 * 4 + 0];
        var m01 = m[0 * 4 + 1];
        var m02 = m[0 * 4 + 2];
        var m03 = m[0 * 4 + 3];
        var m10 = m[1 * 4 + 0];
        var m11 = m[1 * 4 + 1];
        var m12 = m[1 * 4 + 2];
        var m13 = m[1 * 4 + 3];
        var m20 = m[2 * 4 + 0];
        var m21 = m[2 * 4 + 1];
        var m22 = m[2 * 4 + 2];
        var m23 = m[2 * 4 + 3];
        var m30 = m[3 * 4 + 0];
        var m31 = m[3 * 4 + 1];
        var m32 = m[3 * 4 + 2];
        var m33 = m[3 * 4 + 3];

        var tmp0 = m22 * m33;
        var tmp1 = m32 * m23;
        var tmp2 = m12 * m33;
        var tmp3 = m32 * m13;
        var tmp4 = m12 * m23;
        var tmp5 = m22 * m13;

        var t0 = tmp0 * m11 + tmp3 * m21 + tmp4 * m31 -
                 (tmp1 * m11 + tmp2 * m21 + tmp5 * m31);
        var t1 = /*tmp1 * m01 +*/ m02 * m32 * m21 + m03 * m22 * m31 -
                 (tmp0 * m01 + m02 * m23 * m31 + m03 * m32 * m21);
        var t2 = m12 * m01 * m33 + m02 * m32 * m11 + m13 * m32 * m01 -
                 (m12 * m31 * m03 + m02 * m11 * m33 + m13 * m01 * m32);
        var t3 = m13 * m22 * m01 + m03 * m12 * m21 + m23 * m02 * m11 -
                 (m13 * m01 * m22 + m03 * m21 * m12 + m23 * m11 * m02);

        return m00 * t0 + m10 * t1 + m20 * t2 + m30 * t3;
    }

    /// <summary>
    /// Computes the inverse of a 4-by-4 matrix. (same as inverse)
    /// </summary>
    public static Mat4 Invert(Mat4 m, Mat4? dst = default) => Inverse(m, dst);

    /// <summary>
    /// Multiplies two 4-by-4 matrices with a on the left and b on the right
    /// </summary>
    public static Mat4 Multiply(Mat4 a, Mat4 b, Mat4? dst = default)
    {
        dst = dst ?? new Mat4();
        var a00 = a[0];
        var a01 = a[1];
        var a02 = a[2];
        var a03 = a[3];
        var a10 = a[4 + 0];
        var a11 = a[4 + 1];
        var a12 = a[4 + 2];
        var a13 = a[4 + 3];
        var a20 = a[8 + 0];
        var a21 = a[8 + 1];
        var a22 = a[8 + 2];
        var a23 = a[8 + 3];
        var a30 = a[12 + 0];
        var a31 = a[12 + 1];
        var a32 = a[12 + 2];
        var a33 = a[12 + 3];
        var b00 = b[0];
        var b01 = b[1];
        var b02 = b[2];
        var b03 = b[3];
        var b10 = b[4 + 0];
        var b11 = b[4 + 1];
        var b12 = b[4 + 2];
        var b13 = b[4 + 3];
        var b20 = b[8 + 0];
        var b21 = b[8 + 1];
        var b22 = b[8 + 2];
        var b23 = b[8 + 3];
        var b30 = b[12 + 0];
        var b31 = b[12 + 1];
        var b32 = b[12 + 2];
        var b33 = b[12 + 3];

        dst[0] = a00 * b00 + a10 * b01 + a20 * b02 + a30 * b03;
        dst[1] = a01 * b00 + a11 * b01 + a21 * b02 + a31 * b03;
        dst[2] = a02 * b00 + a12 * b01 + a22 * b02 + a32 * b03;
        dst[3] = a03 * b00 + a13 * b01 + a23 * b02 + a33 * b03;
        dst[4] = a00 * b10 + a10 * b11 + a20 * b12 + a30 * b13;
        dst[5] = a01 * b10 + a11 * b11 + a21 * b12 + a31 * b13;
        dst[6] = a02 * b10 + a12 * b11 + a22 * b12 + a32 * b13;
        dst[7] = a03 * b10 + a13 * b11 + a23 * b12 + a33 * b13;
        dst[8] = a00 * b20 + a10 * b21 + a20 * b22 + a30 * b23;
        dst[9] = a01 * b20 + a11 * b21 + a21 * b22 + a31 * b23;
        dst[10] = a02 * b20 + a12 * b21 + a22 * b22 + a32 * b23;
        dst[11] = a03 * b20 + a13 * b21 + a23 * b22 + a33 * b23;
        dst[12] = a00 * b30 + a10 * b31 + a20 * b32 + a30 * b33;
        dst[13] = a01 * b30 + a11 * b31 + a21 * b32 + a31 * b33;
        dst[14] = a02 * b30 + a12 * b31 + a22 * b32 + a32 * b33;
        dst[15] = a03 * b30 + a13 * b31 + a23 * b32 + a33 * b33;
        return dst;
    }

    /// <summary>
    /// Multiplies two 4-by-4 matrices with a on the left and b on the right (same as multiply)
    /// </summary>
    public static Mat4 Mul(Mat4 a, Mat4 b, Mat4? dst = default) => Multiply(a, b, dst);

    /// <summary>
    /// Sets the translation component of a 4-by-4 matrix to the given vector.
    /// </summary>
    public static Mat4 SetTranslation(Mat4 a, Vec3 v, Mat4? dst = default)
    {
        dst = dst ?? Identity();
        if (a != dst)
        {
            dst[0] = a[0]; dst[1] = a[1]; dst[2] = a[2]; dst[3] = a[3];
            dst[4] = a[4]; dst[5] = a[5]; dst[6] = a[6]; dst[7] = a[7];
            dst[8] = a[8]; dst[9] = a[9]; dst[10] = a[10]; dst[11] = a[11];
        }
        dst[12] = v[0];
        dst[13] = v[1];
        dst[14] = v[2];
        dst[15] = 1;
        return dst;
    }

    /// <summary>
    /// Returns the translation component of a 4-by-4 matrix as a vector with 3 entries.
    /// </summary>
    public static Vec3 GetTranslation(Mat4 m, Vec3? dst = default)
    {
        dst = dst ?? Vec3.Create();
        dst[0] = m[12];
        dst[1] = m[13];
        dst[2] = m[14];
        return dst;
    }

    /// <summary>
    /// Returns an axis of a 4x4 matrix as a vector with 3 entries
    /// </summary>
    public static Vec3 GetAxis(Mat4 m, int axis, Vec3? dst = default)
    {
        dst = dst ?? Vec3.Create();
        var off = axis * 4;
        dst[0] = m[off + 0];
        dst[1] = m[off + 1];
        dst[2] = m[off + 2];
        return dst;
    }

    /// <summary>
    /// Sets an axis of a 4x4 matrix as a vector with 3 entries
    /// </summary>
    public static Mat4 SetAxis(Mat4 m, Vec3 v, int axis, Mat4? dst = default)
    {
        dst = dst == m ? m : Copy(m, dst);

        var off = axis * 4;
        dst[off + 0] = v[0];
        dst[off + 1] = v[1];
        dst[off + 2] = v[2];
        return dst;
    }

    /// <summary>
    /// Returns the "3d" scaling component of the matrix
    /// </summary>
    public static Vec3 GetScaling(Mat4 m, Vec3? dst = default)
    {
        dst = dst ?? Vec3.Create();
        var xx = m[0];
        var xy = m[1];
        var xz = m[2];
        var yx = m[4];
        var yy = m[5];
        var yz = m[6];
        var zx = m[8];
        var zy = m[9];
        var zz = m[10];

        dst[0] = MathF.Sqrt(xx * xx + xy * xy + xz * xz);
        dst[1] = MathF.Sqrt(yx * yx + yy * yy + yz * yz);
        dst[2] = MathF.Sqrt(zx * zx + zy * zy + zz * zz);
        return dst;
    }

    /// <summary>
    /// Computes a 4-by-4 perspective transformation matrix given the angular height
    /// of the frustum, the aspect ratio, and the near and far clipping planes.
    /// </summary>
    public static Mat4 Perspective(float fieldOfViewYInRadians, float aspect, float zNear, float zFar, Mat4? dst = default)
    {
        dst = dst ?? new Mat4();
        var f = MathF.Tan(MathF.PI * 0.5f - 0.5f * fieldOfViewYInRadians);
        dst[0] = f / aspect;
        dst[1] = 0;
        dst[2] = 0;
        dst[3] = 0;
        dst[4] = 0;
        dst[5] = f;
        dst[6] = 0;
        dst[7] = 0;
        dst[8] = 0;
        dst[9] = 0;
        dst[11] = -1;
        dst[12] = 0;
        dst[13] = 0;
        dst[15] = 0;
        if (float.IsFinite(zFar))
        {
            var rangeInv = 1.0f / (zNear - zFar);
            dst[10] = zFar * rangeInv;
            dst[14] = zFar * zNear * rangeInv;
        }
        else
        {
            dst[10] = -1;
            dst[14] = -zNear;
        }
        return dst;
    }

    /// <summary>
    /// Computes a 4-by-4 reverse-z perspective transformation matrix.
    /// </summary>
    public static Mat4 PerspectiveReverseZ(float fieldOfViewYInRadians, float aspect, float zNear, float zFar = float.PositiveInfinity, Mat4? dst = default)
    {
        dst = dst ?? new Mat4();
        var f = 1.0f / MathF.Tan(fieldOfViewYInRadians * 0.5f);
        dst[0] = f / aspect;
        dst[1] = 0;
        dst[2] = 0;
        dst[3] = 0;
        dst[4] = 0;
        dst[5] = f;
        dst[6] = 0;
        dst[7] = 0;
        dst[8] = 0;
        dst[9] = 0;
        dst[11] = -1;
        dst[12] = 0;
        dst[13] = 0;
        dst[15] = 0;
        if (float.IsPositiveInfinity(zFar))
        {
            dst[10] = 0;
            dst[14] = zNear;
        }
        else
        {
            var rangeInv = 1.0f / (zFar - zNear);
            dst[10] = zNear * rangeInv;
            dst[14] = zFar * zNear * rangeInv;
        }
        return dst;
    }

    /// <summary>
    /// Computes a 4-by-4 orthogonal transformation matrix.
    /// </summary>
    public static Mat4 Ortho(float left, float right, float bottom, float top, float near, float far, Mat4? dst = default)
    {
        dst = dst ?? new Mat4();
        dst[0] = 2 / (right - left);
        dst[1] = 0;
        dst[2] = 0;
        dst[3] = 0;
        dst[4] = 0;
        dst[5] = 2 / (top - bottom);
        dst[6] = 0;
        dst[7] = 0;
        dst[8] = 0;
        dst[9] = 0;
        dst[10] = 1 / (near - far);
        dst[11] = 0;
        dst[12] = (right + left) / (left - right);
        dst[13] = (top + bottom) / (bottom - top);
        dst[14] = near / (near - far);
        dst[15] = 1;
        return dst;
    }

    /// <summary>
    /// Computes a 4-by-4 perspective transformation matrix given the left, right,
    /// top, bottom, near and far clipping planes.
    /// </summary>
    public static Mat4 Frustum(float left, float right, float bottom, float top, float near, float far, Mat4? dst = default)
    {
        dst = dst ?? new Mat4();
        var dx = right - left;
        var dy = top - bottom;
        var dz = near - far;
        dst[0] = 2 * near / dx;
        dst[1] = 0;
        dst[2] = 0;
        dst[3] = 0;
        dst[4] = 0;
        dst[5] = 2 * near / dy;
        dst[6] = 0;
        dst[7] = 0;
        dst[8] = (left + right) / dx;
        dst[9] = (top + bottom) / dy;
        dst[10] = far / dz;
        dst[11] = -1;
        dst[12] = 0;
        dst[13] = 0;
        dst[14] = near * far / dz;
        dst[15] = 0;
        return dst;
    }

    /// <summary>
    /// Computes a 4-by-4 reverse-z perspective transformation matrix given the left, right,
    /// top, bottom, near and far clipping planes.
    /// </summary>
    public static Mat4 FrustumReverseZ(float left, float right, float bottom, float top, float near, float far = float.PositiveInfinity, Mat4? dst = default)
    {
        dst = dst ?? new Mat4();
        var dx = right - left;
        var dy = top - bottom;
        dst[0] = 2 * near / dx;
        dst[1] = 0;
        dst[2] = 0;
        dst[3] = 0;
        dst[4] = 0;
        dst[5] = 2 * near / dy;
        dst[6] = 0;
        dst[7] = 0;
        dst[8] = (left + right) / dx;
        dst[9] = (top + bottom) / dy;
        dst[11] = -1;
        dst[12] = 0;
        dst[13] = 0;
        dst[15] = 0;
        if (float.IsPositiveInfinity(far))
        {
            dst[10] = 0;
            dst[14] = near;
        }
        else
        {
            var rangeInv = 1.0f / (far - near);
            dst[10] = near * rangeInv;
            dst[14] = far * near * rangeInv;
        }
        return dst;
    }

    private static readonly Vec3 XAxis = Vec3.Create();
    private static readonly Vec3 YAxis = Vec3.Create();
    private static readonly Vec3 ZAxis = Vec3.Create();

    /// <summary>
    /// Computes a 4-by-4 aim transformation.
    /// This is a matrix which positions an object aiming down positive Z toward the target.
    /// Note: this is **NOT** the inverse of lookAt as lookAt looks at negative Z.
    /// </summary>
    public static Mat4 Aim(Vec3 position, Vec3 target, Vec3 up, Mat4? dst = default)
    {
        dst = dst ?? new Mat4();
        Vec3.Normalize(Vec3.Subtract(target, position, ZAxis), ZAxis);
        Vec3.Normalize(Vec3.Cross(up, ZAxis, XAxis), XAxis);
        Vec3.Normalize(Vec3.Cross(ZAxis, XAxis, YAxis), YAxis);
        dst[0] = XAxis[0]; dst[1] = XAxis[1]; dst[2] = XAxis[2]; dst[3] = 0;
        dst[4] = YAxis[0]; dst[5] = YAxis[1]; dst[6] = YAxis[2]; dst[7] = 0;
        dst[8] = ZAxis[0]; dst[9] = ZAxis[1]; dst[10] = ZAxis[2]; dst[11] = 0;
        dst[12] = position[0]; dst[13] = position[1]; dst[14] = position[2]; dst[15] = 1;
        return dst;
    }

    /// <summary>
    /// Computes a 4-by-4 camera aim transformation.
    /// This is a matrix which positions an object aiming down negative Z toward the target.
    /// Note: this is the inverse of `lookAt`
    /// </summary>
    public static Mat4 CameraAim(Vec3 eye, Vec3 target, Vec3 up, Mat4? dst = default)
    {
        dst = dst ?? new Mat4();
        Vec3.Normalize(Vec3.Subtract(eye, target, ZAxis), ZAxis);
        Vec3.Normalize(Vec3.Cross(up, ZAxis, XAxis), XAxis);
        Vec3.Normalize(Vec3.Cross(ZAxis, XAxis, YAxis), YAxis);
        dst[0] = XAxis[0]; dst[1] = XAxis[1]; dst[2] = XAxis[2]; dst[3] = 0;
        dst[4] = YAxis[0]; dst[5] = YAxis[1]; dst[6] = YAxis[2]; dst[7] = 0;
        dst[8] = ZAxis[0]; dst[9] = ZAxis[1]; dst[10] = ZAxis[2]; dst[11] = 0;
        dst[12] = eye[0]; dst[13] = eye[1]; dst[14] = eye[2]; dst[15] = 1;
        return dst;
    }

    /// <summary>
    /// Computes a 4-by-4 view transformation.
    /// This is a view matrix which transforms all other objects
    /// to be in the space of the view defined by the parameters.
    /// </summary>
    public static Mat4 LookAt(Vec3 eye, Vec3 target, Vec3 up, Mat4? dst = default)
    {
        dst = dst ?? new Mat4();
        Vec3.Normalize(Vec3.Subtract(eye, target, ZAxis), ZAxis);
        Vec3.Normalize(Vec3.Cross(up, ZAxis, XAxis), XAxis);
        Vec3.Normalize(Vec3.Cross(ZAxis, XAxis, YAxis), YAxis);
        dst[0] = XAxis[0]; dst[1] = YAxis[0]; dst[2] = ZAxis[0]; dst[3] = 0;
        dst[4] = XAxis[1]; dst[5] = YAxis[1]; dst[6] = ZAxis[1]; dst[7] = 0;
        dst[8] = XAxis[2]; dst[9] = YAxis[2]; dst[10] = ZAxis[2]; dst[11] = 0;
        dst[12] = -(XAxis[0] * eye[0] + XAxis[1] * eye[1] + XAxis[2] * eye[2]);
        dst[13] = -(YAxis[0] * eye[0] + YAxis[1] * eye[1] + YAxis[2] * eye[2]);
        dst[14] = -(ZAxis[0] * eye[0] + ZAxis[1] * eye[1] + ZAxis[2] * eye[2]);
        dst[15] = 1;
        return dst;
    }

    /// <summary>
    /// Creates a 4-by-4 matrix which translates by the given vector v.
    /// </summary>
    public static Mat4 Translation(Vec3 v, Mat4? dst = default)
    {
        dst = dst ?? new Mat4();
        dst[0] = 1; dst[1] = 0; dst[2] = 0; dst[3] = 0;
        dst[4] = 0; dst[5] = 1; dst[6] = 0; dst[7] = 0;
        dst[8] = 0; dst[9] = 0; dst[10] = 1; dst[11] = 0;
        dst[12] = v[0]; dst[13] = v[1]; dst[14] = v[2]; dst[15] = 1;
        return dst;
    }

    /// <summary>
    /// Translates the given 4-by-4 matrix by the given vector v.
    /// </summary>
    public static Mat4 Translate(Mat4 m, Vec3 v, Mat4? dst = default)
    {
        dst = dst ?? new Mat4();
        var v0 = v[0];
        var v1 = v[1];
        var v2 = v[2];
        var m00 = m[0];
        var m01 = m[1];
        var m02 = m[2];
        var m03 = m[3];
        var m10 = m[1 * 4 + 0];
        var m11 = m[1 * 4 + 1];
        var m12 = m[1 * 4 + 2];
        var m13 = m[1 * 4 + 3];
        var m20 = m[2 * 4 + 0];
        var m21 = m[2 * 4 + 1];
        var m22 = m[2 * 4 + 2];
        var m23 = m[2 * 4 + 3];
        var m30 = m[3 * 4 + 0];
        var m31 = m[3 * 4 + 1];
        var m32 = m[3 * 4 + 2];
        var m33 = m[3 * 4 + 3];

        if (m != dst)
        {
            dst[0] = m00;
            dst[1] = m01;
            dst[2] = m02;
            dst[3] = m03;
            dst[4] = m10;
            dst[5] = m11;
            dst[6] = m12;
            dst[7] = m13;
            dst[8] = m20;
            dst[9] = m21;
            dst[10] = m22;
            dst[11] = m23;
        }

        dst[12] = m00 * v0 + m10 * v1 + m20 * v2 + m30;
        dst[13] = m01 * v0 + m11 * v1 + m21 * v2 + m31;
        dst[14] = m02 * v0 + m12 * v1 + m22 * v2 + m32;
        dst[15] = m03 * v0 + m13 * v1 + m23 * v2 + m33;
        return dst;
    }

    /// <summary>
    /// Creates a 4-by-4 matrix which rotates around the x-axis by the given angle.
    /// </summary>
    public static Mat4 RotationX(float angleInRadians, Mat4? dst = default)
    {
        dst = dst ?? new Mat4();
        var c = MathF.Cos(angleInRadians);
        var s = MathF.Sin(angleInRadians);
        dst[0] = 1; dst[1] = 0; dst[2] = 0; dst[3] = 0;
        dst[4] = 0; dst[5] = c; dst[6] = s; dst[7] = 0;
        dst[8] = 0; dst[9] = -s; dst[10] = c; dst[11] = 0;
        dst[12] = 0; dst[13] = 0; dst[14] = 0; dst[15] = 1;
        return dst;
    }

    /// <summary>
    /// Rotates the given 4-by-4 matrix around the x-axis by the given angle.
    /// </summary>
    public static Mat4 RotateX(Mat4 m, float angleInRadians, Mat4? dst = default)
    {
        dst = dst ?? new Mat4();
        var m10 = m[4];
        var m11 = m[5];
        var m12 = m[6];
        var m13 = m[7];
        var m20 = m[8];
        var m21 = m[9];
        var m22 = m[10];
        var m23 = m[11];
        var c = MathF.Cos(angleInRadians);
        var s = MathF.Sin(angleInRadians);

        dst[4] = c * m10 + s * m20;
        dst[5] = c * m11 + s * m21;
        dst[6] = c * m12 + s * m22;
        dst[7] = c * m13 + s * m23;
        dst[8] = c * m20 - s * m10;
        dst[9] = c * m21 - s * m11;
        dst[10] = c * m22 - s * m12;
        dst[11] = c * m23 - s * m13;

        if (m != dst)
        {
            dst[0] = m[0];
            dst[1] = m[1];
            dst[2] = m[2];
            dst[3] = m[3];
            dst[12] = m[12];
            dst[13] = m[13];
            dst[14] = m[14];
            dst[15] = m[15];
        }
        return dst;
    }

    /// <summary>
    /// Creates a 4-by-4 matrix which rotates around the y-axis by the given angle.
    /// </summary>
    public static Mat4 RotationY(float angleInRadians, Mat4? dst = default)
    {
        dst = dst ?? new Mat4();
        var c = MathF.Cos(angleInRadians);
        var s = MathF.Sin(angleInRadians);
        dst[0] = c; dst[1] = 0; dst[2] = -s; dst[3] = 0;
        dst[4] = 0; dst[5] = 1; dst[6] = 0; dst[7] = 0;
        dst[8] = s; dst[9] = 0; dst[10] = c; dst[11] = 0;
        dst[12] = 0; dst[13] = 0; dst[14] = 0; dst[15] = 1;
        return dst;
    }

    /// <summary>
    /// Rotates the given 4-by-4 matrix around the y-axis by the given angle.
    /// </summary>
    public static Mat4 RotateY(Mat4 m, float angleInRadians, Mat4? dst = default)
    {
        dst = dst ?? new Mat4();
        var m00 = m[0 * 4 + 0];
        var m01 = m[0 * 4 + 1];
        var m02 = m[0 * 4 + 2];
        var m03 = m[0 * 4 + 3];
        var m20 = m[2 * 4 + 0];
        var m21 = m[2 * 4 + 1];
        var m22 = m[2 * 4 + 2];
        var m23 = m[2 * 4 + 3];
        var c = MathF.Cos(angleInRadians);
        var s = MathF.Sin(angleInRadians);

        dst[0] = c * m00 - s * m20;
        dst[1] = c * m01 - s * m21;
        dst[2] = c * m02 - s * m22;
        dst[3] = c * m03 - s * m23;
        dst[8] = c * m20 + s * m00;
        dst[9] = c * m21 + s * m01;
        dst[10] = c * m22 + s * m02;
        dst[11] = c * m23 + s * m03;

        if (m != dst)
        {
            dst[4] = m[4];
            dst[5] = m[5];
            dst[6] = m[6];
            dst[7] = m[7];
            dst[12] = m[12];
            dst[13] = m[13];
            dst[14] = m[14];
            dst[15] = m[15];
        }
        return dst;
    }

    /// <summary>
    /// Creates a 4-by-4 matrix which rotates around the z-axis by the given angle.
    /// </summary>
    public static Mat4 RotationZ(float angleInRadians, Mat4? dst = default)
    {
        dst = dst ?? new Mat4();
        var c = MathF.Cos(angleInRadians);
        var s = MathF.Sin(angleInRadians);
        dst[0] = c; dst[1] = s; dst[2] = 0; dst[3] = 0;
        dst[4] = -s; dst[5] = c; dst[6] = 0; dst[7] = 0;
        dst[8] = 0; dst[9] = 0; dst[10] = 1; dst[11] = 0;
        dst[12] = 0; dst[13] = 0; dst[14] = 0; dst[15] = 1;
        return dst;
    }

    /// <summary>
    /// Rotates the given 4-by-4 matrix around the z-axis by the given angle.
    /// </summary>
    public static Mat4 RotateZ(Mat4 m, float angleInRadians, Mat4? dst = default)
    {
        dst = dst ?? new Mat4();
        var m00 = m[0 * 4 + 0];
        var m01 = m[0 * 4 + 1];
        var m02 = m[0 * 4 + 2];
        var m03 = m[0 * 4 + 3];
        var m10 = m[1 * 4 + 0];
        var m11 = m[1 * 4 + 1];
        var m12 = m[1 * 4 + 2];
        var m13 = m[1 * 4 + 3];
        var c = MathF.Cos(angleInRadians);
        var s = MathF.Sin(angleInRadians);

        dst[0] = c * m00 + s * m10;
        dst[1] = c * m01 + s * m11;
        dst[2] = c * m02 + s * m12;
        dst[3] = c * m03 + s * m13;
        dst[4] = c * m10 - s * m00;
        dst[5] = c * m11 - s * m01;
        dst[6] = c * m12 - s * m02;
        dst[7] = c * m13 - s * m03;

        if (m != dst)
        {
            dst[8] = m[8];
            dst[9] = m[9];
            dst[10] = m[10];
            dst[11] = m[11];
            dst[12] = m[12];
            dst[13] = m[13];
            dst[14] = m[14];
            dst[15] = m[15];
        }
        return dst;
    }

    /// <summary>
    /// Creates a 4-by-4 matrix which rotates around the given axis by the given angle.
    /// </summary>
    public static Mat4 AxisRotation(Vec3 axis, float angleInRadians, Mat4? dst = default)
    {
        dst = dst ?? new Mat4();
        var x = axis[0];
        var y = axis[1];
        var z = axis[2];
        var n = MathF.Sqrt(x * x + y * y + z * z);
        x /= n;
        y /= n;
        z /= n;
        var xx = x * x;
        var yy = y * y;
        var zz = z * z;
        var c = MathF.Cos(angleInRadians);
        var s = MathF.Sin(angleInRadians);
        var oneMinusCosine = 1 - c;

        dst[0] = xx + (1 - xx) * c;
        dst[1] = x * y * oneMinusCosine + z * s;
        dst[2] = x * z * oneMinusCosine - y * s;
        dst[3] = 0;
        dst[4] = x * y * oneMinusCosine - z * s;
        dst[5] = yy + (1 - yy) * c;
        dst[6] = y * z * oneMinusCosine + x * s;
        dst[7] = 0;
        dst[8] = x * z * oneMinusCosine + y * s;
        dst[9] = y * z * oneMinusCosine - x * s;
        dst[10] = zz + (1 - zz) * c;
        dst[11] = 0;
        dst[12] = 0;
        dst[13] = 0;
        dst[14] = 0;
        dst[15] = 1;
        return dst;
    }

    /// <summary>
    /// Creates a 4-by-4 matrix which rotates around the given axis by the given angle. (same as axisRotation)
    /// </summary>
    public static Mat4 Rotation(Vec3 axis, float angleInRadians, Mat4? dst = default) => AxisRotation(axis, angleInRadians, dst);

    /// <summary>
    /// Rotates the given 4-by-4 matrix around the given axis by the given angle.
    /// </summary>
    public static Mat4 AxisRotate(Mat4 m, Vec3 axis, float angleInRadians, Mat4? dst = default)
    {
        dst = dst ?? new Mat4();
        var x = axis[0];
        var y = axis[1];
        var z = axis[2];
        var n = MathF.Sqrt(x * x + y * y + z * z);
        x /= n;
        y /= n;
        z /= n;
        var xx = x * x;
        var yy = y * y;
        var zz = z * z;
        var c = MathF.Cos(angleInRadians);
        var s = MathF.Sin(angleInRadians);
        var oneMinusCosine = 1 - c;

        var r00 = xx + (1 - xx) * c;
        var r01 = x * y * oneMinusCosine + z * s;
        var r02 = x * z * oneMinusCosine - y * s;
        var r10 = x * y * oneMinusCosine - z * s;
        var r11 = yy + (1 - yy) * c;
        var r12 = y * z * oneMinusCosine + x * s;
        var r20 = x * z * oneMinusCosine + y * s;
        var r21 = y * z * oneMinusCosine - x * s;
        var r22 = zz + (1 - zz) * c;

        var m00 = m[0];
        var m01 = m[1];
        var m02 = m[2];
        var m03 = m[3];
        var m10 = m[4];
        var m11 = m[5];
        var m12 = m[6];
        var m13 = m[7];
        var m20 = m[8];
        var m21 = m[9];
        var m22 = m[10];
        var m23 = m[11];

        dst[0] = r00 * m00 + r01 * m10 + r02 * m20;
        dst[1] = r00 * m01 + r01 * m11 + r02 * m21;
        dst[2] = r00 * m02 + r01 * m12 + r02 * m22;
        dst[3] = r00 * m03 + r01 * m13 + r02 * m23;
        dst[4] = r10 * m00 + r11 * m10 + r12 * m20;
        dst[5] = r10 * m01 + r11 * m11 + r12 * m21;
        dst[6] = r10 * m02 + r11 * m12 + r12 * m22;
        dst[7] = r10 * m03 + r11 * m13 + r12 * m23;
        dst[8] = r20 * m00 + r21 * m10 + r22 * m20;
        dst[9] = r20 * m01 + r21 * m11 + r22 * m21;
        dst[10] = r20 * m02 + r21 * m12 + r22 * m22;
        dst[11] = r20 * m03 + r21 * m13 + r22 * m23;

        if (m != dst)
        {
            dst[12] = m[12];
            dst[13] = m[13];
            dst[14] = m[14];
            dst[15] = m[15];
        }
        return dst;
    }

    /// <summary>
    /// Rotates the given 4-by-4 matrix around the given axis by the given angle. (same as axisRotate)
    /// </summary>
    public static Mat4 Rotate(Mat4 m, Vec3 axis, float angleInRadians, Mat4? dst = default) => AxisRotate(m, axis, angleInRadians, dst);

    /// <summary>
    /// Creates a 4-by-4 matrix which scales in each dimension by an amount given by
    /// the corresponding entry in the given vector.
    /// </summary>
    public static Mat4 Scaling(Vec3 v, Mat4? dst = default)
    {
        dst = dst ?? new Mat4();
        dst[0] = v[0]; dst[1] = 0; dst[2] = 0; dst[3] = 0;
        dst[4] = 0; dst[5] = v[1]; dst[6] = 0; dst[7] = 0;
        dst[8] = 0; dst[9] = 0; dst[10] = v[2]; dst[11] = 0;
        dst[12] = 0; dst[13] = 0; dst[14] = 0; dst[15] = 1;
        return dst;
    }

    /// <summary>
    /// Scales the given 4-by-4 matrix in each dimension by an amount
    /// given by the corresponding entry in the given vector.
    /// </summary>
    public static Mat4 Scale(Mat4 m, Vec3 v, Mat4? dst = default)
    {
        dst = dst ?? new Mat4();
        var v0 = v[0];
        var v1 = v[1];
        var v2 = v[2];

        dst[0] = v0 * m[0 * 4 + 0];
        dst[1] = v0 * m[0 * 4 + 1];
        dst[2] = v0 * m[0 * 4 + 2];
        dst[3] = v0 * m[0 * 4 + 3];
        dst[4] = v1 * m[1 * 4 + 0];
        dst[5] = v1 * m[1 * 4 + 1];
        dst[6] = v1 * m[1 * 4 + 2];
        dst[7] = v1 * m[1 * 4 + 3];
        dst[8] = v2 * m[2 * 4 + 0];
        dst[9] = v2 * m[2 * 4 + 1];
        dst[10] = v2 * m[2 * 4 + 2];
        dst[11] = v2 * m[2 * 4 + 3];

        if (m != dst)
        {
            dst[12] = m[12];
            dst[13] = m[13];
            dst[14] = m[14];
            dst[15] = m[15];
        }
        return dst;
    }

    /// <summary>
    /// Creates a 4-by-4 matrix which scales a uniform amount in each dimension.
    /// </summary>
    public static Mat4 UniformScaling(float s, Mat4? dst = default)
    {
        dst = dst ?? new Mat4();
        dst[0] = s; dst[1] = 0; dst[2] = 0; dst[3] = 0;
        dst[4] = 0; dst[5] = s; dst[6] = 0; dst[7] = 0;
        dst[8] = 0; dst[9] = 0; dst[10] = s; dst[11] = 0;
        dst[12] = 0; dst[13] = 0; dst[14] = 0; dst[15] = 1;
        return dst;
    }

    /// <summary>
    /// Scales the given 4-by-4 matrix in each dimension by a uniform scale.
    /// </summary>
    public static Mat4 UniformScale(Mat4 m, float s, Mat4? dst = default)
    {
        dst = dst ?? new Mat4();
        dst[0] = s * m[0 * 4 + 0];
        dst[1] = s * m[0 * 4 + 1];
        dst[2] = s * m[0 * 4 + 2];
        dst[3] = s * m[0 * 4 + 3];
        dst[4] = s * m[1 * 4 + 0];
        dst[5] = s * m[1 * 4 + 1];
        dst[6] = s * m[1 * 4 + 2];
        dst[7] = s * m[1 * 4 + 3];
        dst[8] = s * m[2 * 4 + 0];
        dst[9] = s * m[2 * 4 + 1];
        dst[10] = s * m[2 * 4 + 2];
        dst[11] = s * m[2 * 4 + 3];

        if (m != dst)
        {
            dst[12] = m[12];
            dst[13] = m[13];
            dst[14] = m[14];
            dst[15] = m[15];
        }
        return dst;
    }

    public override int GetHashCode()
    {
        // Simple hash combining all elements. Consider a more sophisticated approach if needed.
        return (int)(this[0] * this[1] * this[2] * this[3] *
                     this[4] * this[5] * this[6] * this[7] *
                     this[8] * this[9] * this[10] * this[11] *
                     this[12] * this[13] * this[14] * this[15]);
    }
}
using Microsoft.JSInterop;
using SpawnDev.BlazorJS.JSObjects;

namespace Blazor.WebGPU.Matrix;

public class Mat3 : Float32Array
{
    public Mat3(IJSInProcessObjectReference _ref) : base(_ref) { }

    private Mat3() : base(new Float32Array(12).JSRef!)
    {
        // Initialize padding elements to 0
        this[3] = 0;
        this[7] = 0;
        this[11] = 0;
    }

    /// <summary>
    /// Create a Mat3 from values
    /// </summary>
    public static Mat3 Create(
        float v0 = 1, float v1 = 0, float v2 = 0,
        float v3 = 0, float v4 = 1, float v5 = 0,
        float v6 = 0, float v7 = 0, float v8 = 1)
    {
        var dst = new Mat3();
        dst[0] = v0; dst[1] = v1; dst[2] = v2;
        dst[4] = v3; dst[5] = v4; dst[6] = v5;
        dst[8] = v6; dst[9] = v7; dst[10] = v8;
        return dst;
    }

    /// <summary>
    /// Sets the values of a Mat3
    /// </summary>
    public static Mat3 Set(
        float v0, float v1, float v2,
        float v3, float v4, float v5,
        float v6, float v7, float v8,
        Mat3? dst = default)
    {
        dst = dst ?? new Mat3();
        dst[0] = v0; dst[1] = v1; dst[2] = v2; dst[3] = 0;
        dst[4] = v3; dst[5] = v4; dst[6] = v5; dst[7] = 0;
        dst[8] = v6; dst[9] = v7; dst[10] = v8; dst[11] = 0;
        return dst;
    }

    /// <summary>
    /// Creates a Mat3 from the upper left 3x3 part of a Mat4
    /// </summary>
    public static Mat3 FromMat4(Mat4 m4, Mat3? dst = default)
    {
        dst = dst ?? new Mat3();
        dst[0] = m4[0]; dst[1] = m4[1]; dst[2] = m4[2]; dst[3] = 0;
        dst[4] = m4[4]; dst[5] = m4[5]; dst[6] = m4[6]; dst[7] = 0;
        dst[8] = m4[8]; dst[9] = m4[9]; dst[10] = m4[10]; dst[11] = 0;
        return dst;
    }

    /// <summary>
    /// Creates a Mat3 rotation matrix from a quaternion
    /// </summary>
    public static Mat3 FromQuat(Quat q, Mat3? dst = default)
    {
        dst = dst ?? new Mat3();
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

        return dst;
    }

    /// <summary>
    /// Negates a matrix.
    /// </summary>
    public static Mat3 Negate(Mat3 m, Mat3? dst = default)
    {
        dst = dst ?? new Mat3();
        dst[0] = -m[0]; dst[1] = -m[1]; dst[2] = -m[2];
        dst[4] = -m[4]; dst[5] = -m[5]; dst[6] = -m[6];
        dst[8] = -m[8]; dst[9] = -m[9]; dst[10] = -m[10];
        return dst;
    }

    /// <summary>
    /// multiply a matrix by a scalar matrix.
    /// </summary>
    public static Mat3 MultiplyScalar(Mat3 m, float s, Mat3? dst = default)
    {
        dst = dst ?? new Mat3();
        dst[0] = m[0] * s; dst[1] = m[1] * s; dst[2] = m[2] * s;
        dst[4] = m[4] * s; dst[5] = m[5] * s; dst[6] = m[6] * s;
        dst[8] = m[8] * s; dst[9] = m[9] * s; dst[10] = m[10] * s;
        return dst;
    }

    /// <summary>
    /// multiply a matrix by a scalar matrix. (same as multiplyScalar)
    /// </summary>
    public static Mat3 MulScalar(Mat3 m, float s, Mat3? dst = default) => MultiplyScalar(m, s, dst);

    /// <summary>
    /// add 2 matrices.
    /// </summary>
    public static Mat3 Add(Mat3 a, Mat3 b, Mat3? dst = default)
    {
        dst = dst ?? new Mat3();
        dst[0] = a[0] + b[0]; dst[1] = a[1] + b[1]; dst[2] = a[2] + b[2];
        dst[4] = a[4] + b[4]; dst[5] = a[5] + b[5]; dst[6] = a[6] + b[6];
        dst[8] = a[8] + b[8]; dst[9] = a[9] + b[9]; dst[10] = a[10] + b[10];
        return dst;
    }

    /// <summary>
    /// Copies a matrix. (same as clone)
    /// </summary>
    public static Mat3 Copy(Mat3 m, Mat3? dst = default)
    {
        dst = dst ?? new Mat3();
        dst[0] = m[0]; dst[1] = m[1]; dst[2] = m[2];
        dst[4] = m[4]; dst[5] = m[5]; dst[6] = m[6];
        dst[8] = m[8]; dst[9] = m[9]; dst[10] = m[10];
        return dst;
    }

    /// <summary>
    /// Copies a matrix (same as copy)
    /// </summary>
    public static Mat3 Clone(Mat3 m, Mat3? dst = default) => Copy(m, dst);

    /// <summary>
    /// Check if 2 matrices are approximately equal
    /// </summary>
    public static bool EqualsApproximately(Mat3 a, Mat3 b)
    {
        return MathF.Abs(a[0] - b[0]) < Utils.EPSILON &&
               MathF.Abs(a[1] - b[1]) < Utils.EPSILON &&
               MathF.Abs(a[2] - b[2]) < Utils.EPSILON &&
               MathF.Abs(a[4] - b[4]) < Utils.EPSILON &&
               MathF.Abs(a[5] - b[5]) < Utils.EPSILON &&
               MathF.Abs(a[6] - b[6]) < Utils.EPSILON &&
               MathF.Abs(a[8] - b[8]) < Utils.EPSILON &&
               MathF.Abs(a[9] - b[9]) < Utils.EPSILON &&
               MathF.Abs(a[10] - b[10]) < Utils.EPSILON;
    }

    /// <summary>
    /// Check if 2 matrices are exactly equal
    /// </summary>
    public static bool Equals(Mat3 a, Mat3 b)
    {
        return a[0] == b[0] &&
               a[1] == b[1] &&
               a[2] == b[2] &&
               a[4] == b[4] &&
               a[5] == b[5] &&
               a[6] == b[6] &&
               a[8] == b[8] &&
               a[9] == b[9] &&
               a[10] == b[10];
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj))
            return true;

        if (obj is null) return false;

        if (obj is Mat3 m3)
            return Equals(this, m3);

        return false;
    }

    /// <summary>
    /// Creates a 3-by-3 identity matrix.
    /// </summary>
    public static Mat3 Identity(Mat3? dst = default)
    {
        dst = dst ?? new Mat3();
        dst[0] = 1; dst[1] = 0; dst[2] = 0;
        dst[4] = 0; dst[5] = 1; dst[6] = 0;
        dst[8] = 0; dst[9] = 0; dst[10] = 1;
        return dst;
    }

    /// <summary>
    /// Takes the transpose of a matrix.
    /// </summary>
    public static Mat3 Transpose(Mat3 m, Mat3? dst = default)
    {
        dst = dst ?? new Mat3();
        if (dst == m)
        {
            float t;
            // 0 1 2
            // 4 5 6
            // 8 9 10

            t = m[1];
            m[1] = m[4];
            m[4] = t;

            t = m[2];
            m[2] = m[8];
            m[8] = t;

            t = m[6];
            m[6] = m[9];
            m[9] = t;

            return dst;
        }

        var m00 = m[0 * 4 + 0];
        var m01 = m[0 * 4 + 1];
        var m02 = m[0 * 4 + 2];
        var m10 = m[1 * 4 + 0];
        var m11 = m[1 * 4 + 1];
        var m12 = m[1 * 4 + 2];
        var m20 = m[2 * 4 + 0];
        var m21 = m[2 * 4 + 1];
        var m22 = m[2 * 4 + 2];

        dst[0] = m00; dst[1] = m10; dst[2] = m20;
        dst[4] = m01; dst[5] = m11; dst[6] = m21;
        dst[8] = m02; dst[9] = m12; dst[10] = m22;

        return dst;
    }

    /// <summary>
    /// Computes the inverse of a 3-by-3 matrix.
    /// </summary>
    public static Mat3 Inverse(Mat3 m, Mat3? dst = default)
    {
        dst = dst ?? new Mat3();
        var m00 = m[0 * 4 + 0];
        var m01 = m[0 * 4 + 1];
        var m02 = m[0 * 4 + 2];
        var m10 = m[1 * 4 + 0];
        var m11 = m[1 * 4 + 1];
        var m12 = m[1 * 4 + 2];
        var m20 = m[2 * 4 + 0];
        var m21 = m[2 * 4 + 1];
        var m22 = m[2 * 4 + 2];

        var b01 = m22 * m11 - m12 * m21;
        var b11 = -m22 * m10 + m12 * m20;
        var b21 = m21 * m10 - m11 * m20;

        var invDet = 1.0f / (m00 * b01 + m01 * b11 + m02 * b21);

        dst[0] = b01 * invDet;
        dst[1] = (-m22 * m01 + m02 * m21) * invDet;
        dst[2] = (m12 * m01 - m02 * m11) * invDet;
        dst[4] = b11 * invDet;
        dst[5] = (m22 * m00 - m02 * m20) * invDet;
        dst[6] = (-m12 * m00 + m02 * m10) * invDet;
        dst[8] = b21 * invDet;
        dst[9] = (-m21 * m00 + m01 * m20) * invDet;
        dst[10] = (m11 * m00 - m01 * m10) * invDet;

        return dst;
    }

    /// <summary>
    /// Compute the determinant of a matrix
    /// </summary>
    public static float Determinant(Mat3 m)
    {
        var m00 = m[0 * 4 + 0];
        var m01 = m[0 * 4 + 1];
        var m02 = m[0 * 4 + 2];
        var m10 = m[1 * 4 + 0];
        var m11 = m[1 * 4 + 1];
        var m12 = m[1 * 4 + 2];
        var m20 = m[2 * 4 + 0];
        var m21 = m[2 * 4 + 1];
        var m22 = m[2 * 4 + 2];

        return m00 * (m11 * m22 - m21 * m12) -
               m10 * (m01 * m22 - m21 * m02) +
               m20 * (m01 * m12 - m11 * m02);
    }

    /// <summary>
    /// Computes the inverse of a 3-by-3 matrix. (same as inverse)
    /// </summary>
    public static Mat3 Invert(Mat3 m, Mat3? dst = default) => Inverse(m, dst);

    /// <summary>
    /// Multiplies two 3-by-3 matrices with a on the left and b on the right
    /// </summary>
    public static Mat3 Multiply(Mat3 a, Mat3 b, Mat3? dst = default)
    {
        dst = dst ?? new Mat3();
        var a00 = a[0];
        var a01 = a[1];
        var a02 = a[2];
        var a10 = a[4 + 0];
        var a11 = a[4 + 1];
        var a12 = a[4 + 2];
        var a20 = a[8 + 0];
        var a21 = a[8 + 1];
        var a22 = a[8 + 2];
        var b00 = b[0];
        var b01 = b[1];
        var b02 = b[2];
        var b10 = b[4 + 0];
        var b11 = b[4 + 1];
        var b12 = b[4 + 2];
        var b20 = b[8 + 0];
        var b21 = b[8 + 1];
        var b22 = b[8 + 2];

        dst[0] = a00 * b00 + a10 * b01 + a20 * b02;
        dst[1] = a01 * b00 + a11 * b01 + a21 * b02;
        dst[2] = a02 * b00 + a12 * b01 + a22 * b02;
        dst[4] = a00 * b10 + a10 * b11 + a20 * b12;
        dst[5] = a01 * b10 + a11 * b11 + a21 * b12;
        dst[6] = a02 * b10 + a12 * b11 + a22 * b12;
        dst[8] = a00 * b20 + a10 * b21 + a20 * b22;
        dst[9] = a01 * b20 + a11 * b21 + a21 * b22;
        dst[10] = a02 * b20 + a12 * b21 + a22 * b22;

        return dst;
    }

    /// <summary>
    /// Multiplies two 3-by-3 matrices with a on the left and b on the right (same as multiply)
    /// </summary>
    public static Mat3 Mul(Mat3 a, Mat3 b, Mat3? dst = default) => Multiply(a, b, dst);

    /// <summary>
    /// Sets the translation component of a 3-by-3 matrix to the given vector.
    /// </summary>
    public static Mat3 SetTranslation(Mat3 a, Vec2 v, Mat3? dst = default)
    {
        dst = dst ?? Identity();
        if (a != dst)
        {
            dst[0] = a[0];
            dst[1] = a[1];
            dst[2] = a[2];
            dst[4] = a[4];
            dst[5] = a[5];
            dst[6] = a[6];
        }
        dst[8] = v[0];
        dst[9] = v[1];
        dst[10] = 1;
        return dst;
    }

    /// <summary>
    /// Returns the translation component of a 3-by-3 matrix as a vector with 3 entries.
    /// </summary>
    public static Vec2 GetTranslation(Mat3 m, Vec2? dst = default)
    {
        dst = dst ?? Vec2.Create();
        dst[0] = m[8];
        dst[1] = m[9];
        return dst;
    }

    /// <summary>
    /// Returns an axis of a 3x3 matrix as a vector with 2 entries
    /// </summary>
    public static Vec2 GetAxis(Mat3 m, int axis, Vec2? dst = default)
    {
        dst = dst ?? Vec2.Create();
        var off = axis * 4;
        dst[0] = m[off + 0];
        dst[1] = m[off + 1];
        return dst;
    }

    /// <summary>
    /// Sets an axis of a 3x3 matrix as a vector with 2 entries
    /// </summary>
    public static Mat3 SetAxis(Mat3 m, Vec2 v, int axis, Mat3? dst = default)
    {
        dst = dst == m ? m : Copy(m, dst);

        var off = axis * 4;
        dst[off + 0] = v[0];
        dst[off + 1] = v[1];
        return dst;
    }

    /// <summary>
    /// Returns the "2d" scaling component of the matrix
    /// </summary>
    public static Vec2 GetScaling(Mat3 m, Vec2? dst = default)
    {
        dst = dst ?? Vec2.Create();
        var xx = m[0];
        var xy = m[1];
        var yx = m[4];
        var yy = m[5];

        dst[0] = MathF.Sqrt(xx * xx + xy * xy);
        dst[1] = MathF.Sqrt(yx * yx + yy * yy);

        return dst;
    }

    /// <summary>
    /// Returns the "3d" scaling component of the matrix
    /// </summary>
    public static Vec3 Get3DScaling(Mat3 m, Vec3? dst = default)
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
    /// Creates a 3-by-3 matrix which translates by the given vector v.
    /// </summary>
    public static Mat3 Translation(Vec2 v, Mat3? dst = default)
    {
        dst = dst ?? new Mat3();
        dst[0] = 1; dst[1] = 0; dst[2] = 0;
        dst[4] = 0; dst[5] = 1; dst[6] = 0;
        dst[8] = v[0]; dst[9] = v[1]; dst[10] = 1;
        return dst;
    }

    /// <summary>
    /// Translates the given 3-by-3 matrix by the given vector v.
    /// </summary>
    public static Mat3 Translate(Mat3 m, Vec2 v, Mat3? dst = default)
    {
        dst = dst ?? new Mat3();
        var v0 = v[0];
        var v1 = v[1];

        var m00 = m[0];
        var m01 = m[1];
        var m02 = m[2];
        var m10 = m[1 * 4 + 0];
        var m11 = m[1 * 4 + 1];
        var m12 = m[1 * 4 + 2];
        var m20 = m[2 * 4 + 0];
        var m21 = m[2 * 4 + 1];
        var m22 = m[2 * 4 + 2];

        if (m != dst)
        {
            dst[0] = m00;
            dst[1] = m01;
            dst[2] = m02;
            dst[4] = m10;
            dst[5] = m11;
            dst[6] = m12;
        }

        dst[8] = m00 * v0 + m10 * v1 + m20;
        dst[9] = m01 * v0 + m11 * v1 + m21;
        dst[10] = m02 * v0 + m12 * v1 + m22;

        return dst;
    }

    /// <summary>
    /// Creates a 3-by-3 matrix which rotates by the given angle.
    /// </summary>
    public static Mat3 Rotation(float angleInRadians, Mat3? dst = default)
    {
        dst = dst ?? new Mat3();
        var c = MathF.Cos(angleInRadians);
        var s = MathF.Sin(angleInRadians);

        dst[0] = c; dst[1] = s; dst[2] = 0;
        dst[4] = -s; dst[5] = c; dst[6] = 0;
        dst[8] = 0; dst[9] = 0; dst[10] = 1;

        return dst;
    }

    /// <summary>
    /// Rotates the given 3-by-3 matrix by the given angle.
    /// </summary>
    public static Mat3 Rotate(Mat3 m, float angleInRadians, Mat3? dst = default)
    {
        dst = dst ?? new Mat3();
        var m00 = m[0 * 4 + 0];
        var m01 = m[0 * 4 + 1];
        var m02 = m[0 * 4 + 2];
        var m10 = m[1 * 4 + 0];
        var m11 = m[1 * 4 + 1];
        var m12 = m[1 * 4 + 2];
        var c = MathF.Cos(angleInRadians);
        var s = MathF.Sin(angleInRadians);

        dst[0] = c * m00 + s * m10;
        dst[1] = c * m01 + s * m11;
        dst[2] = c * m02 + s * m12;

        dst[4] = c * m10 - s * m00;
        dst[5] = c * m11 - s * m01;
        dst[6] = c * m12 - s * m02;

        if (m != dst)
        {
            dst[8] = m[8];
            dst[9] = m[9];
            dst[10] = m[10];
        }

        return dst;
    }

    /// <summary>
    /// Creates a 3-by-3 matrix which rotates around the x-axis by the given angle.
    /// </summary>
    public static Mat3 RotationX(float angleInRadians, Mat3? dst = default)
    {
        dst = dst ?? new Mat3();
        var c = MathF.Cos(angleInRadians);
        var s = MathF.Sin(angleInRadians);

        dst[0] = 1; dst[1] = 0; dst[2] = 0;
        dst[4] = 0; dst[5] = c; dst[6] = s;
        dst[8] = 0; dst[9] = -s; dst[10] = c;

        return dst;
    }

    /// <summary>
    /// Rotates the given 3-by-3 matrix around the x-axis by the given angle.
    /// </summary>
    public static Mat3 RotateX(Mat3 m, float angleInRadians, Mat3? dst = default)
    {
        dst = dst ?? new Mat3();
        var m10 = m[4];
        var m11 = m[5];
        var m12 = m[6];
        var m20 = m[8];
        var m21 = m[9];
        var m22 = m[10];

        var c = MathF.Cos(angleInRadians);
        var s = MathF.Sin(angleInRadians);

        dst[4] = c * m10 + s * m20;
        dst[5] = c * m11 + s * m21;
        dst[6] = c * m12 + s * m22;
        dst[8] = c * m20 - s * m10;
        dst[9] = c * m21 - s * m11;
        dst[10] = c * m22 - s * m12;

        if (m != dst)
        {
            dst[0] = m[0];
            dst[1] = m[1];
            dst[2] = m[2];
        }

        return dst;
    }

    /// <summary>
    /// Creates a 3-by-3 matrix which rotates around the y-axis by the given angle.
    /// </summary>
    public static Mat3 RotationY(float angleInRadians, Mat3? dst = default)
    {
        dst = dst ?? new Mat3();
        var c = MathF.Cos(angleInRadians);
        var s = MathF.Sin(angleInRadians);

        dst[0] = c; dst[1] = 0; dst[2] = -s;
        dst[4] = 0; dst[5] = 1; dst[6] = 0;
        dst[8] = s; dst[9] = 0; dst[10] = c;

        return dst;
    }

    /// <summary>
    /// Rotates the given 3-by-3 matrix around the y-axis by the given angle.
    /// </summary>
    public static Mat3 RotateY(Mat3 m, float angleInRadians, Mat3? dst = default)
    {
        dst = dst ?? new Mat3();
        var m00 = m[0 * 4 + 0];
        var m01 = m[0 * 4 + 1];
        var m02 = m[0 * 4 + 2];
        var m20 = m[2 * 4 + 0];
        var m21 = m[2 * 4 + 1];
        var m22 = m[2 * 4 + 2];
        var c = MathF.Cos(angleInRadians);
        var s = MathF.Sin(angleInRadians);

        dst[0] = c * m00 - s * m20;
        dst[1] = c * m01 - s * m21;
        dst[2] = c * m02 - s * m22;
        dst[8] = c * m20 + s * m00;
        dst[9] = c * m21 + s * m01;
        dst[10] = c * m22 + s * m02;

        if (m != dst)
        {
            dst[4] = m[4];
            dst[5] = m[5];
            dst[6] = m[6];
        }

        return dst;
    }

    /// <summary>
    /// Creates a 3-by-3 matrix which rotates around the z-axis by the given angle. (same as rotation)
    /// </summary>
    public static Mat3 RotationZ(float angleInRadians, Mat3? dst = default) => Rotation(angleInRadians, dst);

    /// <summary>
    /// Rotates the given 3-by-3 matrix around the z-axis by the given angle. (same as rotate)
    /// </summary>
    public static Mat3 RotateZ(Mat3 m, float angleInRadians, Mat3? dst = default) => Rotate(m, angleInRadians, dst);

    /// <summary>
    /// Creates a 3-by-3 matrix which scales in each dimension by an amount given by
    /// the corresponding entry in the given vector; assumes the vector has two entries.
    /// </summary>
    public static Mat3 Scaling(Vec2 v, Mat3? dst = default)
    {
        dst = dst ?? new Mat3();
        dst[0] = v[0]; dst[1] = 0; dst[2] = 0;
        dst[4] = 0; dst[5] = v[1]; dst[6] = 0;
        dst[8] = 0; dst[9] = 0; dst[10] = 1;
        return dst;
    }

    /// <summary>
    /// Scales the given 3-by-3 matrix in each dimension by an amount
    /// given by the corresponding entry in the given vector; assumes the vector has two entries.
    /// </summary>
    public static Mat3 Scale(Mat3 m, Vec2 v, Mat3? dst = default)
    {
        dst = dst ?? new Mat3();
        var v0 = v[0];
        var v1 = v[1];

        dst[0] = v0 * m[0 * 4 + 0];
        dst[1] = v0 * m[0 * 4 + 1];
        dst[2] = v0 * m[0 * 4 + 2];

        dst[4] = v1 * m[1 * 4 + 0];
        dst[5] = v1 * m[1 * 4 + 1];
        dst[6] = v1 * m[1 * 4 + 2];

        if (m != dst)
        {
            dst[8] = m[8];
            dst[9] = m[9];
            dst[10] = m[10];
        }

        return dst;
    }

    /// <summary>
    /// Creates a 3-by-3 matrix which scales in each dimension by an amount given by
    /// the corresponding entry in the given vector; assumes the vector has three entries.
    /// </summary>
    public static Mat3 Scaling3D(Vec3 v, Mat3? dst = default)
    {
        dst = dst ?? new Mat3();
        dst[0] = v[0]; dst[1] = 0; dst[2] = 0;
        dst[4] = 0; dst[5] = v[1]; dst[6] = 0;
        dst[8] = 0; dst[9] = 0; dst[10] = v[2];
        return dst;
    }

    /// <summary>
    /// Scales the given 3-by-3 matrix in each dimension by an amount
    /// given by the corresponding entry in the given vector; assumes the vector has three entries.
    /// </summary>
    public static Mat3 Scale3D(Mat3 m, Vec3 v, Mat3? dst = default)
    {
        dst = dst ?? new Mat3();
        var v0 = v[0];
        var v1 = v[1];
        var v2 = v[2];

        dst[0] = v0 * m[0 * 4 + 0];
        dst[1] = v0 * m[0 * 4 + 1];
        dst[2] = v0 * m[0 * 4 + 2];

        dst[4] = v1 * m[1 * 4 + 0];
        dst[5] = v1 * m[1 * 4 + 1];
        dst[6] = v1 * m[1 * 4 + 2];

        dst[8] = v2 * m[2 * 4 + 0];
        dst[9] = v2 * m[2 * 4 + 1];
        dst[10] = v2 * m[2 * 4 + 2];

        return dst;
    }

    /// <summary>
    /// Creates a 3-by-3 matrix which scales uniformly in the X and Y dimensions
    /// </summary>
    public static Mat3 UniformScaling(float s, Mat3? dst = default)
    {
        dst = dst ?? new Mat3();
        dst[0] = s; dst[1] = 0; dst[2] = 0;
        dst[4] = 0; dst[5] = s; dst[6] = 0;
        dst[8] = 0; dst[9] = 0; dst[10] = 1;
        return dst;
    }

    /// <summary>
    /// Scales the given 3-by-3 matrix in the X and Y dimension by an amount given.
    /// </summary>
    public static Mat3 UniformScale(Mat3 m, float s, Mat3? dst = default)
    {
        dst = dst ?? new Mat3();
        dst[0] = s * m[0 * 4 + 0];
        dst[1] = s * m[0 * 4 + 1];
        dst[2] = s * m[0 * 4 + 2];

        dst[4] = s * m[1 * 4 + 0];
        dst[5] = s * m[1 * 4 + 1];
        dst[6] = s * m[1 * 4 + 2];

        if (m != dst)
        {
            dst[8] = m[8];
            dst[9] = m[9];
            dst[10] = m[10];
        }

        return dst;
    }

    /// <summary>
    /// Creates a 3-by-3 matrix which scales uniformly in each dimension
    /// </summary>
    public static Mat3 UniformScaling3D(float s, Mat3? dst = default)
    {
        dst = dst ?? new Mat3();
        dst[0] = s; dst[1] = 0; dst[2] = 0;
        dst[4] = 0; dst[5] = s; dst[6] = 0;
        dst[8] = 0; dst[9] = 0; dst[10] = s;
        return dst;
    }

    /// <summary>
    /// Scales the given 3-by-3 matrix in each dimension by an amount given.
    /// </summary>
    public static Mat3 UniformScale3D(Mat3 m, float s, Mat3? dst = default)
    {
        dst = dst ?? new Mat3();
        dst[0] = s * m[0 * 4 + 0];
        dst[1] = s * m[0 * 4 + 1];
        dst[2] = s * m[0 * 4 + 2];

        dst[4] = s * m[1 * 4 + 0];
        dst[5] = s * m[1 * 4 + 1];
        dst[6] = s * m[1 * 4 + 2];

        dst[8] = s * m[2 * 4 + 0];
        dst[9] = s * m[2 * 4 + 1];
        dst[10] = s * m[2 * 4 + 2];

        return dst;
    }

    public override int GetHashCode()
    {
        return (int)(this[0] * this[1] * this[2] *
                     this[4] * this[5] * this[6] *
                     this[8] * this[9] * this[10]);
    }
}
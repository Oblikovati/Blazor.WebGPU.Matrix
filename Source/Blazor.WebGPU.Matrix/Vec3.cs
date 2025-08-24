using Microsoft.JSInterop;
using SpawnDev.BlazorJS.JSObjects;

namespace Blazor.WebGPU.Matrix;

public class Vec3 : Float32Array
{
    public Vec3(IJSInProcessObjectReference _ref) : base(_ref) { }

    private Vec3() : base(new Float32Array(3).JSRef!) { }

    /// <summary>
    /// Creates a vec3; may be called with x, y, z to set initial values.
    /// </summary>
    public static Vec3 Create(float x = 0, float y = 0, float z = 0)
    {
        var dst = new Vec3();
        dst[0] = x;
        dst[1] = y;
        dst[2] = z;
        return dst;
    }

    /// <summary>
    /// Creates a vec3; may be called with x, y, z to set initial values. (same as create)
    /// </summary>
    public static Vec3 FromValues(float x = 0, float y = 0, float z = 0) => Create(x, y, z);

    /// <summary>
    /// Sets the values of a Vec3
    /// </summary>
    public static Vec3 Set(float x, float y, float z, Vec3? dst = default)
    {
        dst = dst ?? new Vec3();
        dst[0] = x;
        dst[1] = y;
        dst[2] = z;
        return dst;
    }

    /// <summary>
    /// Applies MathF.Ceiling to each element of vector
    /// </summary>
    public static Vec3 Ceil(Vec3 v, Vec3? dst = default)
    {
        dst = dst ?? new Vec3();
        dst[0] = MathF.Ceiling(v[0]);
        dst[1] = MathF.Ceiling(v[1]);
        dst[2] = MathF.Ceiling(v[2]);
        return dst;
    }

    /// <summary>
    /// Applies MathF.Floor to each element of vector
    /// </summary>
    public static Vec3 Floor(Vec3 v, Vec3? dst = default)
    {
        dst = dst ?? new Vec3();
        dst[0] = MathF.Floor(v[0]);
        dst[1] = MathF.Floor(v[1]);
        dst[2] = MathF.Floor(v[2]);
        return dst;
    }

    /// <summary>
    /// Applies MathF.Round to each element of vector
    /// </summary>
    public static Vec3 Round(Vec3 v, Vec3? dst = default)
    {
        dst = dst ?? new Vec3();
        dst[0] = MathF.Round(v[0]);
        dst[1] = MathF.Round(v[1]);
        dst[2] = MathF.Round(v[2]);
        return dst;
    }

    /// <summary>
    /// Clamp each element of vector between min and max
    /// </summary>
    public static Vec3 Clamp(Vec3 v, float min = 0, float max = 1, Vec3? dst = default)
    {
        dst = dst ?? new Vec3();
        dst[0] = MathF.Min(max, MathF.Max(min, v[0]));
        dst[1] = MathF.Min(max, MathF.Max(min, v[1]));
        dst[2] = MathF.Min(max, MathF.Max(min, v[2]));
        return dst;
    }

    /// <summary>
    /// Adds two vectors; assumes a and b have the same dimension.
    /// </summary>
    public static Vec3 Add(Vec3 a, Vec3 b, Vec3? dst = default)
    {
        dst = dst ?? new Vec3();
        dst[0] = a[0] + b[0];
        dst[1] = a[1] + b[1];
        dst[2] = a[2] + b[2];
        return dst;
    }

    /// <summary>
    /// Adds two vectors, scaling the 2nd; assumes a and b have the same dimension.
    /// </summary>
    public static Vec3 AddScaled(Vec3 a, Vec3 b, float scale, Vec3? dst = default)
    {
        dst = dst ?? new Vec3();
        dst[0] = a[0] + b[0] * scale;
        dst[1] = a[1] + b[1] * scale;
        dst[2] = a[2] + b[2] * scale;
        return dst;
    }

    /// <summary>
    /// Returns the angle in radians between two vectors.
    /// </summary>
    public static float Angle(Vec3 a, Vec3 b)
    {
        var ax = a[0];
        var ay = a[1];
        var az = a[2];
        var bx = b[0];
        var by = b[1];
        var bz = b[2];
        var mag1 = MathF.Sqrt(ax * ax + ay * ay + az * az);
        var mag2 = MathF.Sqrt(bx * bx + by * by + bz * bz);
        var mag = mag1 * mag2;
        var cosine = mag != 0 ? Dot(a, b) / mag : 0;
        return MathF.Acos(cosine);
    }

    /// <summary>
    /// Subtracts two vectors.
    /// </summary>
    public static Vec3 Subtract(Vec3 a, Vec3 b, Vec3? dst = default)
    {
        dst = dst ?? new Vec3();
        dst[0] = a[0] - b[0];
        dst[1] = a[1] - b[1];
        dst[2] = a[2] - b[2];
        return dst;
    }

    /// <summary>
    /// Subtracts two vectors. (same as subtract)
    /// </summary>
    public static Vec3 Sub(Vec3 a, Vec3 b, Vec3? dst = default) => Subtract(a, b, dst);

    /// <summary>
    /// Check if 2 vectors are approximately equal
    /// </summary>
    public static bool EqualsApproximately(Vec3 a, Vec3 b)
    {
        return MathF.Abs(a[0] - b[0]) < Utils.EPSILON &&
               MathF.Abs(a[1] - b[1]) < Utils.EPSILON &&
               MathF.Abs(a[2] - b[2]) < Utils.EPSILON;
    }

    /// <summary>
    /// Check if 2 vectors are exactly equal
    /// </summary>
    public static bool Equals(Vec3 a, Vec3 b)
    {
        return a[0] == b[0] && a[1] == b[1] && a[2] == b[2];
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj))
            return true;

        if (obj is null) return false;

        if (obj is Vec3 v3)
            return Equals(this, v3);

        return false;
    }

    /// <summary>
    /// Performs linear interpolation on two vectors.
    /// </summary>
    public static Vec3 Lerp(Vec3 a, Vec3 b, float t, Vec3? dst = default)
    {
        dst = dst ?? new Vec3();
        dst[0] = a[0] + t * (b[0] - a[0]);
        dst[1] = a[1] + t * (b[1] - a[1]);
        dst[2] = a[2] + t * (b[2] - a[2]);
        return dst;
    }

    /// <summary>
    /// Performs linear interpolation on two vectors with vector t.
    /// </summary>
    public static Vec3 LerpV(Vec3 a, Vec3 b, Vec3 t, Vec3? dst = default)
    {
        dst = dst ?? new Vec3();
        dst[0] = a[0] + t[0] * (b[0] - a[0]);
        dst[1] = a[1] + t[1] * (b[1] - a[1]);
        dst[2] = a[2] + t[2] * (b[2] - a[2]);
        return dst;
    }

    /// <summary>
    /// Return max values of two vectors.
    /// </summary>
    public static Vec3 Max(Vec3 a, Vec3 b, Vec3? dst = default)
    {
        dst = dst ?? new Vec3();
        dst[0] = MathF.Max(a[0], b[0]);
        dst[1] = MathF.Max(a[1], b[1]);
        dst[2] = MathF.Max(a[2], b[2]);
        return dst;
    }

    /// <summary>
    /// Return min values of two vectors.
    /// </summary>
    public static Vec3 Min(Vec3 a, Vec3 b, Vec3? dst = default)
    {
        dst = dst ?? new Vec3();
        dst[0] = MathF.Min(a[0], b[0]);
        dst[1] = MathF.Min(a[1], b[1]);
        dst[2] = MathF.Min(a[2], b[2]);
        return dst;
    }

    /// <summary>
    /// Multiplies a vector by a scalar.
    /// </summary>
    public static Vec3 MulScalar(Vec3 v, float k, Vec3? dst = default)
    {
        dst = dst ?? new Vec3();
        dst[0] = v[0] * k;
        dst[1] = v[1] * k;
        dst[2] = v[2] * k;
        return dst;
    }

    /// <summary>
    /// Multiplies a vector by a scalar. (same as mulScalar)
    /// </summary>
    public static Vec3 Scale(Vec3 v, float k, Vec3? dst = default) => MulScalar(v, k, dst);

    /// <summary>
    /// Divides a vector by a scalar.
    /// </summary>
    public static Vec3 DivScalar(Vec3 v, float k, Vec3? dst = default)
    {
        dst = dst ?? new Vec3();
        dst[0] = v[0] / k;
        dst[1] = v[1] / k;
        dst[2] = v[2] / k;
        return dst;
    }

    /// <summary>
    /// Inverse a vector.
    /// </summary>
    public static Vec3 Inverse(Vec3 v, Vec3? dst = default)
    {
        dst = dst ?? new Vec3();
        dst[0] = 1.0f / v[0];
        dst[1] = 1.0f / v[1];
        dst[2] = 1.0f / v[2];
        return dst;
    }

    /// <summary>
    /// Invert a vector. (same as inverse)
    /// </summary>
    public static Vec3 Invert(Vec3 v, Vec3? dst = default) => Inverse(v, dst);

    /// <summary>
    /// Computes the cross product of two vectors.
    /// </summary>
    public static Vec3 Cross(Vec3 a, Vec3 b, Vec3? dst = default)
    {
        dst = dst ?? new Vec3();
        var t1 = a[2] * b[0] - a[0] * b[2];
        var t2 = a[0] * b[1] - a[1] * b[0];
        dst[0] = a[1] * b[2] - a[2] * b[1];
        dst[1] = t1;
        dst[2] = t2;
        return dst;
    }

    /// <summary>
    /// Computes the dot product of two vectors.
    /// </summary>
    public static float Dot(Vec3 a, Vec3 b)
    {
        return a[0] * b[0] + a[1] * b[1] + a[2] * b[2];
    }

    /// <summary>
    /// Computes the length of vector
    /// </summary>
    public static float Length(Vec3 v)
    {
        var v0 = v[0];
        var v1 = v[1];
        var v2 = v[2];
        return MathF.Sqrt(v0 * v0 + v1 * v1 + v2 * v2);
    }

    /// <summary>
    /// Computes the length of vector (same as length)
    /// </summary>
    public static float Len(Vec3 v) => Length(v);

    /// <summary>
    /// Computes the square of the length of vector
    /// </summary>
    public static float LengthSq(Vec3 v)
    {
        var v0 = v[0];
        var v1 = v[1];
        var v2 = v[2];
        return v0 * v0 + v1 * v1 + v2 * v2;
    }

    /// <summary>
    /// Computes the square of the length of vector (same as lengthSq)
    /// </summary>
    public static float LenSq(Vec3 v) => LengthSq(v);

    /// <summary>
    /// Computes the distance between 2 points
    /// </summary>
    public static float Distance(Vec3 a, Vec3 b)
    {
        var dx = a[0] - b[0];
        var dy = a[1] - b[1];
        var dz = a[2] - b[2];
        return MathF.Sqrt(dx * dx + dy * dy + dz * dz);
    }

    /// <summary>
    /// Computes the distance between 2 points (same as distance)
    /// </summary>
    public static float Dist(Vec3 a, Vec3 b) => Distance(a, b);

    /// <summary>
    /// Computes the square of the distance between 2 points
    /// </summary>
    public static float DistanceSq(Vec3 a, Vec3 b)
    {
        var dx = a[0] - b[0];
        var dy = a[1] - b[1];
        var dz = a[2] - b[2];
        return dx * dx + dy * dy + dz * dz;
    }

    /// <summary>
    /// Computes the square of the distance between 2 points (same as distanceSq)
    /// </summary>
    public static float DistSq(Vec3 a, Vec3 b) => DistanceSq(a, b);

    /// <summary>
    /// Divides a vector by its Euclidean length and returns the quotient.
    /// </summary>
    public static Vec3 Normalize(Vec3 v, Vec3? dst = default)
    {
        dst = dst ?? new Vec3();
        var v0 = v[0];
        var v1 = v[1];
        var v2 = v[2];
        var len = MathF.Sqrt(v0 * v0 + v1 * v1 + v2 * v2);

        if (len > 0.00001f)
        {
            dst[0] = v0 / len;
            dst[1] = v1 / len;
            dst[2] = v2 / len;
        }
        else
        {
            dst[0] = 0;
            dst[1] = 0;
            dst[2] = 0;
        }

        return dst;
    }

    /// <summary>
    /// Negates a vector.
    /// </summary>
    public static Vec3 Negate(Vec3 v, Vec3? dst = default)
    {
        dst = dst ?? new Vec3();
        dst[0] = -v[0];
        dst[1] = -v[1];
        dst[2] = -v[2];
        return dst;
    }

    /// <summary>
    /// Copies a vector. (same as clone)
    /// </summary>
    public static Vec3 Copy(Vec3 v, Vec3? dst = default)
    {
        dst = dst ?? new Vec3();
        dst[0] = v[0];
        dst[1] = v[1];
        dst[2] = v[2];
        return dst;
    }

    /// <summary>
    /// Clones a vector. (same as copy)
    /// </summary>
    public static Vec3 Clone(Vec3 v, Vec3? dst = default) => Copy(v, dst);

    /// <summary>
    /// Multiplies a vector by another vector (component-wise).
    /// </summary>
    public static Vec3 Multiply(Vec3 a, Vec3 b, Vec3? dst = default)
    {
        dst = dst ?? new Vec3();
        dst[0] = a[0] * b[0];
        dst[1] = a[1] * b[1];
        dst[2] = a[2] * b[2];
        return dst;
    }

    /// <summary>
    /// Multiplies a vector by another vector (component-wise). (same as multiply)
    /// </summary>
    public static Vec3 Mul(Vec3 a, Vec3 b, Vec3? dst = default) => Multiply(a, b, dst);

    /// <summary>
    /// Divides a vector by another vector (component-wise).
    /// </summary>
    public static Vec3 Divide(Vec3 a, Vec3 b, Vec3? dst = default)
    {
        dst = dst ?? new Vec3();
        dst[0] = a[0] / b[0];
        dst[1] = a[1] / b[1];
        dst[2] = a[2] / b[2];
        return dst;
    }

    /// <summary>
    /// Divides a vector by another vector (component-wise). (same as divide)
    /// </summary>
    public static Vec3 Div(Vec3 a, Vec3 b, Vec3? dst = default) => Divide(a, b, dst);

    /// <summary>
    /// Creates a random vector
    /// </summary>
    public static Vec3 Random(float scale = 1, Vec3? dst = default)
    {
        dst = dst ?? new Vec3();
        var angle = (float)new Random().NextDouble() * 2 * MathF.PI;
        var z = (float)new Random().NextDouble() * 2 - 1;
        var zScale = MathF.Sqrt(1 - z * z) * scale;
        dst[0] = MathF.Cos(angle) * zScale;
        dst[1] = MathF.Sin(angle) * zScale;
        dst[2] = z * scale;
        return dst;
    }

    /// <summary>
    /// Zero's a vector
    /// </summary>
    public static Vec3 Zero(Vec3? dst = default)
    {
        dst = dst ?? new Vec3();
        dst[0] = 0;
        dst[1] = 0;
        dst[2] = 0;
        return dst;
    }

    /// <summary>
    /// Transform Vec3 by 4x4 matrix
    /// </summary>
    public static Vec3 TransformMat4(Vec3 v, Mat4 m, Vec3? dst = default)
    {
        dst = dst ?? new Vec3();
        var x = v[0];
        var y = v[1];
        var z = v[2];
        var w = m[3] * x + m[7] * y + m[11] * z + m[15] != 0 ? m[3] * x + m[7] * y + m[11] * z + m[15] : 1;

        dst[0] = (m[0] * x + m[4] * y + m[8] * z + m[12]) / w;
        dst[1] = (m[1] * x + m[5] * y + m[9] * z + m[13]) / w;
        dst[2] = (m[2] * x + m[6] * y + m[10] * z + m[14]) / w;

        return dst;
    }

    /// <summary>
    /// Transform vec3 by upper 3x3 matrix inside 4x4 matrix.
    /// </summary>
    public static Vec3 TransformMat4Upper3x3(Vec3 v, Mat4 m, Vec3? dst = default)
    {
        dst = dst ?? new Vec3();
        var v0 = v[0];
        var v1 = v[1];
        var v2 = v[2];

        dst[0] = v0 * m[0 * 4 + 0] + v1 * m[1 * 4 + 0] + v2 * m[2 * 4 + 0];
        dst[1] = v0 * m[0 * 4 + 1] + v1 * m[1 * 4 + 1] + v2 * m[2 * 4 + 1];
        dst[2] = v0 * m[0 * 4 + 2] + v1 * m[1 * 4 + 2] + v2 * m[2 * 4 + 2];

        return dst;
    }

    /// <summary>
    /// Transforms vec3 by 3x3 matrix
    /// </summary>
    public static Vec3 TransformMat3(Vec3 v, Mat3 m, Vec3? dst = default)
    {
        dst = dst ?? new Vec3();
        var x = v[0];
        var y = v[1];
        var z = v[2];

        dst[0] = x * m[0] + y * m[4] + z * m[8];
        dst[1] = x * m[1] + y * m[5] + z * m[9];
        dst[2] = x * m[2] + y * m[6] + z * m[10];

        return dst;
    }

    /// <summary>
    /// Transforms vec3 by Quaternion
    /// </summary>
    public static Vec3 TransformQuat(Vec3 v, Quat q, Vec3? dst = default)
    {
        dst = dst ?? new Vec3();
        var qx = q[0];
        var qy = q[1];
        var qz = q[2];
        var w2 = q[3] * 2;

        var x = v[0];
        var y = v[1];
        var z = v[2];

        var uvX = qy * z - qz * y;
        var uvY = qz * x - qx * z;
        var uvZ = qx * y - qy * x;

        dst[0] = x + uvX * w2 + (qy * uvZ - qz * uvY) * 2;
        dst[1] = y + uvY * w2 + (qz * uvX - qx * uvZ) * 2;
        dst[2] = z + uvZ * w2 + (qx * uvY - qy * uvX) * 2;

        return dst;
    }

    /// <summary>
    /// Returns the translation component of a 4-by-4 matrix as a vector with 3 entries.
    /// </summary>
    public static Vec3 GetTranslation(Mat4 m, Vec3? dst = default)
    {
        dst = dst ?? new Vec3();
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
        dst = dst ?? new Vec3();
        var off = axis * 4;
        dst[0] = m[off + 0];
        dst[1] = m[off + 1];
        dst[2] = m[off + 2];
        return dst;
    }

    /// <summary>
    /// Returns the scaling component of the matrix
    /// </summary>
    public static Vec3 GetScaling(Mat4 m, Vec3? dst = default)
    {
        dst = dst ?? new Vec3();
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
    /// Rotate a 3D vector around the x-axis
    /// </summary>
    public static Vec3 RotateX(Vec3 a, Vec3 b, float rad, Vec3? dst = default)
    {
        dst = dst ?? new Vec3();
        var p = new float[3];
        var r = new float[3];

        // Translate point to the origin
        p[0] = a[0] - b[0];
        p[1] = a[1] - b[1];
        p[2] = a[2] - b[2];

        // Perform rotation
        r[0] = p[0];
        r[1] = p[1] * MathF.Cos(rad) - p[2] * MathF.Sin(rad);
        r[2] = p[1] * MathF.Sin(rad) + p[2] * MathF.Cos(rad);

        // Translate to correct position
        dst[0] = r[0] + b[0];
        dst[1] = r[1] + b[1];
        dst[2] = r[2] + b[2];

        return dst;
    }

    /// <summary>
    /// Rotate a 3D vector around the y-axis
    /// </summary>
    public static Vec3 RotateY(Vec3 a, Vec3 b, float rad, Vec3? dst = default)
    {
        dst = dst ?? new Vec3();
        var p = new float[3];
        var r = new float[3];

        // Translate point to the origin
        p[0] = a[0] - b[0];
        p[1] = a[1] - b[1];
        p[2] = a[2] - b[2];

        // Perform rotation
        r[0] = p[2] * MathF.Sin(rad) + p[0] * MathF.Cos(rad);
        r[1] = p[1];
        r[2] = p[2] * MathF.Cos(rad) - p[0] * MathF.Sin(rad);

        // Translate to correct position
        dst[0] = r[0] + b[0];
        dst[1] = r[1] + b[1];
        dst[2] = r[2] + b[2];

        return dst;
    }

    /// <summary>
    /// Rotate a 3D vector around the z-axis
    /// </summary>
    public static Vec3 RotateZ(Vec3 a, Vec3 b, float rad, Vec3? dst = default)
    {
        dst = dst ?? new Vec3();
        var p = new float[3];
        var r = new float[3];

        // Translate point to the origin
        p[0] = a[0] - b[0];
        p[1] = a[1] - b[1];
        p[2] = a[2] - b[2];

        // Perform rotation
        r[0] = p[0] * MathF.Cos(rad) - p[1] * MathF.Sin(rad);
        r[1] = p[0] * MathF.Sin(rad) + p[1] * MathF.Cos(rad);
        r[2] = p[2];

        // Translate to correct position
        dst[0] = r[0] + b[0];
        dst[1] = r[1] + b[1];
        dst[2] = r[2] + b[2];

        return dst;
    }

    /// <summary>
    /// Treat a 3D vector as a direction and set it's length
    /// </summary>
    public static Vec3 SetLength(Vec3 a, float len, Vec3? dst = default)
    {
        dst = dst ?? new Vec3();
        Normalize(a, dst);
        return MulScalar(dst, len, dst);
    }

    /// <summary>
    /// Ensure a vector is not longer than a max length
    /// </summary>
    public static Vec3 Truncate(Vec3 a, float maxLen, Vec3? dst = default)
    {
        dst = dst ?? new Vec3();
        if (Length(a) > maxLen)
        {
            return SetLength(a, maxLen, dst);
        }
        return Copy(a, dst);
    }

    /// <summary>
    /// Return the vector exactly between 2 endpoint vectors
    /// </summary>
    public static Vec3 Midpoint(Vec3 a, Vec3 b, Vec3? dst = default)
    {
        dst = dst ?? new Vec3();
        return Lerp(a, b, 0.5f, dst);
    }

    public override int GetHashCode()
    {
        return (int)(this[0] * this[1] * this[2]);
    }
}
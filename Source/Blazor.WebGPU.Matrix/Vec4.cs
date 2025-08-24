using Microsoft.JSInterop;
using SpawnDev.BlazorJS.JSObjects;

namespace Blazor.WebGPU.Matrix;

public class Vec4 : Float32Array
{
    public Vec4(IJSInProcessObjectReference _ref) : base(_ref) { }

    private Vec4() : base(new Float32Array(4).JSRef!) { }

    /// <summary>
    /// Creates a vec4; may be called with x, y, z, w to set initial values.
    /// </summary>
    public static Vec4 Create(float x = 0, float y = 0, float z = 0, float w = 0)
    {
        var dst = new Vec4();
        dst[0] = x;
        dst[1] = y;
        dst[2] = z;
        dst[3] = w;
        return dst;
    }

    /// <summary>
    /// Creates a vec4; may be called with x, y, z, w to set initial values. (same as create)
    /// </summary>
    public static Vec4 FromValues(float x = 0, float y = 0, float z = 0, float w = 0) => Create(x, y, z, w);

    /// <summary>
    /// Sets the values of a Vec4
    /// </summary>
    public static Vec4 Set(float x, float y, float z, float w, Vec4? dst = default)
    {
        dst = dst ?? new Vec4();
        dst[0] = x;
        dst[1] = y;
        dst[2] = z;
        dst[3] = w;
        return dst;
    }

    /// <summary>
    /// Applies MathF.Ceiling to each element of vector
    /// </summary>
    public static Vec4 Ceil(Vec4 v, Vec4? dst = default)
    {
        dst = dst ?? new Vec4();
        dst[0] = MathF.Ceiling(v[0]);
        dst[1] = MathF.Ceiling(v[1]);
        dst[2] = MathF.Ceiling(v[2]);
        dst[3] = MathF.Ceiling(v[3]);
        return dst;
    }

    /// <summary>
    /// Applies MathF.Floor to each element of vector
    /// </summary>
    public static Vec4 Floor(Vec4 v, Vec4? dst = default)
    {
        dst = dst ?? new Vec4();
        dst[0] = MathF.Floor(v[0]);
        dst[1] = MathF.Floor(v[1]);
        dst[2] = MathF.Floor(v[2]);
        dst[3] = MathF.Floor(v[3]);
        return dst;
    }

    /// <summary>
    /// Applies MathF.Round to each element of vector
    /// </summary>
    public static Vec4 Round(Vec4 v, Vec4? dst = default)
    {
        dst = dst ?? new Vec4();
        dst[0] = MathF.Round(v[0]);
        dst[1] = MathF.Round(v[1]);
        dst[2] = MathF.Round(v[2]);
        dst[3] = MathF.Round(v[3]);
        return dst;
    }

    /// <summary>
    /// Clamp each element of vector between min and max
    /// </summary>
    public static Vec4 Clamp(Vec4 v, float min = 0, float max = 1, Vec4? dst = default)
    {
        dst = dst ?? new Vec4();
        dst[0] = MathF.Min(max, MathF.Max(min, v[0]));
        dst[1] = MathF.Min(max, MathF.Max(min, v[1]));
        dst[2] = MathF.Min(max, MathF.Max(min, v[2]));
        dst[3] = MathF.Min(max, MathF.Max(min, v[3]));
        return dst;
    }

    /// <summary>
    /// Adds two vectors; assumes a and b have the same dimension.
    /// </summary>
    public static Vec4 Add(Vec4 a, Vec4 b, Vec4? dst = default)
    {
        dst = dst ?? new Vec4();
        dst[0] = a[0] + b[0];
        dst[1] = a[1] + b[1];
        dst[2] = a[2] + b[2];
        dst[3] = a[3] + b[3];
        return dst;
    }

    /// <summary>
    /// Adds two vectors, scaling the 2nd; assumes a and b have the same dimension.
    /// </summary>
    public static Vec4 AddScaled(Vec4 a, Vec4 b, float scale, Vec4? dst = default)
    {
        dst = dst ?? new Vec4();
        dst[0] = a[0] + b[0] * scale;
        dst[1] = a[1] + b[1] * scale;
        dst[2] = a[2] + b[2] * scale;
        dst[3] = a[3] + b[3] * scale;
        return dst;
    }

    /// <summary>
    /// Subtracts two vectors.
    /// </summary>
    public static Vec4 Subtract(Vec4 a, Vec4 b, Vec4? dst = default)
    {
        dst = dst ?? new Vec4();
        dst[0] = a[0] - b[0];
        dst[1] = a[1] - b[1];
        dst[2] = a[2] - b[2];
        dst[3] = a[3] - b[3];
        return dst;
    }

    /// <summary>
    /// Subtracts two vectors. (same as subtract)
    /// </summary>
    public static Vec4 Sub(Vec4 a, Vec4 b, Vec4? dst = default) => Subtract(a, b, dst);

    /// <summary>
    /// Check if 2 vectors are approximately equal
    /// </summary>
    public static bool EqualsApproximately(Vec4 a, Vec4 b)
    {
        return MathF.Abs(a[0] - b[0]) < Utils.EPSILON &&
               MathF.Abs(a[1] - b[1]) < Utils.EPSILON &&
               MathF.Abs(a[2] - b[2]) < Utils.EPSILON &&
               MathF.Abs(a[3] - b[3]) < Utils.EPSILON;
    }

    /// <summary>
    /// Check if 2 vectors are exactly equal
    /// </summary>
    public static bool Equals(Vec4 a, Vec4 b)
    {
        return a[0] == b[0] && a[1] == b[1] && a[2] == b[2] && a[3] == b[3];
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj))
            return true;

        if (obj is null) return false;

        if (obj is Vec4 v4)
            return Equals(this, v4);

        return false;
    }

    /// <summary>
    /// Performs linear interpolation on two vectors.
    /// </summary>
    public static Vec4 Lerp(Vec4 a, Vec4 b, float t, Vec4? dst = default)
    {
        dst = dst ?? new Vec4();
        dst[0] = a[0] + t * (b[0] - a[0]);
        dst[1] = a[1] + t * (b[1] - a[1]);
        dst[2] = a[2] + t * (b[2] - a[2]);
        dst[3] = a[3] + t * (b[3] - a[3]);
        return dst;
    }

    /// <summary>
    /// Performs linear interpolation on two vectors with vector t.
    /// </summary>
    public static Vec4 LerpV(Vec4 a, Vec4 b, Vec4 t, Vec4? dst = default)
    {
        dst = dst ?? new Vec4();
        dst[0] = a[0] + t[0] * (b[0] - a[0]);
        dst[1] = a[1] + t[1] * (b[1] - a[1]);
        dst[2] = a[2] + t[2] * (b[2] - a[2]);
        dst[3] = a[3] + t[3] * (b[3] - a[3]);
        return dst;
    }

    /// <summary>
    /// Return max values of two vectors.
    /// </summary>
    public static Vec4 Max(Vec4 a, Vec4 b, Vec4? dst = default)
    {
        dst = dst ?? new Vec4();
        dst[0] = MathF.Max(a[0], b[0]);
        dst[1] = MathF.Max(a[1], b[1]);
        dst[2] = MathF.Max(a[2], b[2]);
        dst[3] = MathF.Max(a[3], b[3]);
        return dst;
    }

    /// <summary>
    /// Return min values of two vectors.
    /// </summary>
    public static Vec4 Min(Vec4 a, Vec4 b, Vec4? dst = default)
    {
        dst = dst ?? new Vec4();
        dst[0] = MathF.Min(a[0], b[0]);
        dst[1] = MathF.Min(a[1], b[1]);
        dst[2] = MathF.Min(a[2], b[2]);
        dst[3] = MathF.Min(a[3], b[3]);
        return dst;
    }

    /// <summary>
    /// Multiplies a vector by a scalar.
    /// </summary>
    public static Vec4 MulScalar(Vec4 v, float k, Vec4? dst = default)
    {
        dst = dst ?? new Vec4();
        dst[0] = v[0] * k;
        dst[1] = v[1] * k;
        dst[2] = v[2] * k;
        dst[3] = v[3] * k;
        return dst;
    }

    /// <summary>
    /// Multiplies a vector by a scalar. (same as mulScalar)
    /// </summary>
    public static Vec4 Scale(Vec4 v, float k, Vec4? dst = default) => MulScalar(v, k, dst);

    /// <summary>
    /// Divides a vector by a scalar.
    /// </summary>
    public static Vec4 DivScalar(Vec4 v, float k, Vec4? dst = default)
    {
        dst = dst ?? new Vec4();
        dst[0] = v[0] / k;
        dst[1] = v[1] / k;
        dst[2] = v[2] / k;
        dst[3] = v[3] / k;
        return dst;
    }

    /// <summary>
    /// Inverse a vector.
    /// </summary>
    public static Vec4 Inverse(Vec4 v, Vec4? dst = default)
    {
        dst = dst ?? new Vec4();
        dst[0] = 1.0f / v[0];
        dst[1] = 1.0f / v[1];
        dst[2] = 1.0f / v[2];
        dst[3] = 1.0f / v[3];
        return dst;
    }

    /// <summary>
    /// Invert a vector. (same as inverse)
    /// </summary>
    public static Vec4 Invert(Vec4 v, Vec4? dst = default) => Inverse(v, dst);

    /// <summary>
    /// Computes the dot product of two vectors
    /// </summary>
    public static float Dot(Vec4 a, Vec4 b)
    {
        return a[0] * b[0] + a[1] * b[1] + a[2] * b[2] + a[3] * b[3];
    }

    /// <summary>
    /// Computes the length of vector
    /// </summary>
    public static float Length(Vec4 v)
    {
        var v0 = v[0];
        var v1 = v[1];
        var v2 = v[2];
        var v3 = v[3];
        return MathF.Sqrt(v0 * v0 + v1 * v1 + v2 * v2 + v3 * v3);
    }

    /// <summary>
    /// Computes the length of vector (same as length)
    /// </summary>
    public static float Len(Vec4 v) => Length(v);

    /// <summary>
    /// Computes the square of the length of vector
    /// </summary>
    public static float LengthSq(Vec4 v)
    {
        var v0 = v[0];
        var v1 = v[1];
        var v2 = v[2];
        var v3 = v[3];
        return v0 * v0 + v1 * v1 + v2 * v2 + v3 * v3;
    }

    /// <summary>
    /// Computes the square of the length of vector (same as lengthSq)
    /// </summary>
    public static float LenSq(Vec4 v) => LengthSq(v);

    /// <summary>
    /// Computes the distance between 2 points
    /// </summary>
    public static float Distance(Vec4 a, Vec4 b)
    {
        var dx = a[0] - b[0];
        var dy = a[1] - b[1];
        var dz = a[2] - b[2];
        var dw = a[3] - b[3];
        return MathF.Sqrt(dx * dx + dy * dy + dz * dz + dw * dw);
    }

    /// <summary>
    /// Computes the distance between 2 points (same as distance)
    /// </summary>
    public static float Dist(Vec4 a, Vec4 b) => Distance(a, b);

    /// <summary>
    /// Computes the square of the distance between 2 points
    /// </summary>
    public static float DistanceSq(Vec4 a, Vec4 b)
    {
        var dx = a[0] - b[0];
        var dy = a[1] - b[1];
        var dz = a[2] - b[2];
        var dw = a[3] - b[3];
        return dx * dx + dy * dy + dz * dz + dw * dw;
    }

    /// <summary>
    /// Computes the square of the distance between 2 points (same as distanceSq)
    /// </summary>
    public static float DistSq(Vec4 a, Vec4 b) => DistanceSq(a, b);

    /// <summary>
    /// Divides a vector by its Euclidean length and returns the quotient.
    /// </summary>
    public static Vec4 Normalize(Vec4 v, Vec4? dst = default)
    {
        dst = dst ?? new Vec4();
        var v0 = v[0];
        var v1 = v[1];
        var v2 = v[2];
        var v3 = v[3];
        var len = MathF.Sqrt(v0 * v0 + v1 * v1 + v2 * v2 + v3 * v3);

        if (len > 0.00001f)
        {
            dst[0] = v0 / len;
            dst[1] = v1 / len;
            dst[2] = v2 / len;
            dst[3] = v3 / len;
        }
        else
        {
            dst[0] = 0;
            dst[1] = 0;
            dst[2] = 0;
            dst[3] = 0;
        }

        return dst;
    }

    /// <summary>
    /// Negates a vector.
    /// </summary>
    public static Vec4 Negate(Vec4 v, Vec4? dst = default)
    {
        dst = dst ?? new Vec4();
        dst[0] = -v[0];
        dst[1] = -v[1];
        dst[2] = -v[2];
        dst[3] = -v[3];
        return dst;
    }

    /// <summary>
    /// Copies a vector. (same as clone)
    /// </summary>
    public static Vec4 Copy(Vec4 v, Vec4? dst = default)
    {
        dst = dst ?? new Vec4();
        dst[0] = v[0];
        dst[1] = v[1];
        dst[2] = v[2];
        dst[3] = v[3];
        return dst;
    }

    /// <summary>
    /// Clones a vector. (same as copy)
    /// </summary>
    public static Vec4 Clone(Vec4 v, Vec4? dst = default) => Copy(v, dst);

    /// <summary>
    /// Multiplies a vector by another vector (component-wise).
    /// </summary>
    public static Vec4 Multiply(Vec4 a, Vec4 b, Vec4? dst = default)
    {
        dst = dst ?? new Vec4();
        dst[0] = a[0] * b[0];
        dst[1] = a[1] * b[1];
        dst[2] = a[2] * b[2];
        dst[3] = a[3] * b[3];
        return dst;
    }

    /// <summary>
    /// Multiplies a vector by another vector (component-wise). (same as multiply)
    /// </summary>
    public static Vec4 Mul(Vec4 a, Vec4 b, Vec4? dst = default) => Multiply(a, b, dst);

    /// <summary>
    /// Divides a vector by another vector (component-wise).
    /// </summary>
    public static Vec4 Divide(Vec4 a, Vec4 b, Vec4? dst = default)
    {
        dst = dst ?? new Vec4();
        dst[0] = a[0] / b[0];
        dst[1] = a[1] / b[1];
        dst[2] = a[2] / b[2];
        dst[3] = a[3] / b[3];
        return dst;
    }

    /// <summary>
    /// Divides a vector by another vector (component-wise). (same as divide)
    /// </summary>
    public static Vec4 Div(Vec4 a, Vec4 b, Vec4? dst = default) => Divide(a, b, dst);

    /// <summary>
    /// Zero's a vector
    /// </summary>
    public static Vec4 Zero(Vec4? dst = default)
    {
        dst = dst ?? new Vec4();
        dst[0] = 0;
        dst[1] = 0;
        dst[2] = 0;
        dst[3] = 0;
        return dst;
    }

    /// <summary>
    /// Transform Vec4 by 4x4 matrix
    /// </summary>
    public static Vec4 TransformMat4(Vec4 v, Mat4 m, Vec4? dst = default)
    {
        dst = dst ?? new Vec4();
        var x = v[0];
        var y = v[1];
        var z = v[2];
        var w = v[3];

        dst[0] = m[0] * x + m[4] * y + m[8] * z + m[12] * w;
        dst[1] = m[1] * x + m[5] * y + m[9] * z + m[13] * w;
        dst[2] = m[2] * x + m[6] * y + m[10] * z + m[14] * w;
        dst[3] = m[3] * x + m[7] * y + m[11] * z + m[15] * w;

        return dst;
    }

    /// <summary>
    /// Treat a 4D vector as a direction and set it's length
    /// </summary>
    public static Vec4 SetLength(Vec4 a, float len, Vec4? dst = default)
    {
        dst = dst ?? new Vec4();
        Normalize(a, dst);
        return MulScalar(dst, len, dst);
    }

    /// <summary>
    /// Ensure a vector is not longer than a max length
    /// </summary>
    public static Vec4 Truncate(Vec4 a, float maxLen, Vec4? dst = default)
    {
        dst = dst ?? new Vec4();
        if (Length(a) > maxLen)
        {
            return SetLength(a, maxLen, dst);
        }
        return Copy(a, dst);
    }

    /// <summary>
    /// Return the vector exactly between 2 endpoint vectors
    /// </summary>
    public static Vec4 Midpoint(Vec4 a, Vec4 b, Vec4? dst = default)
    {
        dst = dst ?? new Vec4();
        return Lerp(a, b, 0.5f, dst);
    }

    public override int GetHashCode()
    {
        return (int)(this[0] * this[1] * this[2] * this[3]);
    }
}
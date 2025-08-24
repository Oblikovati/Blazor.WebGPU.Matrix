using Blazor.WebGPU.Matrix.Internal;
using SpawnDev.BlazorJS.JSObjects;

namespace Blazor.WebGPU.Matrix;

public class Vec2 : BaseArray<float>
{
    /// <summary>
    /// 
    /// </summary>
    public Vec2() : base(2) { }

    private Vec2(float x, float y) : base(2)
    {
        _elements[0] = x;
        _elements[1] = y;
    }

    /// <summary>
    /// Returns a JavaScript Float32Array
    /// </summary>
    public override TypedArray<float> Array => new Float32Array(_elements);

    /// <summary>
    /// Adds two vectors; assumes a and b have the same dimension.
    /// </summary>
    public static Vec2 Add(Vec2 a, Vec2 b, Vec2? dst = default)
    {
        dst = dst ?? new Vec2();
        dst[0] = a[0] + b[0];
        dst[1] = a[1] + b[1];
        return dst;
    }

    /// <summary>
    /// Adds two vectors, scaling the 2nd; assumes a and b have the same dimension.
    /// </summary>
    public static Vec2 AddScaled(Vec2 a, Vec2 b, float scale, Vec2? dst = default)
    {
        dst = dst ?? new Vec2();
        dst[0] = a[0] + b[0] * scale;
        dst[1] = a[1] + b[1] * scale;
        return dst;
    }

    /// <summary>
    /// Returns the angle in radians between two vectors.
    /// </summary>
    public static float Angle(Vec2 a, Vec2 b)
    {
        var ax = a[0];
        var ay = a[1];
        var bx = b[0];
        var by = b[1];
        var mag1 = Math.Sqrt(ax * ax + ay * ay);
        var mag2 = Math.Sqrt(bx * bx + by * by);
        var mag = mag1 * mag2;
        var cosine = mag != 0 ? Dot(a, b) / mag : 0;
        return (float) Math.Acos(cosine);
    }

    /// <summary>
    /// Applies MathF.ceil to each element of vector
    /// </summary>
    public static Vec2 Ceil(Vec2 v, Vec2? dst = default)
    {
        dst = dst ?? new Vec2();
        dst[0] = MathF.Ceiling(v[0]);
        dst[1] = MathF.Ceiling(v[1]);
        return dst;
    }

    /// <summary>
    /// Clamp each element of vector between min and max
    /// </summary>
    public static Vec2 Clamp(Vec2 v, float min = 0, float max = 1, Vec2? dst = default)
    {
        dst = dst ?? new Vec2();
        dst[0] = MathF.Min(max, MathF.Max(min, v[0]));
        dst[1] = MathF.Min(max, MathF.Max(min, v[1]));
        return dst;
    }

    /// <summary>
    /// Clones a vector. (same as copy)
    /// </summary>
    public static Vec2 Clone(Vec2 v, Vec2? dst = default) => Copy(v, dst);

    /// <summary>
    /// Copies a vector. (same as clone)
    /// </summary>
    public static Vec2 Copy(Vec2 v, Vec2? dst = default)
    {
        dst = dst ?? new Vec2();
        dst[0] = v[0];
        dst[1] = v[1];
        return dst;
    }

    /// <summary>
    /// Creates a Vec2; may be called with x, y to set initial values.
    /// </summary>
    public static Vec2 Create(float? x = 0, float? y = 0)
    {
        return new Vec2(x ?? 0, y ?? 0);
    }

    /// <summary>
    /// Computes the cross product of two vectors.
    /// </summary>
    public static Vec3 Cross(Vec2 a, Vec2 b, Vec3? dst = default)
    {
        dst = dst ?? Vec3.Create();
        dst[0] = 0;
        dst[1] = 0;
        dst[2] = a[0] * b[1] - a[1] * b[0];
        return dst;
    }

    /// <summary>
    /// Computes the distance between 2 points (same as distance)
    /// </summary>
    public static float Dist(Vec2 a, Vec2 b) => Distance(a, b);

    /// <summary>
    /// Computes the distance between 2 points
    /// </summary>
    public static float Distance(Vec2 a, Vec2 b)
    {
        var dx = a[0] - b[0];
        var dy = a[1] - b[1];
        return MathF.Sqrt(dx * dx + dy * dy);
    }

    /// <summary>
    /// Computes the square of the distance between 2 points
    /// </summary>
    public static float DistanceSq(Vec2 a, Vec2 b)
    {
        var dx = a[0] - b[0];
        var dy = a[1] - b[1];
        return dx * dx + dy * dy;
    }

    /// <summary>
    /// Computes the square of the distance between 2 points (same as distanceSq)
    /// </summary>
    public static float DistSq(Vec2 a, Vec2 b) => DistanceSq(a, b);

    /// <summary>
    /// Divides a vector by another vector (component-wise). (same as divide)
    /// </summary>
    public static Vec2 Div(Vec2 a, Vec2 b, Vec2? dst = default) => Divide(a, b, dst);

    /// <summary>
    /// Divides a vector by another vector (component-wise).
    /// </summary>
    public static Vec2 Divide(Vec2 a, Vec2 b, Vec2? dst = default)
    {
        dst = dst ?? new Vec2();
        dst[0] = a[0] / b[0];
        dst[1] = a[1] / b[1];
        return dst;
    }

    /// <summary>
    /// Divides a vector by a scalar.
    /// </summary>
    public static Vec2 DivScalar(Vec2 v, float k, Vec2? dst = default)
    {
        dst = dst ?? new Vec2();
        dst[0] = v[0] / k;
        dst[1] = v[1] / k;
        return dst;
    }

    /// <summary>
    /// Computes the dot product of two vectors.
    /// </summary>
    public static float Dot(Vec2 a, Vec2 b)
    {
        return a[0] * b[0] + a[1] * b[1];
    }

    /// <summary>
    /// Check if 2 vectors are exactly equal
    /// </summary>
    public static bool Equals(Vec2 a, Vec2 b)
    {
        return a[0] == b[0] && a[1] == b[1];
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj))
            return true;

        if (obj is null) return false;

        if (obj is Vec2 v2)
            return Equals(this, v2);

        return false;
    }

    /// <summary>
    /// Check if 2 vectors are approximately equal
    /// </summary>
    public static bool EqualsApproximately(Vec2 a, Vec2 b)
    {
        return MathF.Abs(a[0] - b[0]) < Utils.EPSILON &&
               MathF.Abs(a[1] - b[1]) < Utils.EPSILON;
    }

    /// <summary>
    /// Applies MathF.floor to each element of vector
    /// </summary>
    public static Vec2 Floor(Vec2 v, Vec2? dst = default)
    {
        dst = dst ?? new Vec2();
        dst[0] = MathF.Floor(v[0]);
        dst[1] = MathF.Floor(v[1]);
        return dst;
    }

    /// <summary>
    /// Creates a Vec2; may be called with x, y to set initial values. (same as create)
    /// </summary>
    public static Vec2 FromValues(float x = 0, float y = 0) => Create(x, y);

    /// <summary>
    /// Inverse a vector.
    /// </summary>
    public static Vec2 Inverse(Vec2 v, Vec2? dst = default)
    {
        dst = dst ?? new Vec2();
        dst[0] = 1.0f / v[0];
        dst[1] = 1.0f / v[1];
        return dst;
    }

    /// <summary>
    /// Invert a vector. (same as inverse)
    /// </summary>
    public Vec2 Invert(Vec2 v, Vec2? dst = default) => Inverse(v, dst);

    /// <summary>
    /// Computes the length of vector (same as length)
    /// </summary>
    public static float Len(Vec2 v) => Length(v);

    /// <summary>
    /// Computes the length of vector
    /// </summary>
    public static float Length(Vec2 v)
    {
        var v0 = v[0];
        var v1 = v[1];
        return MathF.Sqrt(v0 * v0 + v1 * v1);
    }

    /// <summary>
    /// Computes the square of the length of vector
    /// </summary>
    public static float LengthSq(Vec2 v)
    {
        var v0 = v[0];
        var v1 = v[1];
        return v0 * v0 + v1 * v1;
    }

    /// <summary>
    /// Computes the square of the length of vector (same as lengthSq)
    /// </summary>
    public static float LenSq(Vec2 v) => LengthSq(v);

    /// <summary>
    /// Performs linear interpolation on two vectors.
    /// </summary>
    public static Vec2 Lerp(Vec2 a, Vec2 b, float t, Vec2? dst = default)
    {
        dst = dst ?? new Vec2();
        dst[0] = a[0] + t * (b[0] - a[0]);
        dst[1] = a[1] + t * (b[1] - a[1]);
        return dst;
    }

    /// <summary>
    /// Performs linear interpolation on two vectors with vector t.
    /// </summary>
    public static Vec2 LerpV(Vec2 a, Vec2 b, Vec2 t, Vec2? dst = default)
    {
        dst = dst ?? new Vec2();
        dst[0] = a[0] + t[0] * (b[0] - a[0]);
        dst[1] = a[1] + t[1] * (b[1] - a[1]);
        return dst;
    }

    /// <summary>
    /// Return max values of two vectors.
    /// </summary>
    public static Vec2 Max(Vec2 a, Vec2 b, Vec2? dst = default)
    {
        dst = dst ?? new Vec2();
        dst[0] = MathF.Max(a[0], b[0]);
        dst[1] = MathF.Max(a[1], b[1]);
        return dst;
    }

    /// <summary>
    /// Return the vector exactly between 2 endpoint vectors
    /// </summary>
    public static Vec2 Midpoint(Vec2 a, Vec2 b, Vec2? dst = default)
    {
        dst = dst ?? new Vec2();
        return Lerp(a, b, 0.5f, dst);
    }

    /// <summary>
    /// Return min values of two vectors.
    /// </summary>
    public static Vec2 Min(Vec2 a, Vec2 b, Vec2? dst = default)
    {
        dst = dst ?? new Vec2();
        dst[0] = MathF.Min(a[0], b[0]);
        dst[1] = MathF.Min(a[1], b[1]);
        return dst;
    }

    /// <summary>
    /// Multiplies a vector by another vector (component-wise). (same as multiply)
    /// </summary>
    public static Vec2 Mul(Vec2 a, Vec2 b, Vec2? dst = default) => Multiply(a, b, dst);

    /// <summary>
    /// Multiplies a vector by a scalar.
    /// </summary>
    public static Vec2 MulScalar(Vec2 v, float k, Vec2? dst = default)
    {
        dst = dst ?? new Vec2();
        dst[0] = (float) ((double)v[0] * (double)k);
        dst[1] = (float) ((double)v[1] * (double)k);
        return dst;
    }

    /// <summary>
    /// Multiplies a vector by another vector (component-wise).
    /// </summary>
    public static Vec2 Multiply(Vec2 a, Vec2 b, Vec2? dst = default)
    {
        dst = dst ?? new Vec2();
        dst[0] = a[0] * b[0];
        dst[1] = a[1] * b[1];
        return dst;
    }

    /// <summary>
    /// Negates a vector.
    /// </summary>
    public static Vec2 Negate(Vec2 v, Vec2? dst = default)
    {
        dst = dst ?? new Vec2();
        dst[0] = -v[0];
        dst[1] = -v[1];
        return dst;
    }

    /// <summary>
    /// Divides a vector by its Euclidean length and returns the quotient.
    /// </summary>
    public static Vec2 Normalize(Vec2 v, Vec2? dst = default)
    {
        dst = dst ?? new Vec2();
        var v0 = v[0];
        var v1 = v[1];
        var len = Math.Sqrt(v0 * v0 + v1 * v1);

        if (len > float.Epsilon)
        {
            dst[0] =(float) (v0 / len);
            dst[1] =(float) (v1 / len);
        }
        else
        {
            dst[0] = 0;
            dst[1] = 0;
        }

        return dst;
    }

    /// <summary>
    /// Creates a random unit vector * scale
    /// </summary>
    public static Vec2 Random(float scale = 1, Vec2? dst = default)
    {
        dst = dst ?? new Vec2();
        var angle = new Random().Next() * 2 * MathF.PI;
        dst[0] = MathF.Cos(angle) * scale;
        dst[1] = MathF.Sin(angle) * scale;
        return dst;
    }

    /// <summary>
    /// Rotate a 2D vector
    /// </summary>
    public static Vec2 Rotate(Vec2 a, Vec2 b, float rad, Vec2? dst = default)
    {
        dst = dst ?? new Vec2();
        // Translate point to the origin
        var p0 = a[0] - b[0];
        var p1 = a[1] - b[1];
        var sinC = MathF.Sin(rad);
        var cosC = MathF.Cos(rad);

        // Perform rotation and translate to correct position
        dst[0] = p0 * cosC - p1 * sinC + b[0];
        dst[1] = p0 * sinC + p1 * cosC + b[1];
        return dst;
    }

    /// <summary>
    /// Applies MathF.round to each element of vector
    /// </summary>
    public static Vec2 Round(Vec2 v, Vec2? dst = default)
    {
        dst = dst ?? new Vec2();
        dst[0] = MathF.Round(v[0]);
        dst[1] = MathF.Round(v[1]);
        return dst;
    }

    /// <summary>
    /// Multiplies a vector by a scalar. (same as mulScalar)
    /// </summary>
    public static Vec2 Scale(Vec2 v, float k, Vec2? dst = default) => MulScalar(v, k, dst);

    /// <summary>
    /// Sets the values of a Vec2
    /// </summary>
    public static Vec2 Set(float x, float y, Vec2? dst = default)
    {
        dst = dst ?? new Vec2();
        dst[0] = x;
        dst[1] = y;
        return dst;
    }

    /// <summary>
    /// Treat a 2D vector as a direction and set it's length
    /// </summary>
    public static Vec2 SetLength(Vec2 a, float len, Vec2? dst = default)
    {
        dst = dst ?? new Vec2();
        dst = Normalize(a, dst);
        return MulScalar(dst, len, dst);
    }

    /// <summary>
    /// Subtracts two vectors. (same as subtract)
    /// </summary>
    public static Vec2 Sub(Vec2 a, Vec2 b, Vec2? dst = default) => Subtract(a, b, dst);

    /// <summary>
    /// Subtracts two vectors.
    /// </summary>
    public static Vec2 Subtract(Vec2 a, Vec2 b, Vec2? dst = default)
    {
        dst = dst ?? new Vec2();
        dst[0] = a[0] - b[0];
        dst[1] = a[1] - b[1];
        return dst;
    }

    /// <summary>
    /// Transform Vec2 by 3x3 matrix
    /// </summary>
    public static Vec2 TransformMat3(Vec2 v, Mat3 m, Vec2? dst = default)
    {
        dst = dst ?? new Vec2();
        var x = v[0];
        var y = v[1];
        dst[0] = m[0] * x + m[4] * y + m[8];
        dst[1] = m[1] * x + m[5] * y + m[9];
        return dst;
    }

    /// <summary>
    /// Transform Vec2 by 4x4 matrix
    /// </summary>
    public static Vec2 TransformMat4(Vec2 v, Mat4 m, Vec2? dst = default)
    {
        dst = dst ?? new Vec2();
        var x = v[0];
        var y = v[1];
        dst[0] = x * m[0] + y * m[4] + m[12];
        dst[1] = x * m[1] + y * m[5] + m[13];
        return dst;
    }

    /// <summary>
    /// Ensure a vector is not longer than a max length
    /// </summary>
    public static Vec2 Truncate(Vec2 a, float maxLen, Vec2? dst = default)
    {
        dst = dst ?? new Vec2();
        if (Length(a) > maxLen)
        {
            return SetLength(a, maxLen, dst);
        }
        return Copy(a, dst);
    }

    /// <summary>
    /// Zero's a vector
    /// </summary>
    public static Vec2 Zero(Vec2? dst = default)
    {
        dst = dst ?? new Vec2();
        dst[0] = 0;
        dst[1] = 0;
        return dst;
    }

    public override int GetHashCode()
    {
        return (int) (this[0] * this[1]);
    }
}
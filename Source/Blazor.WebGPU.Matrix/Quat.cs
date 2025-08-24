using Microsoft.JSInterop;
using SpawnDev.BlazorJS.JSObjects;

namespace Blazor.WebGPU.Matrix;

public class Quat : Float32Array
{
    public Quat(IJSInProcessObjectReference _ref) : base(_ref) { }

    private Quat() : base(new Float32Array(4).JSRef!) { }

    /// <summary>
    /// Creates a quat4; may be called with x, y, z, w to set initial values.
    /// </summary>
    public static Quat Create(float x = 0, float y = 0, float z = 0, float w = 1)
    {
        var dst = new Quat();
        dst[0] = x;
        dst[1] = y;
        dst[2] = z;
        dst[3] = w;
        return dst;
    }

    /// <summary>
    /// Creates a Quat; may be called with x, y, z, w to set initial values. (same as create)
    /// </summary>
    public static Quat FromValues(float x = 0, float y = 0, float z = 0, float w = 1) => Create(x, y, z, w);

    /// <summary>
    /// Sets the values of a Quat
    /// </summary>
    public static Quat Set(float x, float y, float z, float w, Quat? dst = default)
    {
        dst = dst ?? new Quat();
        dst[0] = x;
        dst[1] = y;
        dst[2] = z;
        dst[3] = w;
        return dst;
    }

    /// <summary>
    /// Sets a quaternion from the given angle and axis, then returns it.
    /// </summary>
    public static Quat FromAxisAngle(Vec3 axis, float angleInRadians, Quat? dst = default)
    {
        dst = dst ?? new Quat();
        var halfAngle = angleInRadians * 0.5f;
        var s = MathF.Sin(halfAngle);

        dst[0] = s * axis[0];
        dst[1] = s * axis[1];
        dst[2] = s * axis[2];
        dst[3] = MathF.Cos(halfAngle);

        return dst;
    }

    /// <summary>
    /// Gets the rotation axis and angle
    /// </summary>
    public static (float angle, Vec3 axis) ToAxisAngle(Quat q, Vec3? dst = default)
    {
        dst = dst ?? Vec3.Create();

        var angle = MathF.Acos(q[3]) * 2;
        var s = MathF.Sin(angle * 0.5f);
        if (s > Utils.EPSILON)
        {
            dst[0] = q[0] / s;
            dst[1] = q[1] / s;
            dst[2] = q[2] / s;
        }
        else
        {
            dst[0] = 1;
            dst[1] = 0;
            dst[2] = 0;
        }

        return (angle, dst);
    }

    /// <summary>
    /// Returns the angle in radians between two rotations a and b.
    /// </summary>
    public static float Angle(Quat a, Quat b)
    {
        var d = Dot(a, b);
        return MathF.Acos(2 * d * d - 1);
    }

    /// <summary>
    /// Multiplies two quaternions
    /// </summary>
    public static Quat Multiply(Quat a, Quat b, Quat? dst = default)
    {
        dst = dst ?? new Quat();
        var ax = a[0];
        var ay = a[1];
        var az = a[2];
        var aw = a[3];

        var bx = b[0];
        var by = b[1];
        var bz = b[2];
        var bw = b[3];

        dst[0] = ax * bw + aw * bx + ay * bz - az * by;
        dst[1] = ay * bw + aw * by + az * bx - ax * bz;
        dst[2] = az * bw + aw * bz + ax * by - ay * bx;
        dst[3] = aw * bw - ax * bx - ay * by - az * bz;

        return dst;
    }

    /// <summary>
    /// Multiplies two quaternions (same as multiply)
    /// </summary>
    public static Quat Mul(Quat a, Quat b, Quat? dst = default) => Multiply(a, b, dst);

    /// <summary>
    /// Rotates the given quaternion around the X axis by the given angle.
    /// </summary>
    public static Quat RotateX(Quat q, float angleInRadians, Quat? dst = default)
    {
        dst = dst ?? new Quat();
        var halfAngle = angleInRadians * 0.5f;

        var qx = q[0];
        var qy = q[1];
        var qz = q[2];
        var qw = q[3];

        var bx = MathF.Sin(halfAngle);
        var bw = MathF.Cos(halfAngle);

        dst[0] = qx * bw + qw * bx;
        dst[1] = qy * bw + qz * bx;
        dst[2] = qz * bw - qy * bx;
        dst[3] = qw * bw - qx * bx;

        return dst;
    }

    /// <summary>
    /// Rotates the given quaternion around the Y axis by the given angle.
    /// </summary>
    public static Quat RotateY(Quat q, float angleInRadians, Quat? dst = default)
    {
        dst = dst ?? new Quat();
        var halfAngle = angleInRadians * 0.5f;

        var qx = q[0];
        var qy = q[1];
        var qz = q[2];
        var qw = q[3];

        var by = MathF.Sin(halfAngle);
        var bw = MathF.Cos(halfAngle);

        dst[0] = qx * bw - qz * by;
        dst[1] = qy * bw + qw * by;
        dst[2] = qz * bw + qx * by;
        dst[3] = qw * bw - qy * by;

        return dst;
    }

    /// <summary>
    /// Rotates the given quaternion around the Z axis by the given angle.
    /// </summary>
    public static Quat RotateZ(Quat q, float angleInRadians, Quat? dst = default)
    {
        dst = dst ?? new Quat();
        var halfAngle = angleInRadians * 0.5f;

        var qx = q[0];
        var qy = q[1];
        var qz = q[2];
        var qw = q[3];

        var bz = MathF.Sin(halfAngle);
        var bw = MathF.Cos(halfAngle);

        dst[0] = qx * bw + qy * bz;
        dst[1] = qy * bw - qx * bz;
        dst[2] = qz * bw + qw * bz;
        dst[3] = qw * bw - qz * bz;

        return dst;
    }

    /// <summary>
    /// Spherically linear interpolate between two quaternions
    /// </summary>
    public static Quat Slerp(Quat a, Quat b, float t, Quat? dst = default)
    {
        dst = dst ?? new Quat();
        var ax = a[0];
        var ay = a[1];
        var az = a[2];
        var aw = a[3];

        var bx = b[0];
        var by = b[1];
        var bz = b[2];
        var bw = b[3];

        var cosOmega = ax * bx + ay * by + az * bz + aw * bw;

        if (cosOmega < 0)
        {
            cosOmega = -cosOmega;
            bx = -bx;
            by = -by;
            bz = -bz;
            bw = -bw;
        }

        float scale0, scale1;

        if (1.0f - cosOmega > Utils.EPSILON)
        {
            var omega = MathF.Acos(cosOmega);
            var sinOmega = MathF.Sin(omega);
            scale0 = MathF.Sin((1 - t) * omega) / sinOmega;
            scale1 = MathF.Sin(t * omega) / sinOmega;
        }
        else
        {
            scale0 = 1.0f - t;
            scale1 = t;
        }

        dst[0] = scale0 * ax + scale1 * bx;
        dst[1] = scale0 * ay + scale1 * by;
        dst[2] = scale0 * az + scale1 * bz;
        dst[3] = scale0 * aw + scale1 * bw;

        return dst;
    }

    /// <summary>
    /// Compute the inverse of a quaternion
    /// </summary>
    public static Quat Inverse(Quat q, Quat? dst = default)
    {
        dst = dst ?? new Quat();
        var a0 = q[0];
        var a1 = q[1];
        var a2 = q[2];
        var a3 = q[3];

        var dot = a0 * a0 + a1 * a1 + a2 * a2 + a3 * a3;
        var invDot = dot != 0 ? 1 / dot : 0;

        dst[0] = -a0 * invDot;
        dst[1] = -a1 * invDot;
        dst[2] = -a2 * invDot;
        dst[3] = a3 * invDot;

        return dst;
    }

    /// <summary>
    /// Compute the conjugate of a quaternion
    /// For quaternions with a magnitude of 1 (a unit quaternion)
    /// this returns the same as the inverse but is faster to calculate.
    /// </summary>
    public static Quat Conjugate(Quat q, Quat? dst = default)
    {
        dst = dst ?? new Quat();
        dst[0] = -q[0];
        dst[1] = -q[1];
        dst[2] = -q[2];
        dst[3] = q[3];

        return dst;
    }

    /// <summary>
    /// Creates a quaternion from the given rotation matrix.
    /// The created quaternion is not normalized.
    /// </summary>
    public static Quat FromMat(Mat3 m, Quat? dst = default)
    {
        dst = dst ?? new Quat();
        // Algorithm in Ken Shoemake's article in 1987 SIGGRAPH course notes
        // article "Quaternion Calculus and Fast Animation".
        var trace = m[0] + m[5] + m[10];

        if (trace > 0.0f)
        {
            // |w| > 1/2, may as well choose w > 1/2
            var root = MathF.Sqrt(trace + 1); // 2w
            dst[3] = 0.5f * root;
            var invRoot = 0.5f / root; // 1/(4w)

            dst[0] = (m[6] - m[9]) * invRoot;
            dst[1] = (m[8] - m[2]) * invRoot;
            dst[2] = (m[1] - m[4]) * invRoot;
        }
        else
        {
            // |w| <= 1/2
            int i = 0;

            if (m[5] > m[0])
            {
                i = 1;
            }
            if (m[10] > m[i * 4 + i])
            {
                i = 2;
            }

            var j = (i + 1) % 3;
            var k = (i + 2) % 3;

            var root = MathF.Sqrt(m[i * 4 + i] - m[j * 4 + j] - m[k * 4 + k] + 1.0f);
            dst[i] = 0.5f * root;

            var invRoot = 0.5f / root;

            dst[3] = (m[j * 4 + k] - m[k * 4 + j]) * invRoot;
            dst[j] = (m[j * 4 + i] + m[i * 4 + j]) * invRoot;
            dst[k] = (m[k * 4 + i] + m[i * 4 + k]) * invRoot;
        }

        return dst;
    }

    /// <summary>
    /// Creates a quaternion from the given euler angle x, y, z using the provided intrinsic order for the conversion.
    /// </summary>
    public static Quat FromEuler(float xAngleInRadians, float yAngleInRadians, float zAngleInRadians, string order, Quat? dst = default)
    {
        dst = dst ?? new Quat();
        var xHalfAngle = xAngleInRadians * 0.5f;
        var yHalfAngle = yAngleInRadians * 0.5f;
        var zHalfAngle = zAngleInRadians * 0.5f;

        var sx = MathF.Sin(xHalfAngle);
        var cx = MathF.Cos(xHalfAngle);
        var sy = MathF.Sin(yHalfAngle);
        var cy = MathF.Cos(yHalfAngle);
        var sz = MathF.Sin(zHalfAngle);
        var cz = MathF.Cos(zHalfAngle);

        switch (order)
        {
            case "xyz":
                dst[0] = sx * cy * cz + cx * sy * sz;
                dst[1] = cx * sy * cz - sx * cy * sz;
                dst[2] = cx * cy * sz + sx * sy * cz;
                dst[3] = cx * cy * cz - sx * sy * sz;
                break;

            case "xzy":
                dst[0] = sx * cy * cz - cx * sy * sz;
                dst[1] = cx * sy * cz - sx * cy * sz;
                dst[2] = cx * cy * sz + sx * sy * cz;
                dst[3] = cx * cy * cz + sx * sy * sz;
                break;

            case "yxz":
                dst[0] = sx * cy * cz + cx * sy * sz;
                dst[1] = cx * sy * cz - sx * cy * sz;
                dst[2] = cx * cy * sz - sx * sy * cz;
                dst[3] = cx * cy * cz + sx * sy * sz;
                break;

            case "yzx":
                dst[0] = sx * cy * cz + cx * sy * sz;
                dst[1] = cx * sy * cz + sx * cy * sz;
                dst[2] = cx * cy * sz - sx * sy * cz;
                dst[3] = cx * cy * cz - sx * sy * sz;
                break;

            case "zxy":
                dst[0] = sx * cy * cz - cx * sy * sz;
                dst[1] = cx * sy * cz + sx * cy * sz;
                dst[2] = cx * cy * sz + sx * sy * cz;
                dst[3] = cx * cy * cz - sx * sy * sz;
                break;

            case "zyx":
                dst[0] = sx * cy * cz - cx * sy * sz;
                dst[1] = cx * sy * cz + sx * cy * sz;
                dst[2] = cx * cy * sz - sx * sy * cz;
                dst[3] = cx * cy * cz + sx * sy * sz;
                break;

            default:
                throw new ArgumentException($"Unknown rotation order: {order}");
        }

        return dst;
    }

    /// <summary>
    /// Copies a quaternion. (same as clone)
    /// </summary>
    public static Quat Copy(Quat q, Quat? dst = default)
    {
        dst = dst ?? new Quat();
        dst[0] = q[0];
        dst[1] = q[1];
        dst[2] = q[2];
        dst[3] = q[3];

        return dst;
    }

    /// <summary>
    /// Clones a quaternion. (same as copy)
    /// </summary>
    public static Quat Clone(Quat q, Quat? dst = default) => Copy(q, dst);

    /// <summary>
    /// Adds two quaternions; assumes a and b have the same dimension.
    /// </summary>
    public static Quat Add(Quat a, Quat b, Quat? dst = default)
    {
        dst = dst ?? new Quat();
        dst[0] = a[0] + b[0];
        dst[1] = a[1] + b[1];
        dst[2] = a[2] + b[2];
        dst[3] = a[3] + b[3];

        return dst;
    }

    /// <summary>
    /// Subtracts two quaternions.
    /// </summary>
    public static Quat Subtract(Quat a, Quat b, Quat? dst = default)
    {
        dst = dst ?? new Quat();
        dst[0] = a[0] - b[0];
        dst[1] = a[1] - b[1];
        dst[2] = a[2] - b[2];
        dst[3] = a[3] - b[3];

        return dst;
    }

    /// <summary>
    /// Subtracts two quaternions. (same as subtract)
    /// </summary>
    public static Quat Sub(Quat a, Quat b, Quat? dst = default) => Subtract(a, b, dst);

    /// <summary>
    /// Multiplies a quaternion by a scalar.
    /// </summary>
    public static Quat MulScalar(Quat v, float k, Quat? dst = default)
    {
        dst = dst ?? new Quat();
        dst[0] = v[0] * k;
        dst[1] = v[1] * k;
        dst[2] = v[2] * k;
        dst[3] = v[3] * k;

        return dst;
    }

    /// <summary>
    /// Multiplies a quaternion by a scalar. (same as mulScalar)
    /// </summary>
    public static Quat Scale(Quat v, float k, Quat? dst = default) => MulScalar(v, k, dst);

    /// <summary>
    /// Divides a vector by a scalar.
    /// </summary>
    public static Quat DivScalar(Quat v, float k, Quat? dst = default)
    {
        dst = dst ?? new Quat();
        dst[0] = v[0] / k;
        dst[1] = v[1] / k;
        dst[2] = v[2] / k;
        dst[3] = v[3] / k;

        return dst;
    }

    /// <summary>
    /// Computes the dot product of two quaternions
    /// </summary>
    public static float Dot(Quat a, Quat b)
    {
        return a[0] * b[0] + a[1] * b[1] + a[2] * b[2] + a[3] * b[3];
    }

    /// <summary>
    /// Performs linear interpolation on two quaternions.
    /// Given quaternions a and b and interpolation coefficient t, returns
    /// a + t * (b - a).
    /// </summary>
    public static Quat Lerp(Quat a, Quat b, float t, Quat? dst = default)
    {
        dst = dst ?? new Quat();
        dst[0] = a[0] + t * (b[0] - a[0]);
        dst[1] = a[1] + t * (b[1] - a[1]);
        dst[2] = a[2] + t * (b[2] - a[2]);
        dst[3] = a[3] + t * (b[3] - a[3]);

        return dst;
    }

    /// <summary>
    /// Computes the length of quaternion
    /// </summary>
    public static float Length(Quat v)
    {
        var v0 = v[0];
        var v1 = v[1];
        var v2 = v[2];
        var v3 = v[3];
        return MathF.Sqrt(v0 * v0 + v1 * v1 + v2 * v2 + v3 * v3);
    }

    /// <summary>
    /// Computes the length of quaternion (same as length)
    /// </summary>
    public static float Len(Quat v) => Length(v);

    /// <summary>
    /// Computes the square of the length of quaternion
    /// </summary>
    public static float LengthSq(Quat v)
    {
        var v0 = v[0];
        var v1 = v[1];
        var v2 = v[2];
        var v3 = v[3];
        return v0 * v0 + v1 * v1 + v2 * v2 + v3 * v3;
    }

    /// <summary>
    /// Computes the square of the length of quaternion (same as lengthSq)
    /// </summary>
    public static float LenSq(Quat v) => LengthSq(v);

    /// <summary>
    /// Divides a quaternion by its Euclidean length and returns the quotient.
    /// </summary>
    public static Quat Normalize(Quat v, Quat? dst = default)
    {
        dst = dst ?? new Quat();
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
            dst[3] = 1;
        }

        return dst;
    }

    /// <summary>
    /// Check if 2 quaternions are approximately equal
    /// </summary>
    public static bool EqualsApproximately(Quat a, Quat b)
    {
        return MathF.Abs(a[0] - b[0]) < Utils.EPSILON &&
               MathF.Abs(a[1] - b[1]) < Utils.EPSILON &&
               MathF.Abs(a[2] - b[2]) < Utils.EPSILON &&
               MathF.Abs(a[3] - b[3]) < Utils.EPSILON;
    }

    /// <summary>
    /// Check if 2 quaternions are exactly equal
    /// </summary>
    public static bool Equals(Quat a, Quat b)
    {
        return a[0] == b[0] && a[1] == b[1] && a[2] == b[2] && a[3] == b[3];
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj))
            return true;

        if (obj is null) return false;

        if (obj is Quat q)
            return Equals(this, q);

        return false;
    }

    /// <summary>
    /// Creates an identity quaternion
    /// </summary>
    public static Quat Identity(Quat? dst = default)
    {
        dst = dst ?? new Quat();
        dst[0] = 0;
        dst[1] = 0;
        dst[2] = 0;
        dst[3] = 1;

        return dst;
    }

    private static readonly Vec3 TempVec3 = Vec3.Create();
    private static readonly Vec3 XUnitVec3 = Vec3.Create(1, 0, 0);
    private static readonly Vec3 YUnitVec3 = Vec3.Create(0, 1, 0);

    /// <summary>
    /// Computes a quaternion to represent the shortest rotation from one vector to another.
    /// </summary>
    public static Quat RotationTo(Vec3 aUnit, Vec3 bUnit, Quat? dst = default)
    {
        dst = dst ?? new Quat();
        var dot = Vec3.Dot(aUnit, bUnit);

        if (dot < -0.999999f)
        {
            Vec3.Cross(XUnitVec3, aUnit, TempVec3);
            if (Vec3.Length(TempVec3) < 0.000001f)
            {
                Vec3.Cross(YUnitVec3, aUnit, TempVec3);
            }

            Vec3.Normalize(TempVec3, TempVec3);
            FromAxisAngle(TempVec3, MathF.PI, dst);

            return dst;
        }
        else if (dot > 0.999999f)
        {
            dst[0] = 0;
            dst[1] = 0;
            dst[2] = 0;
            dst[3] = 1;

            return dst;
        }
        else
        {
            Vec3.Cross(aUnit, bUnit, TempVec3);

            dst[0] = TempVec3[0];
            dst[1] = TempVec3[1];
            dst[2] = TempVec3[2];
            dst[3] = 1 + dot;

            return Normalize(dst, dst);
        }
    }

    private static readonly Quat TempQuat1 = Create();
    private static readonly Quat TempQuat2 = Create();

    /// <summary>
    /// Performs a spherical linear interpolation with two control points
    /// </summary>
    public static Quat Sqlerp(Quat a, Quat b, Quat c, Quat d, float t, Quat? dst = default)
    {
        dst = dst ?? new Quat();
        Slerp(a, d, t, TempQuat1);
        Slerp(b, c, t, TempQuat2);
        Slerp(TempQuat1, TempQuat2, 2 * t * (1 - t), dst);

        return dst;
    }

    public override int GetHashCode()
    {
        return (int)(this[0] * this[1] * this[2] * this[3]);
    }
}
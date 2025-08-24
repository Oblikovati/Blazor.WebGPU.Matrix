using SpawnDev.Blazor.UnitTesting;

namespace Blazor.WebGPU.Matrix.Tests.Float;

[TestClass]
public class QuatFloatTests
{
    private static bool QuatEqualsApprox(Quat a, Quat b, float epsilon = 1e-6f)
    {
        return MathF.Abs(a[0] - b[0]) < epsilon &&
               MathF.Abs(a[1] - b[1]) < epsilon &&
               MathF.Abs(a[2] - b[2]) < epsilon &&
               MathF.Abs(a[3] - b[3]) < epsilon;
    }

    private static bool QuatEqualsExact(Quat a, Quat b)
    {
        return a[0] == b[0] && a[1] == b[1] && a[2] == b[2] && a[3] == b[3];
    }

    [TestMethod]
    public void Add_Should_Add_Quaternions()
    {
        var a = Quat.Create(1, 2, 3, 4);
        var b = Quat.Create(2, 3, 4, 5);
        var expected = Quat.Create(3, 5, 7, 9);

        var resultWithDest = Quat.Add(a, b, Quat.Create());
        Assert.True(QuatEqualsApprox(resultWithDest, expected));
        Assert.NotSame(a, resultWithDest);
        Assert.NotSame(b, resultWithDest);

        var resultWithoutDest = Quat.Add(a, b);
        Assert.True(QuatEqualsApprox(resultWithoutDest, expected));
        Assert.NotSame(a, resultWithoutDest);
        Assert.NotSame(b, resultWithoutDest);
    }

    [TestMethod]
    public void Equals_Approximately_Should_Compare_With_Tolerance()
    {
        var epsilon = Utils.EPSILON;
        Assert.True(Quat.EqualsApproximately(Quat.Create(1, 2, 3, 4), Quat.Create(1, 2, 3, 4)));
        Assert.True(Quat.EqualsApproximately(Quat.Create(1, 2, 3, 4), Quat.Create(1 + epsilon * 0.5f, 2, 3, 4)));
        Assert.True(Quat.EqualsApproximately(Quat.Create(1, 2, 3, 4), Quat.Create(1, 2 + epsilon * 0.5f, 3, 4)));
        Assert.True(Quat.EqualsApproximately(Quat.Create(1, 2, 3, 4), Quat.Create(1, 2, 3 + epsilon * 0.5f, 4)));
        Assert.True(Quat.EqualsApproximately(Quat.Create(1, 2, 3, 4), Quat.Create(1, 2, 3, 4 + epsilon * 0.5f)));
        Assert.False(Quat.EqualsApproximately(Quat.Create(1, 2, 3, 4), Quat.Create(1.0001f, 2, 3, 4)));
        Assert.False(Quat.EqualsApproximately(Quat.Create(1, 2, 3, 4), Quat.Create(1, 2.0001f, 3, 4)));
        Assert.False(Quat.EqualsApproximately(Quat.Create(1, 2, 3, 4), Quat.Create(1, 2, 3.0001f, 4)));
        Assert.False(Quat.EqualsApproximately(Quat.Create(1, 2, 3, 4), Quat.Create(1, 2, 3, 4.0001f)));
    }

    [TestMethod]
    public void Equals_Should_Compare_Exactly()
    {
        Assert.True(Quat.Equals(Quat.Create(1, 2, 3, 4), Quat.Create(1, 2, 3, 4)));
        Assert.False(Quat.Equals(Quat.Create(1, 2, 3, 4), Quat.Create(1 + Utils.EPSILON * 0.5f, 2, 3, 4)));
        Assert.False(Quat.Equals(Quat.Create(1, 2, 3, 4), Quat.Create(1, 2 + Utils.EPSILON * 0.5f, 3, 4)));
        Assert.False(Quat.Equals(Quat.Create(1, 2, 3, 4), Quat.Create(1, 2, 3 + Utils.EPSILON * 0.5f, 4)));
        Assert.False(Quat.Equals(Quat.Create(1, 2, 3, 4), Quat.Create(1, 2, 3, 4 + Utils.EPSILON * 0.5f)));
    }

    [TestMethod]
    public void Subtract_Should_Subtract_Quaternions()
    {
        var a = Quat.Create(1, 2, 3, 4);
        var b = Quat.Create(2, 4, 6, 8);
        var expected = Quat.Create(-1, -2, -3, -4);

        var resultWithDest = Quat.Subtract(a, b, Quat.Create());
        Assert.True(QuatEqualsApprox(resultWithDest, expected));

        var resultWithoutDest = Quat.Subtract(a, b);
        Assert.True(QuatEqualsApprox(resultWithoutDest, expected));
    }

    [TestMethod]
    public void Sub_Should_Subtract_Quaternions()
    {
        var a = Quat.Create(1, 2, 3, 4);
        var b = Quat.Create(2, 4, 6, 8);
        var expected = Quat.Create(-1, -2, -3, -4);

        var resultWithDest = Quat.Sub(a, b, Quat.Create());
        Assert.True(QuatEqualsApprox(resultWithDest, expected));

        var resultWithoutDest = Quat.Sub(a, b);
        Assert.True(QuatEqualsApprox(resultWithoutDest, expected));
    }

    [TestMethod]
    public void Lerp_Should_Linearly_Interpolate()
    {
        var a = Quat.Create(1, 2, 3, 4);
        var b = Quat.Create(2, 4, 6, 8);
        var expected = Quat.Create(1.5f, 3, 4.5f, 6);

        var resultWithDest = Quat.Lerp(a, b, 0.5f, Quat.Create());
        Assert.True(QuatEqualsApprox(resultWithDest, expected));

        var resultWithoutDest = Quat.Lerp(a, b, 0.5f);
        Assert.True(QuatEqualsApprox(resultWithoutDest, expected));
    }

    [TestMethod]
    public void Lerp_Should_Handle_Under_0()
    {
        var a = Quat.Create(1, 2, 3, 4);
        var b = Quat.Create(2, 4, 6, 8);
        var expected = Quat.Create(0.5f, 1, 1.5f, 2);

        var resultWithDest = Quat.Lerp(a, b, -0.5f, Quat.Create());
        Assert.True(QuatEqualsApprox(resultWithDest, expected));

        var resultWithoutDest = Quat.Lerp(a, b, -0.5f);
        Assert.True(QuatEqualsApprox(resultWithoutDest, expected));
    }

    [TestMethod]
    public void Lerp_Should_Handle_Over_1()
    {
        var a = Quat.Create(1, 2, 3, 4);
        var b = Quat.Create(2, 4, 6, 8);
        var expected = Quat.Create(2.5f, 5, 7.5f, 10);

        var resultWithDest = Quat.Lerp(a, b, 1.5f, Quat.Create());
        Assert.True(QuatEqualsApprox(resultWithDest, expected));

        var resultWithoutDest = Quat.Lerp(a, b, 1.5f);
        Assert.True(QuatEqualsApprox(resultWithoutDest, expected));
    }

    [TestMethod]
    public void MulScalar_Should_Multiply_By_Scalar()
    {
        var a = Quat.Create(1, 2, 3, 4);
        var scalar = 2.0f;
        var expected = Quat.Create(2, 4, 6, 8);

        var resultWithDest = Quat.MulScalar(a, scalar, Quat.Create());
        Assert.True(QuatEqualsExact(resultWithDest, expected));

        var resultWithoutDest = Quat.MulScalar(a, scalar);
        Assert.True(QuatEqualsExact(resultWithoutDest, expected));
    }

    [TestMethod]
    public void Scale_Should_Multiply_By_Scalar()
    {
        var a = Quat.Create(1, 2, 3, 4);
        var scalar = 2.0f;
        var expected = Quat.Create(2, 4, 6, 8);

        var resultWithDest = Quat.Scale(a, scalar, Quat.Create());
        Assert.True(QuatEqualsExact(resultWithDest, expected));

        var resultWithoutDest = Quat.Scale(a, scalar);
        Assert.True(QuatEqualsExact(resultWithoutDest, expected));
    }

    [TestMethod]
    public void DivScalar_Should_Divide_By_Scalar()
    {
        var a = Quat.Create(1, 2, 3, 4);
        var scalar = 2.0f;
        var expected = Quat.Create(0.5f, 1, 1.5f, 2);

        var resultWithDest = Quat.DivScalar(a, scalar, Quat.Create());
        Assert.True(QuatEqualsExact(resultWithDest, expected));

        var resultWithoutDest = Quat.DivScalar(a, scalar);
        Assert.True(QuatEqualsExact(resultWithoutDest, expected));
    }

    [TestMethod]
    public void Inverse_Should_Compute_Inverse()
    {
        var a = Quat.Create(2, 3, -4, -8);
        // Manual calculation:
        // dot = 2*2 + 3*3 + (-4)*(-4) + (-8)*(-8) = 4 + 9 + 16 + 64 = 93
        // invDot = 1/93
        // inverse = [-2/93, -3/93, 4/93, -8/93]
        var expected = Quat.Create(
            -2.0f / 93.0f,
            -3.0f / 93.0f,
            4.0f / 93.0f,
            -8.0f / 93.0f
        );

        var resultWithDest = Quat.Inverse(a, Quat.Create());
        Assert.True(QuatEqualsApprox(resultWithDest, expected, 1e-5f));

        var resultWithoutDest = Quat.Inverse(a);
        Assert.True(QuatEqualsApprox(resultWithoutDest, expected, 1e-5f));
    }

    [TestMethod]
    public void Dot_Should_Compute_Dot_Product()
    {
        var a = Quat.Create(1, 2, 3, 4);
        var b = Quat.Create(2, 4, 6, 8);
        var expected = 1 * 2 + 2 * 4 + 3 * 6 + 4 * 8;
        var result = Quat.Dot(a, b);
        Assert.Equal(expected, result, 5);
    }

    [TestMethod]
    public void Length_Should_Compute_Length()
    {
        var a = Quat.Create(1, 2, 3, 4);
        var expected = MathF.Sqrt(1 * 1 + 2 * 2 + 3 * 3 + 4 * 4);
        var result = Quat.Length(a);
        Assert.Equal(expected, result, 5);
    }

    [TestMethod]
    public void LengthSq_Should_Compute_Length_Squared()
    {
        var a = Quat.Create(1, 2, 3, 4);
        var expected = 1 * 1 + 2 * 2 + 3 * 3 + 4 * 4;
        var result = Quat.LengthSq(a);
        Assert.Equal(expected, result, 5);
    }

    [TestMethod]
    public void Len_Should_Compute_Length()
    {
        var a = Quat.Create(1, 2, 3, 4);
        var expected = MathF.Sqrt(1 * 1 + 2 * 2 + 3 * 3 + 4 * 4);
        var result = Quat.Len(a);
        Assert.Equal(expected, result, 5);
    }

    [TestMethod]
    public void LenSq_Should_Compute_Length_Squared()
    {
        var a = Quat.Create(1, 2, 3, 4);
        var expected = 1 * 1 + 2 * 2 + 3 * 3 + 4 * 4;
        var result = Quat.LenSq(a);
        Assert.Equal(expected, result, 5);
    }

    [TestMethod]
    public void Normalize_Should_Normalize_Quaternion()
    {
        var a = Quat.Create(1, 2, 3, 4);
        var length = MathF.Sqrt(1 * 1 + 2 * 2 + 3 * 3 + 4 * 4);
        var expected = Quat.Create(1 / length, 2 / length, 3 / length, 4 / length);

        var resultWithDest = Quat.Normalize(a, Quat.Create());
        Assert.True(QuatEqualsApprox(resultWithDest, expected));

        var resultWithoutDest = Quat.Normalize(a);
        Assert.True(QuatEqualsApprox(resultWithoutDest, expected));
    }

    [TestMethod]
    public void Copy_Should_Duplicate_Quaternion()
    {
        var original = Quat.Create(1, 2, 3, 4);
        var resultWithDest = Quat.Copy(original, Quat.Create());
        Assert.True(QuatEqualsExact(resultWithDest, original));
        Assert.NotSame(original, resultWithDest);

        var resultWithoutDest = Quat.Copy(original);
        Assert.True(QuatEqualsExact(resultWithoutDest, original));
        Assert.NotSame(original, resultWithoutDest);
    }

    [TestMethod]
    public void Clone_Should_Duplicate_Quaternion()
    {
        var original = Quat.Create(1, 2, 3, 4);
        var resultWithDest = Quat.Clone(original, Quat.Create());
        Assert.True(QuatEqualsExact(resultWithDest, original));
        Assert.NotSame(original, resultWithDest);

        var resultWithoutDest = Quat.Clone(original);
        Assert.True(QuatEqualsExact(resultWithoutDest, original));
        Assert.NotSame(original, resultWithoutDest);
    }

    [TestMethod]
    public void Set_Should_Assign_Values()
    {
        var expected = Quat.Create(2, 3, 4, 5);

        var resultWithDest = Quat.Set(2, 3, 4, 5, Quat.Create());
        Assert.True(QuatEqualsExact(resultWithDest, expected));

        var resultWithoutDest = Quat.Set(2, 3, 4, 5);
        Assert.True(QuatEqualsExact(resultWithoutDest, expected));
    }

    [TestMethod]
    public void Multiply_Should_Multiply_Quaternions()
    {
        var a = Quat.Create(1, 2, 3, 4);
        var b = Quat.Create(-2, -4, -6, -8);
        // Manual calculation (using the C# Multiply formula):
        // ax=1, ay=2, az=3, aw=4
        // bx=-2, by=-4, bz=-6, bw=-8
        // dst[0] = 1*(-8) + 4*(-2) + 2*(-6) - 3*(-4) = -8 -8 -12 +12 = -16
        // dst[1] = 2*(-8) + 4*(-4) + 3*(-2) - 1*(-6) = -16 -16 -6 +6 = -32
        // dst[2] = 3*(-8) + 4*(-6) + 1*(-4) - 2*(-2) = -24 -24 -4 +4 = -48
        // dst[3] = 4*(-8) - 1*(-2) - 2*(-4) - 3*(-6) = -32 +2 +8 +18 = -4
        var expected = Quat.Create(-16, -32, -48, -4);

        var resultWithDest = Quat.Multiply(a, b, Quat.Create());
        Assert.True(QuatEqualsApprox(resultWithDest, expected));

        var resultWithoutDest = Quat.Multiply(a, b);
        Assert.True(QuatEqualsApprox(resultWithoutDest, expected));
    }

    [TestMethod]
    public void Mul_Should_Multiply_Quaternions()
    {
        var a = Quat.Create(1, 2, 3, 4);
        var b = Quat.Create(-2, -4, -6, -8);
        var expected = Quat.Create(-16, -32, -48, -4);

        var resultWithDest = Quat.Mul(a, b, Quat.Create());
        Assert.True(QuatEqualsApprox(resultWithDest, expected));

        var resultWithoutDest = Quat.Mul(a, b);
        Assert.True(QuatEqualsApprox(resultWithoutDest, expected));
    }

    [TestMethod]
    public void From_Values_Should_Create_Quaternion()
    {
        var expected = Quat.Create(1, 2, 3, 4);
        var result = Quat.FromValues(1, 2, 3, 4);
        Assert.True(QuatEqualsExact(result, expected));
    }

    [TestMethod]
    public void From_Axis_Angle_Should_Create_From_Axis_And_Angle()
    {
        var axis = Vec3.Create(1, 0, 0);
        var angle = 0.0f;
        var expected = Quat.Create(0, 0, 0, 1);

        var resultWithDest = Quat.FromAxisAngle(axis, angle, Quat.Create());
        Assert.True(QuatEqualsApprox(resultWithDest, expected));

        var resultWithoutDest = Quat.FromAxisAngle(axis, angle);
        Assert.True(QuatEqualsApprox(resultWithoutDest, expected));
    }

    [TestMethod]
    public void To_Axis_Angle_Should_Extract_Axis_And_Angle()
    {
        var testCases = new[]
        {
            new { Axis = Vec3.Create(1, 0, 0), Angle = 0.1f },
            new { Axis = Vec3.Create(0, 1, 0), Angle = 0.1f },
            new { Axis = Vec3.Create(0, 0, 1), Angle = 0.1f },
            new { Axis = Vec3.Normalize(Vec3.Create(1, 2, 3)), Angle = 0.1f },
        };

        foreach (var testCase in testCases)
        {
            var q = Quat.FromAxisAngle(testCase.Axis, testCase.Angle);
            var (actualAngle, actualAxis) = Quat.ToAxisAngle(q, Vec3.Create());

            Assert.Equal(testCase.Angle, actualAngle, 4);

            Assert.True(Vec3.EqualsApproximately(testCase.Axis, actualAxis));
        }
    }

    [TestMethod]
    public void Angle_Should_Compute_Angle_Between_Quaternions()
    {
        var testAxes = new[]
        {
            Vec3.Create(1, 0, 0),
            Vec3.Create(0, 1, 0),
            Vec3.Create(0, 0, 1),
            Vec3.Normalize(Vec3.Create(1, 2, 3))
        };

        foreach (var axis in testAxes)
        {
            var q1 = Quat.FromAxisAngle(axis, 0.1f);
            var q2 = Quat.FromAxisAngle(axis, 0.4f);
            var actualAngle = Quat.Angle(q1, q2);
            var expectedAngle = 0.3f;
            Assert.Equal(expectedAngle, actualAngle, 4);
        }
    }

    [TestMethod]
    public void Rotate_X_Should_Rotate_Quaternion_Around_X()
    {
        var tests = new[]
        {
            new { Angle = 0.0f, Expected = Quat.Create(1, 2, 3, 4) },
            new { Angle = MathF.PI, Expected = Quat.Create(4, 3, -2, -1) },
            new { Angle = -MathF.PI, Expected = Quat.Create(-4, -3, 2, 1) },
        };

        var qToRotate = Quat.Create(1, 2, 3, 4);

        foreach (var test in tests)
        {
            var resultWithDest = Quat.RotateX(qToRotate, test.Angle, Quat.Create());
            Assert.True(QuatEqualsApprox(resultWithDest, test.Expected));

            var resultWithoutDest = Quat.RotateX(qToRotate, test.Angle);
            Assert.True(QuatEqualsApprox(resultWithoutDest, test.Expected));
        }
    }

    [TestMethod]
    public void Rotate_Y_Should_Rotate_Quaternion_Around_Y()
    {
        var tests = new[]
        {
            new { Angle = 0.0f, Expected = Quat.Create(1, 2, 3, 4) },
            new { Angle = MathF.PI, Expected = Quat.Create(-3, 4, 1, -2) },
            new { Angle = -MathF.PI, Expected = Quat.Create(3, -4, -1, 2) },
        };

        var qToRotate = Quat.Create(1, 2, 3, 4);

        foreach (var test in tests)
        {
            var resultWithDest = Quat.RotateY(qToRotate, test.Angle, Quat.Create());
            Assert.True(QuatEqualsApprox(resultWithDest, test.Expected));

            var resultWithoutDest = Quat.RotateY(qToRotate, test.Angle);
            Assert.True(QuatEqualsApprox(resultWithoutDest, test.Expected));
        }
    }

    [TestMethod]
    public void Rotate_Z_Should_Rotate_Quaternion_Around_Z()
    {
        var tests = new[]
        {
            new { Angle = 0.0f, Expected = Quat.Create(1, 2, 3, 4) },
            new { Angle = MathF.PI, Expected = Quat.Create(-2, 1, 4, -3) },
            new { Angle = -MathF.PI, Expected = Quat.Create(2, -1, -4, 3) },
        };

        var qToRotate = Quat.Create(1, 2, 3, 4);

        foreach (var test in tests)
        {
            var resultWithDest = Quat.RotateZ(qToRotate, test.Angle, Quat.Create());
            Assert.True(QuatEqualsApprox(resultWithDest, test.Expected));

            var resultWithoutDest = Quat.RotateZ(qToRotate, test.Angle);
            Assert.True(QuatEqualsApprox(resultWithoutDest, test.Expected));
        }
    }

    [TestMethod]
    public void Slerp_Should_Spherically_Interpolate()
    {
        var a1 = Quat.Create(0, 1, 0, 1);
        var b1 = Quat.Create(1, 0, 0, 1);
        var a2 = Quat.Create(0, 1, 0, 1);
        var b2 = Quat.Create(0, 1, 0, 0.5f);
        var a3 = Quat.FromEuler(0.1f, 0.2f, 0.3f, "xyz");
        var b3 = Quat.FromEuler(0.3f, 0.2f, 0.1f, "xyz");

        var tests = new[]
        {
            new { A = a1, B = b1, T = 0.0f, Expected = Quat.Create(0, 1, 0, 1) },
            new { A = a1, B = b1, T = 1.0f, Expected = Quat.Create(1, 0, 0, 1) },
            new { A = a1, B = b1, T = 0.5f, Expected = Quat.Create(0.5f, 0.5f, 0, 1) },
            new { A = a2, B = b2, T = 0.5f, Expected = Quat.Create(0, 1, 0, 0.75f) },
            new { A = a3, B = b3, T = 0.5f, Expected = Quat.Create(0.1089731245591333f, 0.09134010671547867f, 0.10897312455913327f, 0.9838224947381737f) },
        };

        foreach (var test in tests)
        {
            var resultWithDest = Quat.Slerp(test.A, test.B, test.T, Quat.Create());
            Assert.True(QuatEqualsApprox(resultWithDest, test.Expected, 1e-4f));

            var resultWithoutDest = Quat.Slerp(test.A, test.B, test.T);
            Assert.True(QuatEqualsApprox(resultWithoutDest, test.Expected, 1e-4f));
        }
    }

    [TestMethod]
    public void Conjugate_Should_Compute_Conjugate()
    {
        var q = Quat.Create(2, 3, 4, 5);
        var expected = Quat.Create(-2, -3, -4, 5);

        var resultWithDest = Quat.Conjugate(q, Quat.Create());
        Assert.True(QuatEqualsExact(resultWithDest, expected));

        var resultWithoutDest = Quat.Conjugate(q);
        Assert.True(QuatEqualsExact(resultWithoutDest, expected));
    }

    [TestMethod]
    public void From_Mat_Should_Create_From_Matrix()
    {
        var identityMat3 = Mat3.Identity();
        var expectedIdentity = Quat.Create(0, 0, 0, 1);

        var resultFromMat3WithDest = Quat.FromMat(identityMat3, Quat.Create());
        Assert.True(QuatEqualsApprox(resultFromMat3WithDest, expectedIdentity));

        var resultFromMat3WithoutDest = Quat.FromMat(identityMat3);
        Assert.True(QuatEqualsApprox(resultFromMat3WithoutDest, expectedIdentity));

        var rotXMat4 = Mat4.RotationX(MathF.PI);
        var rotXMat3 = Mat3.FromMat4(rotXMat4);
        var expectedRotX = Quat.Create(1, 0, 0, 0);

        var resultRotXFromMat3 = Quat.FromMat(rotXMat3);
        Assert.True(QuatEqualsApprox(resultRotXFromMat3, expectedRotX));
    }

    [TestMethod]
    public void From_Euler_Should_Create_From_Euler_Angles()
    {
        var tests = new[]
        {
            new { Args = new object[] { 0.0f, 0.0f, 0.0f, "xyz" }, Expected = Quat.Create(0, 0, 0, 1) },
            new { Args = new object[] { MathF.PI, 0.0f, 0.0f, "xyz" }, Expected = Quat.Create(1, 0, 0, 0) },
            new { Args = new object[] { 0.0f, MathF.PI, 0.0f, "xyz" }, Expected = Quat.Create(0, 1, 0, 0) },
            new { Args = new object[] { 0.0f, 0.0f, MathF.PI, "xyz" }, Expected = Quat.Create(0, 0, 1, 0) },
        };

        foreach (var test in tests)
        {
            var x = (float)test.Args[0];
            var y = (float)test.Args[1];
            var z = (float)test.Args[2];
            var order = (string)test.Args[3];

            var resultWithDest = Quat.FromEuler(x, y, z, order, Quat.Create());
            Assert.True(QuatEqualsApprox(resultWithDest, test.Expected));

            var resultWithoutDest = Quat.FromEuler(x, y, z, order);
            Assert.True(QuatEqualsApprox(resultWithoutDest, test.Expected));
        }
    }

    [TestMethod]
    public void Identity_Should_Create_Identity_Quaternion()
    {
        var expected = Quat.Create(0, 0, 0, 1);

        var resultWithDest = Quat.Identity(Quat.Create());
        Assert.True(QuatEqualsExact(resultWithDest, expected));

        var resultWithoutDest = Quat.Identity();
        Assert.True(QuatEqualsExact(resultWithoutDest, expected));
    }
}
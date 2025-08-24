using SpawnDev.Blazor.UnitTesting;

namespace Blazor.WebGPU.Matrix.Tests.Float;

[TestClass]
public class Mat3FloatTests
{
    private static Mat3 CreateTestMatrix()
    {
        // Represents:
        // [ 0,  1,  2,  0]
        // [ 4,  5,  6,  0]
        // [ 8,  9, 10,  0]
        // (Column 4 is padding)
        return Mat3.Create(
            0, 1, 2,
            4, 5, 6,
            8, 9, 10
        );
    }

    private static bool Mat3EqualsApprox(Mat3 a, Mat3 b, float epsilon = 1e-6f)
    {
        for (int i = 0; i < 12; i++)
        {
            // Skip padding elements if your comparison logic should ignore them,
            // or include them if they are part of the data.
            // Based on EqualsApproximately, it checks specific indices.
            if (i == 3 || i == 7 || i == 11) continue;
            if (MathF.Abs(a[i] - b[i]) >= epsilon)
                return false;
        }
        return true;
    }

    private static bool Mat3EqualsExact(Mat3 a, Mat3 b)
    {
        return Mat3.Equals(a, b);
    }

    [Fact]
    public void Create_Should_Initialize_Correctly()
    {
        var tests = new[]
        {
            new { Expected = new float[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, Args = new float[] {}},
            new { Expected = new float[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, Args = new float[] {1}},
            new { Expected = new float[] { 1, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, Args = new float[] {1, 2}},
            new { Expected = new float[] { 1, 2, 3, 0, 0, 0, 0, 0, 0, 0, 0 }, Args = new float[] {1, 2, 3}},
            new { Expected = new float[] { 1, 2, 3, 0, 4, 0, 0, 0, 0, 0, 0 }, Args = new float[] {1, 2, 3, 4}},
            new { Expected = new float[] { 1, 2, 3, 0, 4, 5, 0, 0, 0, 0, 0 }, Args = new float[] {1, 2, 3, 4, 5}},
            new { Expected = new float[] { 1, 2, 3, 0, 4, 5, 6, 0, 0, 0, 0 }, Args = new float[] {1, 2, 3, 4, 5, 6}},
            new { Expected = new float[] { 1, 2, 3, 0, 4, 5, 6, 0, 7, 0, 0 }, Args = new float[] {1, 2, 3, 4, 5, 6, 7}},
            new { Expected = new float[] { 1, 2, 3, 0, 4, 5, 6, 0, 7, 8, 0 }, Args = new float[] {1, 2, 3, 4, 5, 6, 7, 8}},
            new { Expected = new float[] { 1, 2, 3, 0, 4, 5, 6, 0, 7, 8, 9 }, Args = new float[] {1, 2, 3, 4, 5, 6, 7, 8, 9 }},
        };

        foreach (var test in tests)
        {
            Mat3 m;
            switch (test.Args.Length)
            {
                case 0: m = Mat3.Create(); break;
                case 1: m = Mat3.Create(test.Args[0]); break;
                case 2: m = Mat3.Create(test.Args[0], test.Args[1]); break;
                case 3: m = Mat3.Create(test.Args[0], test.Args[1], test.Args[2]); break;
                case 4: m = Mat3.Create(test.Args[0], test.Args[1], test.Args[2], test.Args[3]); break;
                case 5: m = Mat3.Create(test.Args[0], test.Args[1], test.Args[2], test.Args[3], test.Args[4]); break;
                case 6: m = Mat3.Create(test.Args[0], test.Args[1], test.Args[2], test.Args[3], test.Args[4], test.Args[5]); break;
                case 7: m = Mat3.Create(test.Args[0], test.Args[1], test.Args[2], test.Args[3], test.Args[4], test.Args[5], test.Args[6]); break;
                case 8: m = Mat3.Create(test.Args[0], test.Args[1], test.Args[2], test.Args[3], test.Args[4], test.Args[5], test.Args[6], test.Args[7]); break;
                case 9: m = Mat3.Create(test.Args[0], test.Args[1], test.Args[2], test.Args[3], test.Args[4], test.Args[5], test.Args[6], test.Args[7], test.Args[8]); break;
                default: throw new InvalidOperationException("Unexpected number of args");
            }

            var expected = Mat3.Set(
                test.Expected[0], test.Expected[1], test.Expected[2],
                test.Expected[4], test.Expected[5], test.Expected[6],
                test.Expected[8], test.Expected[9], test.Expected[10]
            );

            Assert.True(Mat3EqualsExact(m, expected), $"Create failed for args: [{string.Join(", ", test.Args)}]");
        }
    }

    [Fact]
    public void Negate_Should_Negate_Elements()
    {
        var m = CreateTestMatrix();
        var expected = Mat3.Create(
             0, -1, -2,
            -4, -5, -6,
            -8, -9, -10
        );

        var resultWithDest = Mat3.Negate(m, Mat3.Create());
        Assert.True(Mat3EqualsExact(resultWithDest, expected));

        var resultWithoutDest = Mat3.Negate(m);
        Assert.True(Mat3EqualsExact(resultWithoutDest, expected));
    }

    [Fact]
    public void Add_Should_Add_Matrices()
    {
        var m = CreateTestMatrix();
        var expected = Mat3.Create(
             0, 2, 4,
             8, 10, 12,
            16, 18, 20
        );

        var resultWithDest = Mat3.Add(m, m, Mat3.Create());
        Assert.True(Mat3EqualsExact(resultWithDest, expected));

        var resultWithoutDest = Mat3.Add(m, m);
        Assert.True(Mat3EqualsExact(resultWithoutDest, expected));
    }

    [Fact]
    public void Multiply_Scalar_Should_Scale_Matrix()
    {
        var m = CreateTestMatrix();
        var scalar = 2.0f;
        var expected = Mat3.Create(
             0, 2, 4,
             8, 10, 12,
            16, 18, 20
        );

        var resultWithDest = Mat3.MultiplyScalar(m, scalar, Mat3.Create());
        Assert.True(Mat3EqualsExact(resultWithDest, expected));

        var resultWithoutDest = Mat3.MultiplyScalar(m, scalar);
        Assert.True(Mat3EqualsExact(resultWithoutDest, expected));

        // Test alias
        var resultMulScalarWithDest = Mat3.MulScalar(m, scalar, Mat3.Create());
        Assert.True(Mat3EqualsExact(resultMulScalarWithDest, expected));
        var resultMulScalarWithoutDest = Mat3.MulScalar(m, scalar);
        Assert.True(Mat3EqualsExact(resultMulScalarWithoutDest, expected));
    }

    [Fact]
    public void Copy_And_Clone_Should_Duplicate_Matrix()
    {
        var m = CreateTestMatrix();

        var copyWithDest = Mat3.Copy(m, Mat3.Create());
        Assert.True(Mat3EqualsExact(copyWithDest, m));
        Assert.NotSame(m, copyWithDest);

        var copyWithoutDest = Mat3.Copy(m);
        Assert.True(Mat3EqualsExact(copyWithoutDest, m));
        Assert.NotSame(m, copyWithoutDest);

        // Test alias
        var cloneWithDest = Mat3.Clone(m, Mat3.Create());
        Assert.True(Mat3EqualsExact(cloneWithDest, m));
        Assert.NotSame(m, cloneWithDest);

        var cloneWithoutDest = Mat3.Clone(m);
        Assert.True(Mat3EqualsExact(cloneWithoutDest, m));
        Assert.NotSame(m, cloneWithoutDest);
    }

    [Fact]
    public void Equals_Approximately_Should_Compare_With_Tolerance()
    {
        var epsilon = 1e-6f;
        var genAlmostEqualMat = (int i) =>
        {
            var arr = new float[12];
            for (int j = 0; j < 12; j++)
            {
                // Skip padding indices for data comparison logic
                if (j == 3 || j == 7 || j == 11) continue;
                arr[j] = j + (j == i ? 0 : epsilon * 0.5f);
            }
            // Manually set padding to 0 if needed by Create/Set
            arr[3] = arr[7] = arr[11] = 0;
            return Mat3.Set(arr[0], arr[1], arr[2], arr[4], arr[5], arr[6], arr[8], arr[9], arr[10]);
        };

        var genNotAlmostEqualMat = (int i) =>
        {
            var arr = new float[12];
            for (int j = 0; j < 12; j++)
            {
                if (j == 3 || j == 7 || j == 11) continue;
                arr[j] = j + (j == i ? 0 : 1.0001f);
            }
            arr[3] = arr[7] = arr[11] = 0;
            return Mat3.Set(arr[0], arr[1], arr[2], arr[4], arr[5], arr[6], arr[8], arr[9], arr[10]);
        };

        for (int i = 0; i < 9; i++) // Only check data indices 0-2, 4-6, 8-10
        {
            int dataIndex = i + (i / 3); // Maps 0,1,2,3,4,5,6,7,8 to 0,1,2,4,5,6,8,9,10
            var almostEqualA = genAlmostEqualMat(-1);
            var almostEqualB = genAlmostEqualMat(dataIndex);
            Assert.True(Mat3.EqualsApproximately(almostEqualA, almostEqualB), $"EqualsApproximately should be true for i={i}");

            var notAlmostEqualA = genNotAlmostEqualMat(-1);
            var notAlmostEqualB = genNotAlmostEqualMat(dataIndex);
            Assert.False(Mat3.EqualsApproximately(notAlmostEqualA, notAlmostEqualB), $"EqualsApproximately should be false for i={i}");
        }
    }

    [Fact]
    public void Equals_Should_Compare_Exactly()
    {
        var genNotEqualMat = (int i) =>
        {
            var arr = new float[12];
            for (int j = 0; j < 12; j++)
            {
                if (j == 3 || j == 7 || j == 11) continue;
                arr[j] = j + (j == i ? 0 : 1.0001f);
            }
            arr[3] = arr[7] = arr[11] = 0;
            return Mat3.Set(arr[0], arr[1], arr[2], arr[4], arr[5], arr[6], arr[8], arr[9], arr[10]);
        };

        for (int i = 0; i < 9; i++)
        {
            int dataIndex = i + (i / 3);
            var matA = genNotEqualMat(dataIndex);
            var matB = genNotEqualMat(dataIndex);
            Assert.True(Mat3.Equals(matA, matB), $"Equals should be true for i={i} (same matrix)");

            var matC = genNotEqualMat(-1);
            Assert.False(Mat3.Equals(matC, matA), $"Equals should be false for i={i} (different matrices)");
        }
    }

    [Fact]
    public void Set_Should_Assign_Values()
    {
        var expected = Mat3.Create(2, 3, 4, 22, 33, 44, 222, 333, 444);

        var resultWithDest = Mat3.Set(2, 3, 4, 22, 33, 44, 222, 333, 444, Mat3.Create());
        Assert.True(Mat3EqualsExact(resultWithDest, expected));

        var resultWithoutDest = Mat3.Set(2, 3, 4, 22, 33, 44, 222, 333, 444);
        Assert.True(Mat3EqualsExact(resultWithoutDest, expected));
    }

    [Fact]
    public void Identity_Should_Create_Identity_Matrix()
    {
        var expected = Mat3.Create(1, 0, 0, 0, 1, 0, 0, 0, 1);

        var resultWithDest = Mat3.Identity(Mat3.Create());
        Assert.True(Mat3EqualsExact(resultWithDest, expected));

        var resultWithoutDest = Mat3.Identity();
        Assert.True(Mat3EqualsExact(resultWithoutDest, expected));
    }

    [Fact]
    public void Transpose_Should_Transpose_Matrix()
    {
        var m = CreateTestMatrix();
        // Original m:
        // [ 0,  1,  2]
        // [ 4,  5,  6]
        // [ 8,  9, 10]
        // Transposed:
        // [ 0,  4,  8]
        // [ 1,  5,  9]
        // [ 2,  6, 10]
        var expected = Mat3.Create(0, 4, 8, 1, 5, 9, 2, 6, 10);

        var resultWithDest = Mat3.Transpose(m, Mat3.Create());
        Assert.True(Mat3EqualsExact(resultWithDest, expected));

        var resultWithoutDest = Mat3.Transpose(m);
        Assert.True(Mat3EqualsExact(resultWithoutDest, expected));
    }

    [Fact]
    public void Multiply_Should_Multiply_Matrices()
    {
        var m1 = CreateTestMatrix(); // [0,1,2; 4,5,6; 8,9,10]
        var m2 = Mat3.Create(4, 5, 6, 1, 2, 3, 9, 10, 11);
        // Expected result calculated manually or using reference impl
        var expected = Mat3.Create(
            22, 28, 34, // Row 1: 0*4+1*1+2*9, 0*5+1*2+2*10, 0*6+1*3+2*11
            74, 88, 102, // Row 2: 4*4+5*1+6*9, 4*5+5*2+6*10, 4*6+5*3+6*11
            126, 148, 170 // Row 3: 8*4+9*1+10*9, 8*5+9*2+10*10, 8*6+9*3+10*11
        );

        var resultWithDest = Mat3.Multiply(m1, m2, Mat3.Create());
        Assert.True(Mat3EqualsApprox(resultWithDest, expected)); // Use approx for potential float errors

        var resultWithoutDest = Mat3.Multiply(m1, m2);
        Assert.True(Mat3EqualsApprox(resultWithoutDest, expected));

        // Test alias
        var resultMulWithDest = Mat3.Mul(m1, m2, Mat3.Create());
        Assert.True(Mat3EqualsApprox(resultMulWithDest, expected));
        var resultMulWithoutDest = Mat3.Mul(m1, m2);
        Assert.True(Mat3EqualsApprox(resultMulWithoutDest, expected));
    }


    [Fact]
    public void Inverse_Should_Compute_Matrix_Inverse()
    {
        var testCases = new[]
        {
            new {
                M = Mat3.Create(2, 1, 3, 1, 2, 1, 3, 1, 2),
                Expected = Mat3.Create(-0.375f, -0.125f, 0.625f, -0.125f, 0.625f, -0.125f, 0.625f, -0.125f, -0.375f)
            },
            new {
                M = Mat3.Create(1, 0, 0, 0, 1, 0, 2, 3, 4),
                Expected = Mat3.Create(1, 0, 0, 0, 1, 0, -0.5f, -0.75f, 0.25f)
            }
            // Add more test cases as needed
        };

        foreach (var testCase in testCases)
        {
            var resultWithDest = Mat3.Inverse(testCase.M, Mat3.Create());
            Assert.True(Mat3EqualsApprox(resultWithDest, testCase.Expected, 1e-5f)); // Slightly higher tolerance

            var resultWithoutDest = Mat3.Inverse(testCase.M);
            Assert.True(Mat3EqualsApprox(resultWithoutDest, testCase.Expected, 1e-5f));

            // Test alias
            var resultInvertWithDest = Mat3.Invert(testCase.M, Mat3.Create());
            Assert.True(Mat3EqualsApprox(resultInvertWithDest, testCase.Expected, 1e-5f));
            var resultInvertWithoutDest = Mat3.Invert(testCase.M);
            Assert.True(Mat3EqualsApprox(resultInvertWithoutDest, testCase.Expected, 1e-5f));
        }
    }

    [Fact]
    public void Determinant_Should_Compute_Correctly()
    {
        var m1 = Mat3.Create(2, 1, 3, 1, 2, 1, 3, 1, 2);
        // Manual calc: 2*(2*2 - 1*1) - 1*(1*2 - 1*3) + 3*(1*1 - 2*3)
        // = 2*(4-1) - 1*(2-3) + 3*(1-6) = 2*3 - 1*(-1) + 3*(-5) = 6 + 1 - 15 = -8
        var expectedDet1 = -8.0f;
        Assert.Equal(expectedDet1, Mat3.Determinant(m1), 5); // Precision check

        var m2 = Mat3.Create(2, 0, 0, 0, 3, 0, 0, 0, 4);
        // Diagonal: 2 * 3 * 4 = 24
        var expectedDet2 = 24.0f;
        Assert.Equal(expectedDet2, Mat3.Determinant(m2), 5);
    }

    [Fact]
    public void Set_Translation_Get_Translation_Should_Work()
    {
        var m = CreateTestMatrix();
        var translation = Vec2.Create(11, 22);
        // Expected: m with last column [11, 22, 1]
        var expectedSet = Mat3.Create(
            0, 1, 2,
            4, 5, 6,
            11, 22, 1 // Modified last column
        );

        var resultSetWithDest = Mat3.SetTranslation(m, translation, Mat3.Create());
        Assert.True(Mat3EqualsExact(resultSetWithDest, expectedSet));

        var resultSetWithoutDest = Mat3.SetTranslation(m, translation);
        Assert.True(Mat3EqualsExact(resultSetWithoutDest, expectedSet));

        // Get translation
        var expectedGet = Vec2.Create(8, 9); // Original m's translation
        var resultGetWithDest = Mat3.GetTranslation(m, Vec2.Create());
        Assert.True(Vec2.Equals(expectedGet, resultGetWithDest)); // Assuming Vec2.Equals exists

        var resultGetWithoutDest = Mat3.GetTranslation(m);
        Assert.True(Vec2.Equals(expectedGet, resultGetWithoutDest));
    }

    [Fact]
    public void Get_Axis_Set_Axis_Should_Work()
    {
        var m = CreateTestMatrix();
        var newAxis = Vec2.Create(11, 22);

        // Test GetAxis (X-axis = column 0)
        var expectedGetX = Vec2.Create(0, 1); // Column 0 of m
        var resultGetXWithDest = Mat3.GetAxis(m, 0, Vec2.Create());
        Assert.True(Vec2.Equals(expectedGetX, resultGetXWithDest));
        var resultGetWithoutDest = Mat3.GetAxis(m, 0);
        Assert.True(Vec2.Equals(expectedGetX, resultGetWithoutDest));

        // Test GetAxis (Y-axis = column 1)
        var expectedGetY = Vec2.Create(4, 5); // Column 1 of m
        var resultGetYWithDest = Mat3.GetAxis(m, 1, Vec2.Create());
        Assert.True(Vec2.Equals(expectedGetY, resultGetYWithDest));
        var resultGetYWithoutDest = Mat3.GetAxis(m, 1);
        Assert.True(Vec2.Equals(expectedGetY, resultGetYWithoutDest));

        // Test SetAxis (X-axis)
        var expectedSetX = Mat3.Create(
            11, 1, 2, // Column 0 changed
             4, 5, 6,
             8, 9, 10
        );
        var resultSetXWithDest = Mat3.SetAxis(m, newAxis, 0, Mat3.Create());
        Assert.True(Mat3EqualsExact(resultSetXWithDest, expectedSetX));
        var resultSetXWithoutDest = Mat3.SetAxis(m, newAxis, 0);
        Assert.True(Mat3EqualsExact(resultSetXWithoutDest, expectedSetX));

        // Test SetAxis (Y-axis)
        var expectedSetY = Mat3.Create(
            0, 1, 2,
           11, 22, 6, // Column 1 changed
            8, 9, 10
        );
        var resultSetYWithDest = Mat3.SetAxis(m, newAxis, 1, Mat3.Create());
        Assert.True(Mat3EqualsExact(resultSetYWithDest, expectedSetY));
        var resultSetYWithoutDest = Mat3.SetAxis(m, newAxis, 1);
        Assert.True(Mat3EqualsExact(resultSetYWithoutDest, expectedSetY));
    }

    [Fact]
    public void Get_Scaling_Get_3D_Scaling_Should_Work()
    {
        // GetScaling (2D)
        var m2d = Mat3.Create(2, 8, 3, 5, 6, 7, 9, 10, 11);
        var expected2d = Vec2.Create(MathF.Sqrt(2 * 2 + 8 * 8), MathF.Sqrt(5 * 5 + 6 * 6));
        var result2dWithDest = Mat3.GetScaling(m2d, Vec2.Create());
        Assert.True(Vec2.EqualsApproximately(expected2d, result2dWithDest)); // Assuming Vec2.EqualsApproximately
        var result2dWithoutDest = Mat3.GetScaling(m2d);
        Assert.True(Vec2.EqualsApproximately(expected2d, result2dWithoutDest));

        // Get3DScaling (Note: Mat3 is 3x3, so this uses all 3 columns)
        // Using the same matrix m2d for 3D scaling test
        var expected3d = Vec3.Create(
            MathF.Sqrt(2 * 2 + 8 * 8 + 3 * 3),
            MathF.Sqrt(5 * 5 + 6 * 6 + 7 * 7),
            MathF.Sqrt(9 * 9 + 10 * 10 + 11 * 11)
        );
        var result3dWithDest = Mat3.Get3DScaling(m2d, Vec3.Create());
        Assert.True(Vec3.EqualsApproximately(expected3d, result3dWithDest)); // Assuming Vec3.EqualsApproximately
        var result3dWithoutDest = Mat3.Get3DScaling(m2d);
        Assert.True(Vec3.EqualsApproximately(expected3d, result3dWithoutDest));
    }

    [Fact]
    public void Translation_Translate_Should_Work()
    {
        var translation = Vec2.Create(2, 3);
        var expectedTranslation = Mat3.Create(1, 0, 0, 0, 1, 0, 2, 3, 1);

        var resultTransWithDest = Mat3.Translation(translation, Mat3.Create());
        Assert.True(Mat3EqualsExact(resultTransWithDest, expectedTranslation));
        var resultTransWithoutDest = Mat3.Translation(translation);
        Assert.True(Mat3EqualsExact(resultTransWithoutDest, expectedTranslation));

        var m = CreateTestMatrix();
        // Manual calculation for translate result:
        // [m00 m01 m02]   [1 0 0]   [m00 m01 m02]
        // [m10 m11 m12] * [0 1 0] = [m10 m11 m12]
        // [m20 m21 m22]   [tx ty 1] [m20 + m00*tx + m10*ty, m21 + m01*tx + m11*ty, m22 + m02*tx + m12*ty]
        // Result col 3: [8 + 0*2 + 4*3, 9 + 1*2 + 5*3, 10 + 2*2 + 6*3] = [8+0+12, 9+2+15, 10+4+18] = [20, 26, 32]
        var expectedTranslate = Mat3.Create(0, 1, 2, 4, 5, 6, 20, 26, 32);

        var resultTranslateWithDest = Mat3.Translate(m, translation, Mat3.Create());
        Assert.True(Mat3EqualsExact(resultTranslateWithDest, expectedTranslate));
        var resultTranslateWithoutDest = Mat3.Translate(m, translation);
        Assert.True(Mat3EqualsExact(resultTranslateWithoutDest, expectedTranslate));
    }

    [Fact]
    public void Rotation_Rotate_Should_Work()
    {
        var angle = 1.23f;
        var c = MathF.Cos(angle);
        var s = MathF.Sin(angle);
        var expectedRotation = Mat3.Create(c, s, 0, -s, c, 0, 0, 0, 1);

        var resultRotWithDest = Mat3.Rotation(angle, Mat3.Create());
        Assert.True(Mat3EqualsApprox(resultRotWithDest, expectedRotation));
        var resultRotWithoutDest = Mat3.Rotation(angle);
        Assert.True(Mat3EqualsApprox(resultRotWithoutDest, expectedRotation));

        var m = CreateTestMatrix();
        // Result = m * RotationMatrix
        var expectedRotate = Mat3.Multiply(m, expectedRotation); // Use reference multiply

        var resultRotateWithDest = Mat3.Rotate(m, angle, Mat3.Create());
        Assert.True(Mat3EqualsApprox(resultRotateWithDest, expectedRotate));
        var resultRotateWithoutDest = Mat3.Rotate(m, angle);
        Assert.True(Mat3EqualsApprox(resultRotateWithoutDest, expectedRotate));
    }

    [Fact]
    public void Rotation_X_Rotate_X_Should_Work()
    {
        var angle = 1.23f;
        var c = MathF.Cos(angle);
        var s = MathF.Sin(angle);
        var expectedRotationX = Mat3.Create(1, 0, 0, 0, c, s, 0, -s, c);

        var resultRotXWithDest = Mat3.RotationX(angle, Mat3.Create());
        Assert.True(Mat3EqualsApprox(resultRotXWithDest, expectedRotationX));
        var resultRotXWithoutDest = Mat3.RotationX(angle);
        Assert.True(Mat3EqualsApprox(resultRotXWithoutDest, expectedRotationX));

        var m = CreateTestMatrix();
        var expectedRotateX = Mat3.Multiply(m, expectedRotationX);

        var resultRotateXWithDest = Mat3.RotateX(m, angle, Mat3.Create());
        Assert.True(Mat3EqualsApprox(resultRotateXWithDest, expectedRotateX));
        var resultRotateXWithoutDest = Mat3.RotateX(m, angle);
        Assert.True(Mat3EqualsApprox(resultRotateXWithoutDest, expectedRotateX));
    }

    [Fact]
    public void Rotation_Y_Rotate_Y_Should_Work()
    {
        var angle = 1.23f;
        var c = MathF.Cos(angle);
        var s = MathF.Sin(angle);
        var expectedRotationY = Mat3.Create(c, 0, -s, 0, 1, 0, s, 0, c);

        var resultRotYWithDest = Mat3.RotationY(angle, Mat3.Create());
        Assert.True(Mat3EqualsApprox(resultRotYWithDest, expectedRotationY));
        var resultRotYWithoutDest = Mat3.RotationY(angle);
        Assert.True(Mat3EqualsApprox(resultRotYWithoutDest, expectedRotationY));

        var m = CreateTestMatrix();
        var expectedRotateY = Mat3.Multiply(m, expectedRotationY);

        var resultRotateYWithDest = Mat3.RotateY(m, angle, Mat3.Create());
        Assert.True(Mat3EqualsApprox(resultRotateYWithDest, expectedRotateY));
        var resultRotateYWithoutDest = Mat3.RotateY(m, angle);
        Assert.True(Mat3EqualsApprox(resultRotateYWithoutDest, expectedRotateY));
    }

    [Fact]
    public void Rotation_Z_RotateZ_Should_Work()
    {
        var angle = 1.23f;
        var c = MathF.Cos(angle);
        var s = MathF.Sin(angle);
        var expectedRotationZ = Mat3.Create(c, s, 0, -s, c, 0, 0, 0, 1);

        var resultRotZWithDest = Mat3.RotationZ(angle, Mat3.Create());
        Assert.True(Mat3EqualsApprox(resultRotZWithDest, expectedRotationZ));
        var resultRotZWithoutDest = Mat3.RotationZ(angle);
        Assert.True(Mat3EqualsApprox(resultRotZWithoutDest, expectedRotationZ));

        // Test alias
        var resultRotZAliasWithDest = Mat3.Rotation(angle, Mat3.Create());
        Assert.True(Mat3EqualsApprox(resultRotZAliasWithDest, expectedRotationZ));
        var resultRotZAliasWithoutDest = Mat3.Rotation(angle);
        Assert.True(Mat3EqualsApprox(resultRotZAliasWithoutDest, expectedRotationZ));

        var m = CreateTestMatrix();
        var expectedRotateZ = Mat3.Multiply(m, expectedRotationZ);

        var resultRotateZWithDest = Mat3.RotateZ(m, angle, Mat3.Create());
        Assert.True(Mat3EqualsApprox(resultRotateZWithDest, expectedRotateZ));
        var resultRotateZWithoutDest = Mat3.RotateZ(m, angle);
        Assert.True(Mat3EqualsApprox(resultRotateZWithoutDest, expectedRotateZ));

        // Test alias
        var resultRotateZAliasWithDest = Mat3.Rotate(m, angle, Mat3.Create());
        Assert.True(Mat3EqualsApprox(resultRotateZAliasWithDest, expectedRotateZ));
        var resultRotateZAliasWithoutDest = Mat3.Rotate(m, angle);
        Assert.True(Mat3EqualsApprox(resultRotateZAliasWithoutDest, expectedRotateZ));
    }

    [Fact]
    public void Scaling_Scale_Should_Work()
    {
        var scale = Vec2.Create(2, 3);
        var expectedScaling = Mat3.Create(2, 0, 0, 0, 3, 0, 0, 0, 1);

        var resultScaleWithDest = Mat3.Scaling(scale, Mat3.Create());
        Assert.True(Mat3EqualsExact(resultScaleWithDest, expectedScaling));
        var resultScaleWithoutDest = Mat3.Scaling(scale);
        Assert.True(Mat3EqualsExact(resultScaleWithoutDest, expectedScaling));

        var m = CreateTestMatrix();
        // Result col 1 = m col 1 * sx, col 2 = m col 2 * sy
        // [0*2, 1*2, 2*2] = [0, 2, 4]
        // [4*3, 5*3, 6*3] = [12, 15, 18]
        // [8, 9, 10] (unchanged)
        var expectedScale = Mat3.Create(0, 2, 4, 12, 15, 18, 8, 9, 10);

        var resultScaleMatWithDest = Mat3.Scale(m, scale, Mat3.Create());
        Assert.True(Mat3EqualsExact(resultScaleMatWithDest, expectedScale));
        var resultScaleMatWithoutDest = Mat3.Scale(m, scale);
        Assert.True(Mat3EqualsExact(resultScaleMatWithoutDest, expectedScale));
    }

    [Fact]
    public void Scaling_3D_Scale3D_Should_Work()
    {
        var scale3d = Vec3.Create(2, 3, 4);
        var expectedScaling3D = Mat3.Create(2, 0, 0, 0, 3, 0, 0, 0, 4);

        var resultScale3DWithDest = Mat3.Scaling3D(scale3d, Mat3.Create());
        Assert.True(Mat3EqualsExact(resultScale3DWithDest, expectedScaling3D));
        var resultScale3DWithoutDest = Mat3.Scaling3D(scale3d);
        Assert.True(Mat3EqualsExact(resultScale3DWithoutDest, expectedScaling3D));

        var m = CreateTestMatrix();
        // Result col 1 *= sx, col 2 *= sy, col 3 *= sz
        // [0*2, 1*2, 2*2] = [0, 2, 4]
        // [4*3, 5*3, 6*3] = [12, 15, 18]
        // [8*4, 9*4, 10*4] = [32, 36, 40]
        var expectedScale3D = Mat3.Create(0, 2, 4, 12, 15, 18, 32, 36, 40);

        var resultScale3DMatWithDest = Mat3.Scale3D(m, scale3d, Mat3.Create());
        Assert.True(Mat3EqualsExact(resultScale3DMatWithDest, expectedScale3D));
        var resultScale3DMatWithoutDest = Mat3.Scale3D(m, scale3d);
        Assert.True(Mat3EqualsExact(resultScale3DMatWithoutDest, expectedScale3D));
    }

    [Fact]
    public void Uniform_Scaling_Uniform_Scale_Should_Work()
    {
        var scale = 2.0f;
        var expectedUniformScaling = Mat3.Create(2, 0, 0, 0, 2, 0, 0, 0, 1);

        var resultUScaleWithDest = Mat3.UniformScaling(scale, Mat3.Create());
        Assert.True(Mat3EqualsExact(resultUScaleWithDest, expectedUniformScaling));
        var resultUScaleWithoutDest = Mat3.UniformScaling(scale);
        Assert.True(Mat3EqualsExact(resultUScaleWithoutDest, expectedUniformScaling));

        var m = CreateTestMatrix();
        // All columns * scale (except last col which is translation-like)
        // [0*2, 1*2, 2*2] = [0, 2, 4]
        // [4*2, 5*2, 6*2] = [8, 10, 12]
        // [8, 9, 10] (unchanged)
        var expectedUScale = Mat3.Create(0, 2, 4, 8, 10, 12, 8, 9, 10);

        var resultUScaleMatWithDest = Mat3.UniformScale(m, scale, Mat3.Create());
        Assert.True(Mat3EqualsExact(resultUScaleMatWithDest, expectedUScale));
        var resultUScaleMatWithoutDest = Mat3.UniformScale(m, scale);
        Assert.True(Mat3EqualsExact(resultUScaleMatWithoutDest, expectedUScale));
    }

    [Fact]
    public void Uniform_Scaling_3D_Uniform_Scale_3D_Should_Work()
    {
        var scale = 2.0f;
        var expectedUniformScaling3D = Mat3.Create(2, 0, 0, 0, 2, 0, 0, 0, 2);

        var resultUScale3DWithDest = Mat3.UniformScaling3D(scale, Mat3.Create());
        Assert.True(Mat3EqualsExact(resultUScale3DWithDest, expectedUniformScaling3D));
        var resultUScale3DWithoutDest = Mat3.UniformScaling3D(scale);
        Assert.True(Mat3EqualsExact(resultUScale3DWithoutDest, expectedUniformScaling3D));

        var m = CreateTestMatrix();
        // All columns * scale
        // [0*2, 1*2, 2*2] = [0, 2, 4]
        // [4*2, 5*2, 6*2] = [8, 10, 12]
        // [8*2, 9*2, 10*2] = [16, 18, 20]
        var expectedUScale3D = Mat3.Create(0, 2, 4, 8, 10, 12, 16, 18, 20);

        var resultUScale3DMatWithDest = Mat3.UniformScale3D(m, scale, Mat3.Create());
        Assert.True(Mat3EqualsExact(resultUScale3DMatWithDest, expectedUScale3D));
        var resultUScale3DMatWithoutDest = Mat3.UniformScale3D(m, scale);
        Assert.True(Mat3EqualsExact(resultUScale3DMatWithoutDest, expectedUScale3D));
    }

    [Fact]
    public void From_Mat4_Should_Extract_Upper_Left()
    {
        var m4 = Mat4.Create(
            1, 2, 3, 4,
            5, 6, 7, 8,
            9, 10, 11, 12,
            13, 14, 15, 16
        );
        var expected = Mat3.Create(1, 2, 3, 5, 6, 7, 9, 10, 11);

        var resultWithDest = Mat3.FromMat4(m4, Mat3.Create());
        Assert.True(Mat3EqualsExact(resultWithDest, expected));

        var resultWithoutDest = Mat3.FromMat4(m4);
        Assert.True(Mat3EqualsExact(resultWithoutDest, expected));
    }

}
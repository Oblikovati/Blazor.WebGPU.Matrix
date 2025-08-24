using SpawnDev.Blazor.UnitTesting;

namespace Blazor.WebGPU.Matrix.Tests.Float;

[TestClass]
public class Mat4FloatTests
{
    private static Mat4 CreateTestMatrix()
    {
        // Represents a 4x4 matrix:
        // [ 0,  1,  2,  3]
        // [ 4,  5,  6,  7]
        // [ 8,  9, 10, 11]
        // [12, 13, 14, 15]
        return Mat4.Create(
            0, 1, 2, 3,
            4, 5, 6, 7,
            8, 9, 10, 11,
            12, 13, 14, 15
        );
    }

    private static bool Mat4EqualsApprox(Mat4 a, Mat4 b, float epsilon = 1e-6f)
    {
        // Compare all 16 elements
        for (int i = 0; i < 16; i++)
        {
            if (MathF.Abs(a[i] - b[i]) >= epsilon)
                return false;
        }
        return true;
    }

    private static bool Mat4EqualsExact(Mat4 a, Mat4 b)
    {
        // Compare all 16 elements exactly
        for (int i = 0; i < 16; i++)
        {
            if (a[i] != b[i]) // Use exact equality for float
                return false;
        }
        return true;
    }

    [TestMethod]
    public void Create_Should_Initialize_Correctly()
    {
        var tests = new[]
        {
            new { Args = new float[0], Expected = new float[] {1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1} },
            new { Args = new float[] {1}, Expected = new float[] {1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1} },
            new { Args = new float[] {1, 2}, Expected = new float[] {1, 2, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1} },
            new { Args = new float[] {1, 2, 3}, Expected = new float[] {1, 2, 3, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1} },
            new { Args = new float[] {1, 2, 3, 4}, Expected = new float[] {1, 2, 3, 4, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1} },
            new { Args = new float[] {1, 2, 3, 4, 5}, Expected = new float[] {1, 2, 3, 4, 5, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1} },
            new { Args = new float[] {1, 2, 3, 4, 5, 6}, Expected = new float[] {1, 2, 3, 4, 5, 6, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1} },
            new { Args = new float[] {1, 2, 3, 4, 5, 6, 7}, Expected = new float[] {1, 2, 3, 4, 5, 6, 7, 0, 0, 0, 1, 0, 0, 0, 0, 1} },
            new { Args = new float[] {1, 2, 3, 4, 5, 6, 7, 8}, Expected = new float[] {1, 2, 3, 4, 5, 6, 7, 8, 0, 0, 1, 0, 0, 0, 0, 1} },
            new { Args = new float[] {1, 2, 3, 4, 5, 6, 7, 8, 9}, Expected = new float[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 0, 0, 0, 0, 1} },
            new { Args = new float[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10}, Expected = new float[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 0, 0, 0, 0, 0, 1} },
            new { Args = new float[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11}, Expected = new float[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 0, 0, 0, 0, 1} },
            new { Args = new float[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12}, Expected = new float[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 0, 0, 0, 1} },
            new { Args = new float[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13}, Expected = new float[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 0, 0, 1} },
            new { Args = new float[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14}, Expected = new float[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 0, 1} },
            new { Args = new float[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15}, Expected = new float[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 1} },
        };

        foreach (var test in tests)
        {
            Mat4 m;
            switch (test.Args.Length)
            {
                case 0: m = Mat4.Create(); break;
                case 1: m = Mat4.Create(test.Args[0]); break;
                case 2: m = Mat4.Create(test.Args[0], test.Args[1]); break;
                case 3: m = Mat4.Create(test.Args[0], test.Args[1], test.Args[2]); break;
                case 4: m = Mat4.Create(test.Args[0], test.Args[1], test.Args[2], test.Args[3]); break;
                case 5: m = Mat4.Create(test.Args[0], test.Args[1], test.Args[2], test.Args[3], test.Args[4]); break;
                case 6: m = Mat4.Create(test.Args[0], test.Args[1], test.Args[2], test.Args[3], test.Args[4], test.Args[5]); break;
                case 7: m = Mat4.Create(test.Args[0], test.Args[1], test.Args[2], test.Args[3], test.Args[4], test.Args[5], test.Args[6]); break;
                case 8: m = Mat4.Create(test.Args[0], test.Args[1], test.Args[2], test.Args[3], test.Args[4], test.Args[5], test.Args[6], test.Args[7]); break;
                case 9: m = Mat4.Create(test.Args[0], test.Args[1], test.Args[2], test.Args[3], test.Args[4], test.Args[5], test.Args[6], test.Args[7], test.Args[8]); break;
                case 10: m = Mat4.Create(test.Args[0], test.Args[1], test.Args[2], test.Args[3], test.Args[4], test.Args[5], test.Args[6], test.Args[7], test.Args[8], test.Args[9]); break;
                case 11: m = Mat4.Create(test.Args[0], test.Args[1], test.Args[2], test.Args[3], test.Args[4], test.Args[5], test.Args[6], test.Args[7], test.Args[8], test.Args[9], test.Args[10]); break;
                case 12: m = Mat4.Create(test.Args[0], test.Args[1], test.Args[2], test.Args[3], test.Args[4], test.Args[5], test.Args[6], test.Args[7], test.Args[8], test.Args[9], test.Args[10], test.Args[11]); break;
                case 13: m = Mat4.Create(test.Args[0], test.Args[1], test.Args[2], test.Args[3], test.Args[4], test.Args[5], test.Args[6], test.Args[7], test.Args[8], test.Args[9], test.Args[10], test.Args[11], test.Args[12]); break;
                case 14: m = Mat4.Create(test.Args[0], test.Args[1], test.Args[2], test.Args[3], test.Args[4], test.Args[5], test.Args[6], test.Args[7], test.Args[8], test.Args[9], test.Args[10], test.Args[11], test.Args[12], test.Args[13]); break;
                case 15: m = Mat4.Create(test.Args[0], test.Args[1], test.Args[2], test.Args[3], test.Args[4], test.Args[5], test.Args[6], test.Args[7], test.Args[8], test.Args[9], test.Args[10], test.Args[11], test.Args[12], test.Args[13], test.Args[14]); break;
                default: throw new InvalidOperationException("Unexpected number of args");
            }

            var expected = Mat4.Set(
                test.Expected[0], test.Expected[1], test.Expected[2], test.Expected[3],
                test.Expected[4], test.Expected[5], test.Expected[6], test.Expected[7],
                test.Expected[8], test.Expected[9], test.Expected[10], test.Expected[11],
                test.Expected[12], test.Expected[13], test.Expected[14], test.Expected[15]
            );

            Assert.True(Mat4EqualsExact(m, expected), $"Create failed for args: [{string.Join(", ", test.Args)}]");
        }
    }

    [TestMethod]
    public void Negate_Should_Negate_Elements()
    {
        var m = CreateTestMatrix();
        var expected = Mat4.Create(
            -0, -1, -2, -3,
            -4, -5, -6, -7,
            -8, -9, -10, -11,
            -12, -13, -14, -15
        );

        var resultWithDest = Mat4.Negate(m, Mat4.Create());
        Assert.True(Mat4EqualsExact(resultWithDest, expected));

        var resultWithoutDest = Mat4.Negate(m);
        Assert.True(Mat4EqualsExact(resultWithoutDest, expected));
    }

    [TestMethod]
    public void Add_Should_Add_Matrices()
    {
        var m = CreateTestMatrix();
        var expected = Mat4.Create(
             0, 2, 4, 6,
             8, 10, 12, 14,
            16, 18, 20, 22,
            24, 26, 28, 30
        );

        var resultWithDest = Mat4.Add(m, m, Mat4.Create());
        Assert.True(Mat4EqualsExact(resultWithDest, expected));

        var resultWithoutDest = Mat4.Add(m, m);
        Assert.True(Mat4EqualsExact(resultWithoutDest, expected));
    }

    [TestMethod]
    public void Multiply_Scalar_Should_Scale_Matrix()
    {
        var m = CreateTestMatrix();
        var scalar = 2.0f;
        var expected = Mat4.Create(
             0, 2, 4, 6,
             8, 10, 12, 14,
            16, 18, 20, 22,
            24, 26, 28, 30
        );

        var resultWithDest = Mat4.MultiplyScalar(m, scalar, Mat4.Create());
        Assert.True(Mat4EqualsExact(resultWithDest, expected));

        var resultWithoutDest = Mat4.MultiplyScalar(m, scalar);
        Assert.True(Mat4EqualsExact(resultWithoutDest, expected));

        // Test alias
        var resultMulScalarWithDest = Mat4.MulScalar(m, scalar, Mat4.Create());
        Assert.True(Mat4EqualsExact(resultMulScalarWithDest, expected));
        var resultMulScalarWithoutDest = Mat4.MulScalar(m, scalar);
        Assert.True(Mat4EqualsExact(resultMulScalarWithoutDest, expected));
    }

    [TestMethod]
    public void Copy_And_Clone_Should_Duplicate_Matrix()
    {
        var m = CreateTestMatrix();

        var copyWithDest = Mat4.Copy(m, Mat4.Create());
        Assert.True(Mat4EqualsExact(copyWithDest, m));
        Assert.NotSame(m, copyWithDest);

        var copyWithoutDest = Mat4.Copy(m);
        Assert.True(Mat4EqualsExact(copyWithoutDest, m));
        Assert.NotSame(m, copyWithoutDest);

        // Test alias
        var cloneWithDest = Mat4.Clone(m, Mat4.Create());
        Assert.True(Mat4EqualsExact(cloneWithDest, m));
        Assert.NotSame(m, cloneWithDest);

        var cloneWithoutDest = Mat4.Clone(m);
        Assert.True(Mat4EqualsExact(cloneWithoutDest, m));
        Assert.NotSame(m, cloneWithoutDest);
    }

    [TestMethod]
    public void Equals_Approximately_Should_Compare_With_Tolerance()
    {
        var epsilon = 1e-6f;
        var genAlmostEqualMat = (int i) =>
        {
            var arr = new float[16];
            for (int j = 0; j < 16; j++)
            {
                arr[j] = j + (j == i ? 0 : epsilon * 0.5f);
            }
            return Mat4.Set(
                arr[0], arr[1], arr[2], arr[3],
                arr[4], arr[5], arr[6], arr[7],
                arr[8], arr[9], arr[10], arr[11],
                arr[12], arr[13], arr[14], arr[15]
            );
        };

        var genNotAlmostEqualMat = (int i) =>
        {
            var arr = new float[16];
            for (int j = 0; j < 16; j++)
            {
                arr[j] = j + (j == i ? 0 : 1.0001f);
            }
            return Mat4.Set(
                arr[0], arr[1], arr[2], arr[3],
                arr[4], arr[5], arr[6], arr[7],
                arr[8], arr[9], arr[10], arr[11],
                arr[12], arr[13], arr[14], arr[15]
            );
        };

        for (int i = 0; i < 16; i++)
        {
            var almostEqualA = genAlmostEqualMat(-1);
            var almostEqualB = genAlmostEqualMat(i);
            Assert.True(Mat4.EqualsApproximately(almostEqualA, almostEqualB), $"EqualsApproximately should be true for i={i}");

            var notAlmostEqualA = genNotAlmostEqualMat(-1);
            var notAlmostEqualB = genNotAlmostEqualMat(i);
            Assert.False(Mat4.EqualsApproximately(notAlmostEqualA, notAlmostEqualB), $"EqualsApproximately should be false for i={i}");
        }
    }

    [TestMethod]
    public void Equals_Should_Compare_Exactly()
    {
        var genNotEqualMat = (int i) =>
        {
            var arr = new float[16];
            for (int j = 0; j < 16; j++)
            {
                arr[j] = j + (j == i ? 0 : 1.0001f);
            }
            return Mat4.Set(
                arr[0], arr[1], arr[2], arr[3],
                arr[4], arr[5], arr[6], arr[7],
                arr[8], arr[9], arr[10], arr[11],
                arr[12], arr[13], arr[14], arr[15]
            );
        };

        for (int i = 0; i < 16; i++)
        {
            var matA = genNotEqualMat(i);
            var matB = genNotEqualMat(i);
            Assert.True(Mat4.Equals(matA, matB), $"Equals should be true for i={i} (same matrix)");

            var matC = genNotEqualMat(-1);
            Assert.False(Mat4.Equals(matC, matA), $"Equals should be false for i={i} (different matrices)");
        }
    }

    [TestMethod]
    public void Set_Should_Assign_Values()
    {
        var expected = Mat4.Create(2, 3, 4, 5, 22, 33, 44, 55, 222, 333, 444, 555, 2222, 3333, 4444, 5555);

        var resultWithDest = Mat4.Set(2, 3, 4, 5, 22, 33, 44, 55, 222, 333, 444, 555, 2222, 3333, 4444, 5555, Mat4.Create());
        Assert.True(Mat4EqualsExact(resultWithDest, expected));

        var resultWithoutDest = Mat4.Set(2, 3, 4, 5, 22, 33, 44, 55, 222, 333, 444, 555, 2222, 3333, 4444, 5555);
        Assert.True(Mat4EqualsExact(resultWithoutDest, expected));
    }

    [TestMethod]
    public void Identity_Should_Create_Identity_Matrix()
    {
        var expected = Mat4.Create(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1);

        var resultWithDest = Mat4.Identity(Mat4.Create());
        Assert.True(Mat4EqualsExact(resultWithDest, expected));

        var resultWithoutDest = Mat4.Identity();
        Assert.True(Mat4EqualsExact(resultWithoutDest, expected));
    }

    [TestMethod]
    public void Transpose_Should_Transpose_Matrix()
    {
        var m = CreateTestMatrix();
        // Original m:
        // [ 0,  1,  2,  3]
        // [ 4,  5,  6,  7]
        // [ 8,  9, 10, 11]
        // [12, 13, 14, 15]
        // Transposed:
        // [ 0,  4,  8, 12]
        // [ 1,  5,  9, 13]
        // [ 2,  6, 10, 14]
        // [ 3,  7, 11, 15]
        var expected = Mat4.Create(0, 4, 8, 12, 1, 5, 9, 13, 2, 6, 10, 14, 3, 7, 11, 15);

        var resultWithDest = Mat4.Transpose(m, Mat4.Create());
        Assert.True(Mat4EqualsExact(resultWithDest, expected));

        var resultWithoutDest = Mat4.Transpose(m);
        Assert.True(Mat4EqualsExact(resultWithoutDest, expected));
    }

    [TestMethod]
    public void Multiply_Should_Multiply_Matrices()
    {
        var m1 = CreateTestMatrix(); // [0,1,2,3; 4,5,6,7; 8,9,10,11; 12,13,14,15]
        var m2 = Mat4.Create(4, 5, 6, 7, 1, 2, 3, 4, 9, 10, 11, 12, 13, 14, 15, 16);
        // Expected result calculated manually or using reference impl
        // Row 1: [0*4+1*1+2*9+3*13, 0*5+1*2+2*10+3*14, 0*6+1*3+2*11+3*15, 0*7+1*4+2*12+3*16]
        //        = [0+1+18+39, 0+2+20+42, 0+3+22+45, 0+4+24+48] = [58, 64, 70, 76]
        // Row 2: [4*4+5*1+6*9+7*13, 4*5+5*2+6*10+7*14, 4*6+5*3+6*11+7*15, 4*7+5*4+6*12+7*16]
        //        = [16+5+54+91, 20+10+60+98, 24+15+66+105, 28+20+72+112] = [166, 188, 210, 232]
        // Row 3: [8*4+9*1+10*9+11*13, 8*5+9*2+10*10+11*14, 8*6+9*3+10*11+11*15, 8*7+9*4+10*12+11*16]
        //        = [32+9+90+143, 40+18+100+154, 48+27+110+165, 56+36+120+176] = [274, 312, 350, 388]
        // Row 4: [12*4+13*1+14*9+15*13, 12*5+13*2+14*10+15*14, 12*6+13*3+14*11+15*15, 12*7+13*4+14*12+15*16]
        //        = [48+13+126+195, 60+26+140+210, 72+39+154+225, 84+52+168+240] = [382, 436, 490, 544]
        var expected = Mat4.Create(58, 64, 70, 76, 166, 188, 210, 232, 274, 312, 350, 388, 382, 436, 490, 544);

        var resultWithDest = Mat4.Multiply(m1, m2, Mat4.Create());
        Assert.True(Mat4EqualsApprox(resultWithDest, expected)); // Use approx for potential float errors

        var resultWithoutDest = Mat4.Multiply(m1, m2);
        Assert.True(Mat4EqualsApprox(resultWithoutDest, expected));

        // Test alias
        var resultMulWithDest = Mat4.Mul(m1, m2, Mat4.Create());
        Assert.True(Mat4EqualsApprox(resultMulWithDest, expected));
        var resultMulWithoutDest = Mat4.Mul(m1, m2);
        Assert.True(Mat4EqualsApprox(resultMulWithoutDest, expected));
    }

    [TestMethod]
    public void Inverse_Should_Compute_Matrix_Inverse()
    {
        var testCases = new[]
        {
            new {
                M = Mat4.Create(2, 1, 3, 1, 1, 2, 1, 2, 3, 1, 2, 1, 1, 2, 1, 2),
                Expected = Mat4.Create(
                    0.2f, -0.2f, 0.6f, -0.2f,
                    -0.2f, 0.6f, -0.2f, 0.2f,
                    0.6f, -0.2f, 0.2f, -0.2f,
                    -0.2f, 0.2f, -0.2f, 0.6f
                )
            },
            new {
                M = Mat4.Create(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 2, 3, 4, 1),
                Expected = Mat4.Create(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, -2, -3, -4, 1)
            }

        };

        foreach (var testCase in testCases)
        {
            var resultWithDest = Mat4.Inverse(testCase.M, Mat4.Create());
            Assert.True(Mat4EqualsApprox(resultWithDest, testCase.Expected, 1e-5f));

            var resultWithoutDest = Mat4.Inverse(testCase.M);
            Assert.True(Mat4EqualsApprox(resultWithoutDest, testCase.Expected, 1e-5f));

            // Test alias
            var resultInvertWithDest = Mat4.Invert(testCase.M, Mat4.Create());
            Assert.True(Mat4EqualsApprox(resultInvertWithDest, testCase.Expected, 1e-5f));
            var resultInvertWithoutDest = Mat4.Invert(testCase.M);
            Assert.True(Mat4EqualsApprox(resultInvertWithoutDest, testCase.Expected, 1e-5f));
        }
    }

    [TestMethod]
    public void Determinant_Should_Compute_Correctly()
    {
        // Simple diagonal matrix
        var m1 = Mat4.Create(2, 0, 0, 0, 0, 3, 0, 0, 0, 0, 4, 0, 0, 0, 0, 5);
        // Diagonal: 2 * 3 * 4 * 5 = 120
        var expectedDet1 = 120.0f;
        Assert.Equal(expectedDet1, Mat4.Determinant(m1), 5); // Precision check

        // Identity matrix
        var m2 = Mat4.Identity();
        var expectedDet2 = 1.0f;
        Assert.Equal(expectedDet2, Mat4.Determinant(m2), 5);

        // Test with the matrix from the 'should compute determinant' test case
        var m3 = Mat4.Create(
           2, 1, 3, 0,
           1, 2, 1, 0,
           3, 1, 2, 0,
           4, 5, 6, 1
        );
        // Manual calc for 3x3 submatrix (top-left 3x3) determinant:
        // |2 1 3|
        // |1 2 1| = 2*(2*2 - 1*1) - 1*(1*2 - 1*3) + 3*(1*1 - 2*3)
        // |3 1 2|   = 2*(4-1) - 1*(2-3) + 3*(1-6) = 2*3 - 1*(-1) + 3*(-5) = 6 + 1 - 15 = -8
        // Full 4x4 det = -8 * 1 = -8 (since last row is [4,5,6,1])
        var expectedDet3 = -8.0f;
        Assert.Equal(expectedDet3, Mat4.Determinant(m3), 5); // Precision check
    }

    [TestMethod]
    public void Set_Translation_Get_Translation_Should_Work()
    {
        var m = CreateTestMatrix();
        var translation = Vec3.Create(11, 22, 33);
        // Expected: m with last column [11, 22, 33, 1]
        var expectedSet = Mat4.Create(
            0, 1, 2, 3,
            4, 5, 6, 7,
            8, 9, 10, 11,
            11, 22, 33, 1 // Modified last column
        );

        var resultSetWithDest = Mat4.SetTranslation(m, translation, Mat4.Create());
        Assert.True(Mat4EqualsExact(resultSetWithDest, expectedSet));

        var resultSetWithoutDest = Mat4.SetTranslation(m, translation);
        Assert.True(Mat4EqualsExact(resultSetWithoutDest, expectedSet));

        // Get translation
        var expectedGet = Vec3.Create(12, 13, 14); // Original m's translation
        var resultGetWithDest = Mat4.GetTranslation(m, Vec3.Create());
        Assert.True(Vec3.Equals(expectedGet, resultGetWithDest)); // Assuming Vec3.Equals exists

        var resultGetWithoutDest = Mat4.GetTranslation(m);
        Assert.True(Vec3.Equals(expectedGet, resultGetWithoutDest));
    }

    [TestMethod]
    public void Get_Axis_Set_Axis_Should_Work()
    {
        var m = CreateTestMatrix();
        var newAxis = Vec3.Create(11, 22, 33);

        // Test GetAxis (X-axis = column 0)
        var expectedGetX = Vec3.Create(0, 1, 2); // Column 0 of m
        var resultGetXWithDest = Mat4.GetAxis(m, 0, Vec3.Create());
        Assert.True(Vec3.Equals(expectedGetX, resultGetXWithDest));
        var resultGetWithoutDest = Mat4.GetAxis(m, 0);
        Assert.True(Vec3.Equals(expectedGetX, resultGetWithoutDest));

        // Test GetAxis (Y-axis = column 1)
        var expectedGetY = Vec3.Create(4, 5, 6); // Column 1 of m
        var resultGetYWithDest = Mat4.GetAxis(m, 1, Vec3.Create());
        Assert.True(Vec3.Equals(expectedGetY, resultGetYWithDest));
        var resultGetYWithoutDest = Mat4.GetAxis(m, 1);
        Assert.True(Vec3.Equals(expectedGetY, resultGetYWithoutDest));

        // Test GetAxis (Z-axis = column 2)
        var expectedGetZ = Vec3.Create(8, 9, 10); // Column 2 of m
        var resultGetZWithDest = Mat4.GetAxis(m, 2, Vec3.Create());
        Assert.True(Vec3.Equals(expectedGetZ, resultGetZWithDest));
        var resultGetZWithoutDest = Mat4.GetAxis(m, 2);
        Assert.True(Vec3.Equals(expectedGetZ, resultGetZWithoutDest));

        // Test SetAxis (X-axis)
        var expectedSetX = Mat4.Create(
            11, 1, 2, 3, // Column 0 changed
             4, 5, 6, 7,
             8, 9, 10, 11,
            12, 13, 14, 15
        );
        var resultSetXWithDest = Mat4.SetAxis(m, newAxis, 0, Mat4.Create());
        Assert.True(Mat4EqualsExact(resultSetXWithDest, expectedSetX));
        var resultSetXWithoutDest = Mat4.SetAxis(m, newAxis, 0);
        Assert.True(Mat4EqualsExact(resultSetXWithoutDest, expectedSetX));

        // Test SetAxis (Y-axis)
        var expectedSetY = Mat4.Create(
            0, 1, 2, 3,
           11, 22, 33, 7, // Column 1 changed
            8, 9, 10, 11,
           12, 13, 14, 15
        );
        var resultSetYWithDest = Mat4.SetAxis(m, newAxis, 1, Mat4.Create());
        Assert.True(Mat4EqualsExact(resultSetYWithDest, expectedSetY));
        var resultSetYWithoutDest = Mat4.SetAxis(m, newAxis, 1);
        Assert.True(Mat4EqualsExact(resultSetYWithoutDest, expectedSetY));

        // Test SetAxis (Z-axis)
        var expectedSetZ = Mat4.Create(
            0, 1, 2, 3,
            4, 5, 6, 7,
           11, 22, 33, 11, // Column 2 changed
           12, 13, 14, 15
        );
        var resultSetZWithDest = Mat4.SetAxis(m, newAxis, 2, Mat4.Create());
        Assert.True(Mat4EqualsExact(resultSetZWithDest, expectedSetZ));
        var resultSetZWithoutDest = Mat4.SetAxis(m, newAxis, 2);
        Assert.True(Mat4EqualsExact(resultSetZWithoutDest, expectedSetZ));
    }

    [TestMethod]
    public void Get_Scaling_Should_Work()
    {
        var m = Mat4.Create(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);
        // Scaling is sqrt of sum of squares of each *row* of the upper 3x3
        // Note: The C# GetScaling uses columns, but the TS test implies rows for scaling factors.
        // However, the C# implementation `GetScaling` reads columns. Let's test the C# impl as written.
        // Column 0: [1, 5, 9] -> sqrt(1+25+81) = sqrt(107)
        // Column 1: [2, 6, 10] -> sqrt(4+36+100) = sqrt(140)
        // Column 2: [3, 7, 11] -> sqrt(9+49+121) = sqrt(179)
        var expected = Vec3.Create(MathF.Sqrt(107), MathF.Sqrt(140), MathF.Sqrt(179));
        var resultWithDest = Mat4.GetScaling(m, Vec3.Create());
        Assert.True(Vec3.EqualsApproximately(expected, resultWithDest)); // Assuming Vec3.EqualsApproximately
        var resultWithoutDest = Mat4.GetScaling(m);
        Assert.True(Vec3.EqualsApproximately(expected, resultWithoutDest));
    }

    [TestMethod]
    public void Translation_Translate_Should_Work()
    {
        var translation = Vec3.Create(2, 3, 4);
        var expectedTranslation = Mat4.Create(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 2, 3, 4, 1);

        var resultTransWithDest = Mat4.Translation(translation, Mat4.Create());
        Assert.True(Mat4EqualsExact(resultTransWithDest, expectedTranslation));
        var resultTransWithoutDest = Mat4.Translation(translation);
        Assert.True(Mat4EqualsExact(resultTransWithoutDest, expectedTranslation));

        var m = CreateTestMatrix();
        // Manual calculation for translate result (post-multiply translation matrix):
        // Result = m * TranslationMatrix
        // [m00 m01 m02 m03]   [1 0 0 0]   [m00 m01 m02 m00*tx+m01*ty+m02*tz+m03]
        // [m10 m11 m12 m13] * [0 1 0 0] = [m10 m11 m12 m10*tx+m11*ty+m12*tz+m13]
        // [m20 m21 m22 m23]   [0 0 1 0]   [m20 m21 m22 m20*tx+m21*ty+m22*tz+m23]
        // [m30 m31 m32 m33]   [tx ty tz 1] [m30 m31 m32 m30*tx+m31*ty+m32*tz+m33]
        // Last column of result:
        // [0*2 + 1*3 + 2*4 + 3, 4*2 + 5*3 + 6*4 + 7, 8*2 + 9*3 + 10*4 + 11, 12*2 + 13*3 + 14*4 + 15]
        // = [0+3+8+3, 8+15+24+7, 16+27+40+11, 24+39+56+15] = [14, 54, 94, 134]
        var expectedTranslate = Mat4.Create(0, 1, 2, 14, 4, 5, 6, 54, 8, 9, 10, 94, 12, 13, 14, 134);

        var resultTranslateWithDest = Mat4.Translate(m, translation, Mat4.Create());
        Assert.True(Mat4EqualsExact(resultTranslateWithDest, expectedTranslate));
        var resultTranslateWithoutDest = Mat4.Translate(m, translation);
        Assert.True(Mat4EqualsExact(resultTranslateWithoutDest, expectedTranslate));
    }

    [TestMethod]
    public void Rotation_X_Rotate_X_Should_Work()
    {
        var angle = 1.23f;
        var c = MathF.Cos(angle);
        var s = MathF.Sin(angle);
        var expectedRotationX = Mat4.Create(1, 0, 0, 0, 0, c, s, 0, 0, -s, c, 0, 0, 0, 0, 1);

        var resultRotXWithDest = Mat4.RotationX(angle, Mat4.Create());
        Assert.True(Mat4EqualsApprox(resultRotXWithDest, expectedRotationX));
        var resultRotXWithoutDest = Mat4.RotationX(angle);
        Assert.True(Mat4EqualsApprox(resultRotXWithoutDest, expectedRotationX));

        var m = CreateTestMatrix();
        var expectedRotateX = Mat4.Multiply(m, expectedRotationX); // Use reference multiply

        var resultRotateXWithDest = Mat4.RotateX(m, angle, Mat4.Create());
        Assert.True(Mat4EqualsApprox(resultRotateXWithDest, expectedRotateX));
        var resultRotateXWithoutDest = Mat4.RotateX(m, angle);
        Assert.True(Mat4EqualsApprox(resultRotateXWithoutDest, expectedRotateX));
    }

    [TestMethod]
    public void Rotation_Y_Rotate_Y_Should_Work()
    {
        var angle = 1.23f;
        var c = MathF.Cos(angle);
        var s = MathF.Sin(angle);
        var expectedRotationY = Mat4.Create(c, 0, -s, 0, 0, 1, 0, 0, s, 0, c, 0, 0, 0, 0, 1);

        var resultRotYWithDest = Mat4.RotationY(angle, Mat4.Create());
        Assert.True(Mat4EqualsApprox(resultRotYWithDest, expectedRotationY));
        var resultRotYWithoutDest = Mat4.RotationY(angle);
        Assert.True(Mat4EqualsApprox(resultRotYWithoutDest, expectedRotationY));

        var m = CreateTestMatrix();
        var expectedRotateY = Mat4.Multiply(m, expectedRotationY);

        var resultRotateYWithDest = Mat4.RotateY(m, angle, Mat4.Create());
        Assert.True(Mat4EqualsApprox(resultRotateYWithDest, expectedRotateY));
        var resultRotateYWithoutDest = Mat4.RotateY(m, angle);
        Assert.True(Mat4EqualsApprox(resultRotateYWithoutDest, expectedRotateY));
    }

    [TestMethod]
    public void Rotation_Z_Rotate_Z_Should_Work()
    {
        var angle = 1.23f;
        var c = MathF.Cos(angle);
        var s = MathF.Sin(angle);
        var expectedRotationZ = Mat4.Create(c, s, 0, 0, -s, c, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1);

        var resultRotZWithDest = Mat4.RotationZ(angle, Mat4.Create());
        Assert.True(Mat4EqualsApprox(resultRotZWithDest, expectedRotationZ));
        var resultRotZWithoutDest = Mat4.RotationZ(angle);
        Assert.True(Mat4EqualsApprox(resultRotZWithoutDest, expectedRotationZ));

        var m = CreateTestMatrix();
        var expectedRotateZ = Mat4.Multiply(m, expectedRotationZ);

        var resultRotateZWithDest = Mat4.RotateZ(m, angle, Mat4.Create());
        Assert.True(Mat4EqualsApprox(resultRotateZWithDest, expectedRotateZ));
        var resultRotateZWithoutDest = Mat4.RotateZ(m, angle);
        Assert.True(Mat4EqualsApprox(resultRotateZWithoutDest, expectedRotateZ));
    }

    [TestMethod]
    public void Scaling_Scale_Should_Work()
    {
        var scale = Vec3.Create(2, 3, 4);
        var expectedScaling = Mat4.Create(2, 0, 0, 0, 0, 3, 0, 0, 0, 0, 4, 0, 0, 0, 0, 1);

        var resultScaleWithDest = Mat4.Scaling(scale, Mat4.Create());
        Assert.True(Mat4EqualsExact(resultScaleWithDest, expectedScaling));
        var resultScaleWithoutDest = Mat4.Scaling(scale);
        Assert.True(Mat4EqualsExact(resultScaleWithoutDest, expectedScaling));

        var m = CreateTestMatrix();
        // Result: Multiply each column by the corresponding scale factor
        // Col 0 *= 2, Col 1 *= 3, Col 2 *= 4, Col 3 unchanged
        // [0*2, 1*2, 2*2, 3*2] = [0, 2, 4, 6]
        // [4*3, 5*3, 6*3, 7*3] = [12, 15, 18, 21]
        // [8*4, 9*4, 10*4, 11*4] = [32, 36, 40, 44]
        // [12, 13, 14, 15] (unchanged)
        var expectedScale = Mat4.Create(0, 2, 4, 6, 12, 15, 18, 21, 32, 36, 40, 44, 12, 13, 14, 15);

        var resultScaleMatWithDest = Mat4.Scale(m, scale, Mat4.Create());
        Assert.True(Mat4EqualsExact(resultScaleMatWithDest, expectedScale));
        var resultScaleMatWithoutDest = Mat4.Scale(m, scale);
        Assert.True(Mat4EqualsExact(resultScaleMatWithoutDest, expectedScale));
    }

    [TestMethod]
    public void From_Mat3_Should_Create_Correct_Matrix()
    {
        var m3 = Mat3.Create(1, 2, 3, 4, 5, 6, 7, 8, 9);
        // Expected: Upper 3x3 filled, rest identity-like
        var expected = Mat4.Create(1, 2, 3, 0, 4, 5, 6, 0, 7, 8, 9, 0, 0, 0, 0, 1);

        var resultWithDest = Mat4.FromMat3(m3, Mat4.Create());
        Assert.True(Mat4EqualsExact(resultWithDest, expected));

        var resultWithoutDest = Mat4.FromMat3(m3);
        Assert.True(Mat4EqualsExact(resultWithoutDest, expected));
    }

    [TestMethod]
    public void Perspective_Should_Create_Correct_Matrix()
    {
        var fov = 0.3f;
        var aspect = 2.0f;
        var znear = 0.1f;
        var zfar = 100.0f;

        var f = MathF.Tan(MathF.PI * 0.5f - 0.5f * fov);
        var rangeInv = 1.0f / (znear - zfar);

        var expected = Mat4.Create(
            f / aspect, 0, 0, 0,
            0, f, 0, 0,
            0, 0, zfar * rangeInv, -1,
            0, 0, zfar * znear * rangeInv, 0
        );

        var resultWithDest = Mat4.Perspective(fov, aspect, znear, zfar, Mat4.Create());
        Assert.True(Mat4EqualsApprox(resultWithDest, expected));

        var resultWithoutDest = Mat4.Perspective(fov, aspect, znear, zfar);
        Assert.True(Mat4EqualsApprox(resultWithoutDest, expected));
    }

    [TestMethod]
    public void Ortho_Should_Create_Correct_Matrix()
    {
        var left = -10.0f;
        var right = -2.0f;
        var bottom = 10.0f;
        var top = 40.0f;
        var near = 0.1f;
        var far = 100.0f;

        var expected = Mat4.Create(
            2 / (right - left), 0, 0, 0,
            0, 2 / (top - bottom), 0, 0,
            0, 0, 1 / (near - far), 0,
            (right + left) / (left - right), (top + bottom) / (bottom - top), near / (near - far), 1
        );

        var resultWithDest = Mat4.Ortho(left, right, bottom, top, near, far, Mat4.Create());
        Assert.True(Mat4EqualsApprox(resultWithDest, expected));

        var resultWithoutDest = Mat4.Ortho(left, right, bottom, top, near, far);
        Assert.True(Mat4EqualsApprox(resultWithoutDest, expected));
    }

    [TestMethod]
    public void Ortho_Should_Compute_Correct_Transforms()
    {

        var left = -2f;
        var right = 4f;
        var top = 10f;
        var bottom = 30f;
        var znear = 15f;
        var zfar = 25f;
        var m = Mat4.Ortho(left, right, bottom, top, znear, zfar);
        var p1 = Vec3.Create(left, bottom, -znear);
        var p1Transformed = Vec3.TransformMat4(p1, m);
        var expectedP1 = Vec3.Create(-1, -1, 0);
        Assert.True(Vec3.EqualsApproximately(expectedP1, p1Transformed));
    }
}
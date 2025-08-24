using SpawnDev.Blazor.UnitTesting;

namespace Blazor.WebGPU.Matrix.Tests.Float
{
    [TestClass]
    public class Vec3FloatTests 
    {
        private const float EPSILON = 0.000001f;

        private void AssertEqualApproximately(Vec3 a, Vec3 b)
        {
            Assert.True(MathF.Abs(a[0] - b[0]) < EPSILON);
            Assert.True(MathF.Abs(a[1] - b[1]) < EPSILON);
            Assert.True(MathF.Abs(a[2] - b[2]) < EPSILON);
        }

        private void AssertEqualApproximately(float a, float b)
        {
            Assert.True(MathF.Abs(a - b) < EPSILON);
        }

        [TestMethod]
        public void Should_Add()
        {
            var a = Vec3.Create(1, 2, 3);
            var b = Vec3.Create(2, 3, 4);
            var expected = Vec3.Create(3, 5, 7);

            // Test without destination
            var result1 = Vec3.Add(a, b);
            AssertEqualApproximately(result1, expected);

            // Test with destination
            var dst = Vec3.Create();
            var result2 = Vec3.Add(a, b, dst);
            Assert.Same(dst, result2);
            AssertEqualApproximately(result2, expected);
        }

        [TestMethod]
        public void Should_Compute_Angle()
        {
            var tests = new[]
            {
                new { a = Vec3.Create(1, 0, 0), b = Vec3.Create(0, 1, 0), expected = MathF.PI / 2 },
                new { a = Vec3.Create(1, 0, 0), b = Vec3.Create(-1, 0, 0), expected = MathF.PI },
                new { a = Vec3.Create(1, 0, 0), b = Vec3.Create(1, 0, 0), expected = 0f },
                new { a = Vec3.Create(1, 2, 3), b = Vec3.Create(4, 5, 6), expected = 0.2257261f }
            };

            foreach (var test in tests)
            {
                var result = Vec3.Angle(test.a, test.b);
                AssertEqualApproximately(result, test.expected);

                // Test with scaled vectors
                var aScaled = Vec3.MulScalar(test.a, 1000);
                var bScaled = Vec3.MulScalar(test.b, 1000);
                var resultScaled = Vec3.Angle(aScaled, bScaled);
                AssertEqualApproximately(resultScaled, test.expected);
            }
        }

        [TestMethod]
        public void Should_Compute_Ceil()
        {
            var input = Vec3.Create(1.1f, -1.1f, 2.9f);
            var expected = Vec3.Create(2, -1, 3);

            // Test without destination
            var result1 = Vec3.Ceil(input);
            AssertEqualApproximately(result1, expected);

            // Test with destination
            var dst = Vec3.Create();
            var result2 = Vec3.Ceil(input, dst);
            Assert.Same(dst, result2);
            AssertEqualApproximately(result2, expected);
        }

        [TestMethod]
        public void Should_Compute_Floor()
        {
            var input = Vec3.Create(1.1f, -1.1f, 2.9f);
            var expected = Vec3.Create(1, -2, 2);

            // Test without destination
            var result1 = Vec3.Floor(input);
            AssertEqualApproximately(result1, expected);

            // Test with destination
            var dst = Vec3.Create();
            var result2 = Vec3.Floor(input, dst);
            Assert.Same(dst, result2);
            AssertEqualApproximately(result2, expected);
        }

        [TestMethod]
        public void Should_Compute_Round()
        {
            var input = Vec3.Create(1.1f, -1.1f, 2.9f);
            var expected = Vec3.Create(1, -1, 3);

            // Test without destination
            var result1 = Vec3.Round(input);
            AssertEqualApproximately(result1, expected);

            // Test with destination
            var dst = Vec3.Create();
            var result2 = Vec3.Round(input, dst);
            Assert.Same(dst, result2);
            AssertEqualApproximately(result2, expected);
        }

        [TestMethod]
        public void Should_Clamp()
        {
            // Test case 1
            var input1 = Vec3.Create(2, -1, 0.5f);
            var expected1 = Vec3.Create(1, 0, 0.5f);

            // Test without destination
            var result1a = Vec3.Clamp(input1, 0, 1);
            AssertEqualApproximately(result1a, expected1);

            // Test with destination
            var dst1 = Vec3.Create();
            var result1b = Vec3.Clamp(input1, 0, 1, dst1);
            Assert.Same(dst1, result1b);
            AssertEqualApproximately(result1b, expected1);

            // Test case 2
            var input2 = Vec3.Create(-22, 50, 2.9f);
            var expected2 = Vec3.Create(-10, 5, 2.9f);

            // Test without destination
            var result2a = Vec3.Clamp(input2, -10, 5);
            AssertEqualApproximately(result2a, expected2);

            // Test with destination
            var dst2 = Vec3.Create();
            var result2b = Vec3.Clamp(input2, -10, 5, dst2);
            Assert.Same(dst2, result2b);
            AssertEqualApproximately(result2b, expected2);
        }

        [TestMethod]
        public void Should_Equals_Approximately()
        {
            var a = Vec3.Create(1, 2, 3);
            var b = Vec3.Create(1, 2, 3);
            var c = Vec3.Create(1 + EPSILON * 0.5f, 2, 3);
            var d = Vec3.Create(1.001f, 2, 3);

            Assert.True(Vec3.EqualsApproximately(a, b));
            Assert.True(Vec3.EqualsApproximately(a, c));
            Assert.False(Vec3.EqualsApproximately(a, d));
        }

        [TestMethod]
        public void Should_Equals()
        {
            var a = Vec3.Create(1, 2, 3);
            var b = Vec3.Create(1, 2, 3);
            var c = Vec3.Create(1 + EPSILON * 0.5f, 2, 3);

            Assert.True(Vec3.Equals(a, b));
            Assert.False(Vec3.Equals(a, c));
        }

        [TestMethod]
        public void Should_Subtract()
        {
            var a = Vec3.Create(1, 2, 3);
            var b = Vec3.Create(2, 4, 6);
            var expected = Vec3.Create(-1, -2, -3);

            // Test without destination
            var result1 = Vec3.Subtract(a, b);
            AssertEqualApproximately(result1, expected);

            // Test with destination
            var dst = Vec3.Create();
            var result2 = Vec3.Subtract(a, b, dst);
            Assert.Same(dst, result2);
            AssertEqualApproximately(result2, expected);
        }

        [TestMethod]
        public void Should_Sub()
        {
            var a = Vec3.Create(1, 2, 3);
            var b = Vec3.Create(2, 4, 6);
            var expected = Vec3.Create(-1, -2, -3);

            // Test without destination
            var result1 = Vec3.Sub(a, b);
            AssertEqualApproximately(result1, expected);

            // Test with destination
            var dst = Vec3.Create();
            var result2 = Vec3.Sub(a, b, dst);
            Assert.Same(dst, result2);
            AssertEqualApproximately(result2, expected);
        }

        [TestMethod]
        public void Should_Lerp()
        {
            var a = Vec3.Create(1, 2, 3);
            var b = Vec3.Create(2, 4, 6);
            var expected = Vec3.Create(1.5f, 3, 4.5f);

            // Test without destination
            var result1 = Vec3.Lerp(a, b, 0.5f);
            AssertEqualApproximately(result1, expected);

            // Test with destination
            var dst = Vec3.Create();
            var result2 = Vec3.Lerp(a, b, 0.5f, dst);
            Assert.Same(dst, result2);
            AssertEqualApproximately(result2, expected);
        }

        [TestMethod]
        public void Should_Lerp_Under_0()
        {
            var a = Vec3.Create(1, 2, 3);
            var b = Vec3.Create(2, 4, 6);
            var expected = Vec3.Create(0.5f, 1, 1.5f);

            // Test without destination
            var result1 = Vec3.Lerp(a, b, -0.5f);
            AssertEqualApproximately(result1, expected);

            // Test with destination
            var dst = Vec3.Create();
            var result2 = Vec3.Lerp(a, b, -0.5f, dst);
            Assert.Same(dst, result2);
            AssertEqualApproximately(result2, expected);
        }

        [TestMethod]
        public void Should_Lerp_Over_0()
        {
            var a = Vec3.Create(1, 2, 3);
            var b = Vec3.Create(2, 4, 6);
            var expected = Vec3.Create(2.5f, 5, 7.5f);

            // Test without destination
            var result1 = Vec3.Lerp(a, b, 1.5f);
            AssertEqualApproximately(result1, expected);

            // Test with destination
            var dst = Vec3.Create();
            var result2 = Vec3.Lerp(a, b, 1.5f, dst);
            Assert.Same(dst, result2);
            AssertEqualApproximately(result2, expected);
        }

        [TestMethod]
        public void Should_Multiply_By_Scalar()
        {
            var a = Vec3.Create(1, 2, 3);
            var expected = Vec3.Create(2, 4, 6);

            // Test without destination
            var result1 = Vec3.MulScalar(a, 2);
            AssertEqualApproximately(result1, expected);

            // Test with destination
            var dst = Vec3.Create();
            var result2 = Vec3.MulScalar(a, 2, dst);
            Assert.Same(dst, result2);
            AssertEqualApproximately(result2, expected);
        }

        [TestMethod]
        public void Should_Scale()
        {
            var a = Vec3.Create(1, 2, 3);
            var expected = Vec3.Create(2, 4, 6);

            // Test without destination
            var result1 = Vec3.Scale(a, 2);
            AssertEqualApproximately(result1, expected);

            // Test with destination
            var dst = Vec3.Create();
            var result2 = Vec3.Scale(a, 2, dst);
            Assert.Same(dst, result2);
            AssertEqualApproximately(result2, expected);
        }

        [TestMethod]
        public void Should_Add_Scaled()
        {
            var a = Vec3.Create(1, 2, 3);
            var b = Vec3.Create(2, 4, 6);
            var expected = Vec3.Create(5, 10, 15);

            // Test without destination
            var result1 = Vec3.AddScaled(a, b, 2);
            AssertEqualApproximately(result1, expected);

            // Test with destination
            var dst = Vec3.Create();
            var result2 = Vec3.AddScaled(a, b, 2, dst);
            Assert.Same(dst, result2);
            AssertEqualApproximately(result2, expected);
        }

        [TestMethod]
        public void Should_Divide_By_Scalar()
        {
            var a = Vec3.Create(1, 2, 3);
            var expected = Vec3.Create(0.5f, 1, 1.5f);

            // Test without destination
            var result1 = Vec3.DivScalar(a, 2);
            AssertEqualApproximately(result1, expected);

            // Test with destination
            var dst = Vec3.Create();
            var result2 = Vec3.DivScalar(a, 2, dst);
            Assert.Same(dst, result2);
            AssertEqualApproximately(result2, expected);
        }

        [TestMethod]
        public void Should_Inverse()
        {
            var a = Vec3.Create(2, 3, -4);
            var expected = Vec3.Create(1f / 2f, 1f / 3f, 1f / -4f);

            // Test without destination
            var result1 = Vec3.Inverse(a);
            AssertEqualApproximately(result1, expected);

            // Test with destination
            var dst = Vec3.Create();
            var result2 = Vec3.Inverse(a, dst);
            Assert.Same(dst, result2);
            AssertEqualApproximately(result2, expected);
        }

        [TestMethod]
        public void Should_Cross()
        {
            var a = Vec3.Create(1, 2, 3);
            var b = Vec3.Create(2, 4, 6);
            var expected = Vec3.Create(
                2 * 6 - 3 * 4,
                3 * 2 - 1 * 6,
                1 * 4 - 2 * 2
            );

            // Test without destination
            var result1 = Vec3.Cross(a, b);
            AssertEqualApproximately(result1, expected);

            // Test with destination
            var dst = Vec3.Create();
            var result2 = Vec3.Cross(a, b, dst);
            Assert.Same(dst, result2);
            AssertEqualApproximately(result2, expected);
        }

        [TestMethod]
        public void Should_Compute_Dot_Product()
        {
            var a = Vec3.Create(1, 2, 3);
            var b = Vec3.Create(2, 4, 6);
            var expected = 1 * 2 + 2 * 4 + 3 * 6;

            var result = Vec3.Dot(a, b);
            Assert.Equal(expected, result);
        }

        [TestMethod]
        public void Should_Compute_Length()
        {
            var a = Vec3.Create(1, 2, 3);
            var expected = MathF.Sqrt(1 * 1 + 2 * 2 + 3 * 3);

            var result = Vec3.Length(a);
            Assert.Equal(expected, result);
        }

        [TestMethod]
        public void Should_Compute_Length_Squared()
        {
            var a = Vec3.Create(1, 2, 3);
            var expected = 1 * 1 + 2 * 2 + 3 * 3;

            var result = Vec3.LengthSq(a);
            Assert.Equal(expected, result);
        }

        [TestMethod]
        public void Should_Compute_Len()
        {
            var a = Vec3.Create(1, 2, 3);
            var expected = MathF.Sqrt(1 * 1 + 2 * 2 + 3 * 3);

            var result = Vec3.Len(a);
            Assert.Equal(expected, result);
        }

        [TestMethod]
        public void Should_Compute_Len_Sq()
        {
            var a = Vec3.Create(1, 2, 3);
            var expected = 1 * 1 + 2 * 2 + 3 * 3;

            var result = Vec3.LenSq(a);
            Assert.Equal(expected, result);
        }

        [TestMethod]
        public void Should_Compute_Distance()
        {
            var a = Vec3.Create(1, 2, 3);
            var b = Vec3.Create(3, 5, 7);
            var expected = MathF.Sqrt(2 * 2 + 3 * 3 + 4 * 4);

            var result = Vec3.Distance(a, b);
            Assert.Equal(expected, result);
        }

        [TestMethod]
        public void Should_Compute_Distance_Squared()
        {
            var a = Vec3.Create(1, 2, 3);
            var b = Vec3.Create(3, 5, 7);
            var expected = 2 * 2 + 3 * 3 + 4 * 4;

            var result = Vec3.DistanceSq(a, b);
            Assert.Equal(expected, result);
        }

        [TestMethod]
        public void Should_Compute_Dist()
        {
            var a = Vec3.Create(1, 2, 3);
            var b = Vec3.Create(3, 5, 7);
            var expected = MathF.Sqrt(2 * 2 + 3 * 3 + 4 * 4);

            var result = Vec3.Dist(a, b);
            Assert.Equal(expected, result);
        }

        [TestMethod]
        public void Should_Compute_Dist_Squared()
        {
            var a = Vec3.Create(1, 2, 3);
            var b = Vec3.Create(3, 5, 7);
            var expected = 2 * 2 + 3 * 3 + 4 * 4;

            var result = Vec3.DistSq(a, b);
            Assert.Equal(expected, result);
        }

        [TestMethod]
        public void Should_Normalize()
        {
            var a = Vec3.Create(1, 2, 3);
            var length = MathF.Sqrt(1 * 1 + 2 * 2 + 3 * 3);
            var expected = Vec3.Create(1 / length, 2 / length, 3 / length);

            // Test without destination
            var result1 = Vec3.Normalize(a);
            AssertEqualApproximately(result1, expected);

            // Test with destination
            var dst = Vec3.Create();
            var result2 = Vec3.Normalize(a, dst);
            Assert.Same(dst, result2);
            AssertEqualApproximately(result2, expected);
        }

        [TestMethod]
        public void Should_Negate()
        {
            var a = Vec3.Create(1, 2, 3);
            var expected = Vec3.Create(-1, -2, -3);

            // Test without destination
            var result1 = Vec3.Negate(a);
            AssertEqualApproximately(result1, expected);

            // Test with destination
            var dst = Vec3.Create();
            var result2 = Vec3.Negate(a, dst);
            Assert.Same(dst, result2);
            AssertEqualApproximately(result2, expected);
        }

        [TestMethod]
        public void Should_Copy()
        {
            var a = Vec3.Create(1, 2, 3);
            var expected = Vec3.Create(1, 2, 3);

            // Test without destination
            var result1 = Vec3.Copy(a);
            Assert.NotSame(a, result1);
            AssertEqualApproximately(result1, expected);

            // Test with destination
            var dst = Vec3.Create();
            var result2 = Vec3.Copy(a, dst);
            Assert.Same(dst, result2);
            AssertEqualApproximately(result2, expected);
        }

        [TestMethod]
        public void Should_Clone()
        {
            var a = Vec3.Create(1, 2, 3);
            var expected = Vec3.Create(1, 2, 3);

            // Test without destination
            var result1 = Vec3.Clone(a);
            Assert.NotSame(a, result1);
            AssertEqualApproximately(result1, expected);

            // Test with destination
            var dst = Vec3.Create();
            var result2 = Vec3.Clone(a, dst);
            Assert.Same(dst, result2);
            AssertEqualApproximately(result2, expected);
        }

        [TestMethod]
        public void Should_Set()
        {
            var expected = Vec3.Create(2, 3, 4);

            // Test without destination
            var result1 = Vec3.Set(2, 3, 4);
            AssertEqualApproximately(result1, expected);

            // Test with destination
            var dst = Vec3.Create();
            var result2 = Vec3.Set(2, 3, 4, dst);
            Assert.Same(dst, result2);
            AssertEqualApproximately(result2, expected);
        }

        [TestMethod]
        public void Should_Multiply()
        {
            var a = Vec3.Create(1, 2, 3);
            var b = Vec3.Create(2, 4, 6);
            var expected = Vec3.Create(2, 8, 18);

            // Test without destination
            var result1 = Vec3.Multiply(a, b);
            AssertEqualApproximately(result1, expected);

            // Test with destination
            var dst = Vec3.Create();
            var result2 = Vec3.Multiply(a, b, dst);
            Assert.Same(dst, result2);
            AssertEqualApproximately(result2, expected);
        }

        [TestMethod]
        public void Should_Mul()
        {
            var a = Vec3.Create(1, 2, 3);
            var b = Vec3.Create(2, 4, 6);
            var expected = Vec3.Create(2, 8, 18);

            // Test without destination
            var result1 = Vec3.Mul(a, b);
            AssertEqualApproximately(result1, expected);

            // Test with destination
            var dst = Vec3.Create();
            var result2 = Vec3.Mul(a, b, dst);
            Assert.Same(dst, result2);
            AssertEqualApproximately(result2, expected);
        }

        [TestMethod]
        public void Should_Divide()
        {
            var a = Vec3.Create(1, 2, 3);
            var b = Vec3.Create(2, 3, 4);
            var expected = Vec3.Create(1f / 2f, 2f / 3f, 3f / 4f);

            // Test without destination
            var result1 = Vec3.Divide(a, b);
            AssertEqualApproximately(result1, expected);

            // Test with destination
            var dst = Vec3.Create();
            var result2 = Vec3.Divide(a, b, dst);
            Assert.Same(dst, result2);
            AssertEqualApproximately(result2, expected);
        }

        [TestMethod]
        public void Should_Div()
        {
            var a = Vec3.Create(1, 2, 3);
            var b = Vec3.Create(2, 3, 4);
            var expected = Vec3.Create(1f / 2f, 2f / 3f, 3f / 4f);

            // Test without destination
            var result1 = Vec3.Div(a, b);
            AssertEqualApproximately(result1, expected);

            // Test with destination
            var dst = Vec3.Create();
            var result2 = Vec3.Div(a, b, dst);
            Assert.Same(dst, result2);
            AssertEqualApproximately(result2, expected);
        }

        [TestMethod]
        public void Should_From_Values()
        {
            var expected = Vec3.Create(1, 2, 3);
            var result = Vec3.FromValues(1, 2, 3);
            AssertEqualApproximately(result, expected);
        }

        [TestMethod]
        public void Should_Random()
        {
            for (int i = 0; i < 10; ++i) // Reduced iterations for testing
            {
                var v1 = Vec3.Random();
                AssertEqualApproximately(Vec3.Length(v1), 1);

                var v2 = Vec3.Random(2);
                AssertEqualApproximately(Vec3.Length(v2), 2);

                var vp5 = Vec3.Random(0.5f);
                AssertEqualApproximately(Vec3.Length(vp5), 0.5f);

                var vd = Vec3.Create();
                var vn = Vec3.Random(3, vd);
                Assert.Same(vd, vn);
                AssertEqualApproximately(Vec3.Length(vd), 3);
            }
        }

        //[TestMethod]
        //public void Should_Transform_By_Mat3()
        //{
        //    var v = Vec3.Create(1, 2, 3);
        //    // Create a 3x3 matrix as a 12-element array (4x3)
        //    var m = new float[] {
        //        4, 0, 0, 0,
        //        0, 5, 0, 0,
        //        0, 0, 6, 0
        //    };
        //    var expected = Vec3.Create(4, 10, 18);

        //    // Test without destination
        //    var result1 = Vec3.TransformMat3(v, m);
        //    AssertEqualApproximately(result1, expected);

        //    // Test with destination
        //    var dst = Vec3.Create();
        //    var result2 = Vec3.TransformMat3(v, m, dst);
        //    Assert.Same(dst, result2);
        //    AssertEqualApproximately(result2, expected);
        //}

        //[TestMethod]
        //public void Should_Transform_By_Mat4()
        //{
        //    var v = Vec3.Create(1, 2, 3);
        //    // Create a 4x4 matrix
        //    var m = new float[] {
        //        1, 0, 0, 0,
        //        0, 2, 0, 0,
        //        0, 0, 3, 0,
        //        4, 5, 6, 1
        //    };
        //    var expected = Vec3.Create(5, 9, 15);

        //    // Test without destination
        //    var result1 = Vec3.TransformMat4(v, m);
        //    AssertEqualApproximately(result1, expected);

        //    // Test with destination
        //    var dst = Vec3.Create();
        //    var result2 = Vec3.TransformMat4(v, m, dst);
        //    Assert.Same(dst, result2);
        //    AssertEqualApproximately(result2, expected);
        //}

        //[TestMethod]
        //public void Should_Transform_By_Mat4_Upper3x3()
        //{
        //    var v = Vec3.Create(2, 3, 4);
        //    // Create a 4x4 matrix
        //    var m = new float[] {
        //        1, 0, 0, 0,
        //        0, 2, 0, 0,
        //        0, 0, 3, 0,
        //        4, 5, 6, 1
        //    };
        //    var expected = Vec3.Create(2, 6, 12);

        //    // Test without destination
        //    var result1 = Vec3.TransformMat4Upper3x3(v, m);
        //    AssertEqualApproximately(result1, expected);

        //    // Test with destination
        //    var dst = Vec3.Create();
        //    var result2 = Vec3.TransformMat4Upper3x3(v, m, dst);
        //    Assert.Same(dst, result2);
        //    AssertEqualApproximately(result2, expected);
        //}

        [TestMethod]
        public void Should_Zero()
        {
            var v = Vec3.Zero();
            AssertEqualApproximately(v, Vec3.Create(0, 0, 0));

            var v2 = Vec3.Create(1, 2, 3);
            var vn = Vec3.Zero(v2);
            Assert.Same(v2, vn);
            AssertEqualApproximately(v2, Vec3.Create(0, 0, 0));
        }

        [TestMethod]
        public void Should_Rotate_X()
        {
            // Rotation around world origin [0, 0, 0]
            var a = Vec3.Create(0, 1, 0);
            var b = Vec3.Create(0, 0, 0);
            var expected = Vec3.Create(0, -1, 0);

            // Test without destination
            var result1 = Vec3.RotateX(a, b, MathF.PI);
            AssertEqualApproximately(result1, expected);

            // Test with destination
            var dst = Vec3.Create();
            var result2 = Vec3.RotateX(a, b, MathF.PI, dst);
            Assert.Same(dst, result2);
            AssertEqualApproximately(result2, expected);

            // Rotation around an arbitrary origin
            var a2 = Vec3.Create(2, 7, 0);
            var b2 = Vec3.Create(2, 5, 0);
            var expected2 = Vec3.Create(2, 3, 0);

            // Test without destination
            var result3 = Vec3.RotateX(a2, b2, MathF.PI);
            AssertEqualApproximately(result3, expected2);

            // Test with destination
            var dst2 = Vec3.Create();
            var result4 = Vec3.RotateX(a2, b2, MathF.PI, dst2);
            Assert.Same(dst2, result4);
            AssertEqualApproximately(result4, expected2);
        }

        [TestMethod]
        public void Should_Rotate_Y()
        {
            // Rotation around world origin [0, 0, 0]
            var a = Vec3.Create(1, 0, 0);
            var b = Vec3.Create(0, 0, 0);
            var expected = Vec3.Create(-1, 0, 0);

            // Test without destination
            var result1 = Vec3.RotateY(a, b, MathF.PI);
            AssertEqualApproximately(result1, expected);

            // Test with destination
            var dst = Vec3.Create();
            var result2 = Vec3.RotateY(a, b, MathF.PI, dst);
            Assert.Same(dst, result2);
            AssertEqualApproximately(result2, expected);

            // Rotation around an arbitrary origin
            var a2 = Vec3.Create(-2, 3, 10);
            var b2 = Vec3.Create(-4, 3, 10);
            var expected2 = Vec3.Create(-6, 3, 10);

            // Test without destination
            var result3 = Vec3.RotateY(a2, b2, MathF.PI);
            AssertEqualApproximately(result3, expected2);

            // Test with destination
            var dst2 = Vec3.Create();
            var result4 = Vec3.RotateY(a2, b2, MathF.PI, dst2);
            Assert.Same(dst2, result4);
            AssertEqualApproximately(result4, expected2);
        }

        [TestMethod]
        public void Should_Rotate_Z()
        {
            // Rotation around world origin [0, 0, 0]
            var a = Vec3.Create(0, 1, 0);
            var b = Vec3.Create(0, 0, 0);
            var expected = Vec3.Create(0, -1, 0);

            // Test without destination
            var result1 = Vec3.RotateZ(a, b, MathF.PI);
            AssertEqualApproximately(result1, expected);

            // Test with destination
            var dst = Vec3.Create();
            var result2 = Vec3.RotateZ(a, b, MathF.PI, dst);
            Assert.Same(dst, result2);
            AssertEqualApproximately(result2, expected);

            // Rotation around an arbitrary origin
            var a2 = Vec3.Create(0, 6, -5);
            var b2 = Vec3.Create(0, 0, -5);
            var expected2 = Vec3.Create(0, -6, -5);

            // Test without destination
            var result3 = Vec3.RotateZ(a2, b2, MathF.PI);
            AssertEqualApproximately(result3, expected2);

            // Test with destination
            var dst2 = Vec3.Create();
            var result4 = Vec3.RotateZ(a2, b2, MathF.PI, dst2);
            Assert.Same(dst2, result4);
            AssertEqualApproximately(result4, expected2);
        }

        [TestMethod]
        public void Should_Set_Length()
        {
            var a = Vec3.Create(1, 1, 1);
            var len = 14.6f;
            var expectedLength = MathF.Sqrt(3) * (len / MathF.Sqrt(3)); // Should be len
            var expected = Vec3.Normalize(a);
            expected = Vec3.MulScalar(expected, len);

            // Test without destination
            var result1 = Vec3.SetLength(a, len);
            AssertEqualApproximately(Vec3.Length(result1), len);

            // Test with destination
            var dst = Vec3.Create();
            var result2 = Vec3.SetLength(a, len, dst);
            Assert.Same(dst, result2);
            AssertEqualApproximately(Vec3.Length(result2), len);
        }

        [TestMethod]
        public void Should_Truncate()
        {
            var a = Vec3.Create(8.429313930168536f, 8.429313930168536f, 8.429313930168536f);

            // Should shorten the vector
            var maxLen1 = 4.0f;
            var expected1 = Vec3.Normalize(a);
            expected1 = Vec3.MulScalar(expected1, maxLen1);

            // Test without destination
            var result1 = Vec3.Truncate(a, maxLen1);
            AssertEqualApproximately(result1, expected1);

            // Test with destination
            var dst1 = Vec3.Create();
            var result2 = Vec3.Truncate(a, maxLen1, dst1);
            Assert.Same(dst1, result2);
            AssertEqualApproximately(result2, expected1);

            // Should preserve the vector when shorter than maxLen
            var maxLen2 = 18.0f;
            var expected2 = Vec3.Clone(a);

            // Test without destination
            var result3 = Vec3.Truncate(a, maxLen2);
            AssertEqualApproximately(result3, expected2);

            // Test with destination
            var dst2 = Vec3.Create();
            var result4 = Vec3.Truncate(a, maxLen2, dst2);
            Assert.Same(dst2, result4);
            AssertEqualApproximately(result4, expected2);
        }

        [TestMethod]
        public void Should_Midpoint()
        {
            // Should return the midpoint
            var vecA = Vec3.Create(0, 0, 0);
            var vecB = Vec3.Create(10, 10, 10);
            var expected = Vec3.Create(5, 5, 5);

            var result = Vec3.Midpoint(vecA, vecB);
            AssertEqualApproximately(result, expected);

            // Should handle negatives
            var vecA2 = Vec3.Create(-10, -10, -10);
            var vecB2 = Vec3.Create(10, 10, 10);
            var expected2 = Vec3.Create(0, 0, 0);

            var result2 = Vec3.Midpoint(vecA2, vecB2);
            AssertEqualApproximately(result2, expected2);
        }
    }
}
using SpawnDev.Blazor.UnitTesting;

namespace Blazor.WebGPU.Matrix.Tests.Float
{
    [TestClass]
    public class Vec2FloatTests
    {
        private const float EPSILON = 1e-6f;

        private void AssertEqualApproximately(Vec2 actual, Vec2 expected, float tolerance = EPSILON)
        {
            Assert.True(MathF.Abs(actual[0] - expected[0]) < tolerance,
                $"Expected {expected[0]} but got {actual[0]} at index 0");
            Assert.True(MathF.Abs(actual[1] - expected[1]) < tolerance,
                $"Expected {expected[1]} but got {actual[1]} at index 1");
        }

        private void AssertEqualApproximately(float actual, float expected, float tolerance = EPSILON)
        {
            Assert.True(MathF.Abs(actual - expected) < tolerance,
                $"Expected {expected} but got {actual}");
        }

        private void AssertArrayEqualApproximately(Vec3 actual, float[] expected, float tolerance = EPSILON)
        {
            Assert.True(MathF.Abs(actual[0] - expected[0]) < tolerance,
                $"Expected {expected[0]} but got {actual[0]} at index 0");
            Assert.True(MathF.Abs(actual[1] - expected[1]) < tolerance,
                $"Expected {expected[1]} but got {actual[1]} at index 1");
            Assert.True(MathF.Abs(actual[2] - expected[2]) < tolerance,
                $"Expected {expected[2]} but got {actual[2]} at index 2");
        }

        [TestMethod]
        public void Should_Add()
        {
            var a = Vec2.Create(1, 2);
            var b = Vec2.Create(2, 3);
            var expected = Vec2.Create(3, 5);

            var result = Vec2.Add(a, b);
            AssertEqualApproximately(result, expected);

            var dest = Vec2.Create();
            var result2 = Vec2.Add(a, b, dest);
            Assert.Same(dest, result2);
            AssertEqualApproximately(result2, expected);
        }

        [TestMethod]
        public void Should_Compute_Angle()
        {
            var tests = new[]
            {
                new { a = new float[] { 1, 0 }, b = new float[] { 0, 1 }, expected = MathF.PI / 2 },
                new { a = new float[] { 1, 0 }, b = new float[] { -1, 0 }, expected = MathF.PI },
                new { a = new float[] { 1, 0 }, b = new float[] { 1, 0 }, expected = 0f },
                new { a = new float[] { 1, 2 }, b = new float[] { 4, 5 }, expected = 0.2110933f },
            };

            foreach (var test in tests)
            {
                var a = Vec2.Create(test.a[0], test.a[1]);
                var b = Vec2.Create(test.b[0], test.b[1]);
                var result = Vec2.Angle(a, b);
                AssertEqualApproximately(result, test.expected, 1e-5f);

                Vec2.MulScalar(a, 1000, a);
                Vec2.MulScalar(b, 1000, b);
                result = Vec2.Angle(a, b);
                AssertEqualApproximately(result, test.expected, 1e-5f);
            }
        }

        [TestMethod]
        public void Should_Compute_Ceil()
        {
            var a = Vec2.Create(1.1f, -1.1f);
            var expected = Vec2.Create(2, -1);

            var result = Vec2.Ceil(a);
            AssertEqualApproximately(result, expected);

            var dest = Vec2.Create();
            var result2 = Vec2.Ceil(a, dest);
            Assert.Same(dest, result2);
            AssertEqualApproximately(result2, expected);
        }

        [TestMethod]
        public void Should_Compute_Floor()
        {
            var a = Vec2.Create(1.1f, -1.1f);
            var expected = Vec2.Create(1, -2);

            var result = Vec2.Floor(a);
            AssertEqualApproximately(result, expected);

            var dest = Vec2.Create();
            var result2 = Vec2.Floor(a, dest);
            Assert.Same(dest, result2);
            AssertEqualApproximately(result2, expected);
        }

        [TestMethod]
        public void Should_Compute_Round()
        {
            var a = Vec2.Create(1.1f, -1.1f);
            var expected = Vec2.Create(1, -1);

            var result = Vec2.Round(a);
            AssertEqualApproximately(result, expected);

            var dest = Vec2.Create();
            var result2 = Vec2.Round(a, dest);
            Assert.Same(dest, result2);
            AssertEqualApproximately(result2, expected);
        }

        [TestMethod]
        public void Should_Clamp()
        {
            // Test 1
            var a1 = Vec2.Create(2, -1);
            var expected1 = Vec2.Create(1, 0);

            var result1 = Vec2.Clamp(a1, 0, 1);
            AssertEqualApproximately(result1, expected1);

            var dest1 = Vec2.Create();
            var result1b = Vec2.Clamp(a1, 0, 1, dest1);
            Assert.Same(dest1, result1b);
            AssertEqualApproximately(result1b, expected1);

            // Test 2
            var a2 = Vec2.Create(-22, 50);
            var expected2 = Vec2.Create(-10, 5);

            var result2 = Vec2.Clamp(a2, -10, 5);
            AssertEqualApproximately(result2, expected2);

            var dest2 = Vec2.Create();
            var result2b = Vec2.Clamp(a2, -10, 5, dest2);
            Assert.Same(dest2, result2b);
            AssertEqualApproximately(result2b, expected2);
        }

        [TestMethod]
        public void Should_Equals_Approximately()
        {
            var a = Vec2.Create(2, 3);
            var b = Vec2.Create(2, 3);
            var c = Vec2.Create(2 + EPSILON * 0.5f, 3);
            var d = Vec2.Create(2.001f, 3);

            Assert.True(Vec2.EqualsApproximately(a, b));
            Assert.True(Vec2.EqualsApproximately(a, c));
            Assert.False(Vec2.EqualsApproximately(a, d));
        }

        [TestMethod]
        public void Should_Equals()
        {
            var a = Vec2.Create(2, 3);
            var b = Vec2.Create(2, 3);
            var c = Vec2.Create(2 + EPSILON * 0.5f, 3);

            Assert.True(Vec2.Equals(a, b));
            Assert.False(Vec2.Equals(a, c));
        }

        [TestMethod]
        public void Should_Subtract()
        {
            var a = Vec2.Create(2, 3);
            var b = Vec2.Create(4, 6);
            var expected = Vec2.Create(-2, -3);

            var result = Vec2.Subtract(a, b);
            AssertEqualApproximately(result, expected);

            var dest = Vec2.Create();
            var result2 = Vec2.Subtract(a, b, dest);
            Assert.Same(dest, result2);
            AssertEqualApproximately(result2, expected);
        }

        [TestMethod]
        public void Should_Sub()
        {
            var a = Vec2.Create(2, 3);
            var b = Vec2.Create(4, 6);
            var expected = Vec2.Create(-2, -3);

            var result = Vec2.Sub(a, b);
            AssertEqualApproximately(result, expected);

            var dest = Vec2.Create();
            var result2 = Vec2.Sub(a, b, dest);
            Assert.Same(dest, result2);
            AssertEqualApproximately(result2, expected);
        }

        [TestMethod]
        public void Should_Lerp()
        {
            var a = Vec2.Create(2, 3);
            var b = Vec2.Create(4, 6);
            var expected = Vec2.Create(3, 4.5f);

            var result = Vec2.Lerp(a, b, 0.5f);
            AssertEqualApproximately(result, expected);

            var dest = Vec2.Create();
            var result2 = Vec2.Lerp(a, b, 0.5f, dest);
            Assert.Same(dest, result2);
            AssertEqualApproximately(result2, expected);
        }

        [TestMethod]
        public void Should_Lerp_Under_0()
        {
            var a = Vec2.Create(1, 3);
            var b = Vec2.Create(2, 6);
            var expected = Vec2.Create(0.5f, 1.5f);

            var result = Vec2.Lerp(a, b, -0.5f);
            AssertEqualApproximately(result, expected);

            var dest = Vec2.Create();
            var result2 = Vec2.Lerp(a, b, -0.5f, dest);
            Assert.Same(dest, result2);
            AssertEqualApproximately(result2, expected);
        }

        [TestMethod]
        public void Should_Lerp_Over_0()
        {
            var a = Vec2.Create(1, 3);
            var b = Vec2.Create(2, 6);
            var expected = Vec2.Create(2.5f, 7.5f);

            var result = Vec2.Lerp(a, b, 1.5f);
            AssertEqualApproximately(result, expected);

            var dest = Vec2.Create();
            var result2 = Vec2.Lerp(a, b, 1.5f, dest);
            Assert.Same(dest, result2);
            AssertEqualApproximately(result2, expected);
        }

        [TestMethod]
        public void Should_Multiply_By_Scalar()
        {
            var a = Vec2.Create(2, 3);
            var expected = Vec2.Create(4, 6);

            var result = Vec2.MulScalar(a, 2);
            AssertEqualApproximately(result, expected);

            var dest = Vec2.Create();
            var result2 = Vec2.MulScalar(a, 2, dest);
            Assert.Same(dest, result2);
            AssertEqualApproximately(result2, expected);
        }

        [TestMethod]
        public void Should_Scale()
        {
            var a = Vec2.Create(2, 3);
            var expected = Vec2.Create(4, 6);

            var result = Vec2.Scale(a, 2);
            AssertEqualApproximately(result, expected);

            var dest = Vec2.Create();
            var result2 = Vec2.Scale(a, 2, dest);
            Assert.Same(dest, result2);
            AssertEqualApproximately(result2, expected);
        }

        [TestMethod]
        public void Should_Add_Scaled()
        {
            var a = Vec2.Create(2, 3);
            var b = Vec2.Create(4, 6);
            var expected = Vec2.Create(10, 15);

            var result = Vec2.AddScaled(a, b, 2);
            AssertEqualApproximately(result, expected);

            var dest = Vec2.Create();
            var result2 = Vec2.AddScaled(a, b, 2, dest);
            Assert.Same(dest, result2);
            AssertEqualApproximately(result2, expected);
        }

        [TestMethod]
        public void Should_Divide_By_Scalar()
        {
            var a = Vec2.Create(1, 3);
            var expected = Vec2.Create(0.5f, 1.5f);

            var result = Vec2.DivScalar(a, 2, null);
            AssertEqualApproximately(result, expected);

            var dest = Vec2.Create();
            var result2 = Vec2.DivScalar(a, 2, dest);
            Assert.Same(dest, result2);
            AssertEqualApproximately(result2, expected);
        }

        [TestMethod]
        public void Should_Inverse()
        {
            var a = Vec2.Create(3, -4);
            var expected = Vec2.Create(1f / 3f, 1f / -4f);

            var result = Vec2.Inverse(a);
            AssertEqualApproximately(result, expected);

            var dest = Vec2.Create();
            var result2 = Vec2.Inverse(a, dest);
            Assert.Same(dest, result2);
            AssertEqualApproximately(result2, expected);
        }

        [TestMethod]
        public void Should_Cross()
        {
            var a = Vec2.Create(2, 3);
            var b = Vec2.Create(4, 5);
            var expected = new float[] { 0, 0, 2 * 5 - 3 * 4 };

            var result = Vec2.Cross(a, b);
            AssertArrayEqualApproximately(result, expected);

            var dest = Vec3.Create();
            var result2 = Vec2.Cross(a, b, dest);
            Assert.Same(dest, result2);
            var expected2 = new float[] { 0, 0, 3 * 5 - 2 * 4 };
            AssertArrayEqualApproximately(result2, expected2);
        }

        [TestMethod]
        public void Should_Compute_Dot_Product()
        {
            var a = Vec2.Create(2, 3);
            var b = Vec2.Create(4, 6);
            var expected = 2 * 4 + 3 * 6;

            var result = Vec2.Dot(a, b);
            Assert.Equal(expected, result);
        }

        [TestMethod]
        public void Should_Compute_Length()
        {
            var a = Vec2.Create(2, 3);
            var expected = MathF.Sqrt(2 * 2 + 3 * 3);

            var result = Vec2.Length(a);
            Assert.Equal(expected, result);
        }

        [TestMethod]
        public void Should_Compute_Length_Squared()
        {
            var a = Vec2.Create(2, 3);
            var expected = 2 * 2 + 3 * 3;

            var result = Vec2.LengthSq(a);
            Assert.Equal(expected, result);
        }

        [TestMethod]
        public void Should_Compute_Len()
        {
            var a = Vec2.Create(2, 3);
            var expected = MathF.Sqrt(2 * 2 + 3 * 3);

            var result = Vec2.Len(a);
            Assert.Equal(expected, result);
        }

        [TestMethod]
        public void Should_Compute_Len_Sq()
        {
            var a = Vec2.Create(2, 3);
            var expected = 2 * 2 + 3 * 3;

            var result = Vec2.LenSq(a);
            Assert.Equal(expected, result);
        }

        [TestMethod]
        public void Should_Compute_Distance()
        {
            var a = Vec2.Create(2, 3);
            var b = Vec2.Create(5, 7);
            var expected = MathF.Sqrt(3 * 3 + 4 * 4);

            var result = Vec2.Distance(a, b);
            Assert.Equal(expected, result);
        }

        [TestMethod]
        public void Should_Compute_Distance_Squared()
        {
            var a = Vec2.Create(2, 3);
            var b = Vec2.Create(5, 7);
            var expected = 3 * 3 + 4 * 4;

            var result = Vec2.DistanceSq(a, b);
            Assert.Equal(expected, result);
        }

        [TestMethod]
        public void Should_Compute_Dist()
        {
            var a = Vec2.Create(2, 3);
            var b = Vec2.Create(5, 7);
            var expected = MathF.Sqrt(3 * 3 + 4 * 4);

            var result = Vec2.Dist(a, b);
            Assert.Equal(expected, result);
        }

        [TestMethod]
        public void Should_Compute_Dist_Squared()
        {
            var a = Vec2.Create(2, 3);
            var b = Vec2.Create(5, 7);
            var expected = 3 * 3 + 4 * 4;

            var result = Vec2.DistSq(a, b);
            Assert.Equal(expected, result);
        }

        [TestMethod]
        public void Should_Normalize()
        {
            var a = Vec2.Create(2, 3);
            var length = MathF.Sqrt(2 * 2 + 3 * 3);
            var expected = Vec2.Create(2 / length, 3 / length);

            var result = Vec2.Normalize(a);
            AssertEqualApproximately(result, expected);

            var dest = Vec2.Create();
            var result2 = Vec2.Normalize(a, dest);
            Assert.Same(dest, result2);
            AssertEqualApproximately(result2, expected);
        }

        [TestMethod]
        public void Should_Negate()
        {
            var a = Vec2.Create(2, -3);
            var expected = Vec2.Create(-2, 3);

            var result = Vec2.Negate(a);
            AssertEqualApproximately(result, expected);

            var dest = Vec2.Create();
            var result2 = Vec2.Negate(a, dest);
            Assert.Same(dest, result2);
            AssertEqualApproximately(result2, expected);
        }

        [TestMethod]
        public void Should_Copy()
        {
            var a = Vec2.Create(2, 3);
            var expected = Vec2.Create(2, 3);

            var result = Vec2.Copy(a);
            Assert.NotSame(a, result);
            AssertEqualApproximately(result, expected);

            var dest = Vec2.Create();
            var result2 = Vec2.Copy(a, dest);
            Assert.Same(dest, result2);
            AssertEqualApproximately(result2, expected);
        }

        [TestMethod]
        public void Should_Clone()
        {
            var a = Vec2.Create(2, 3);
            var expected = Vec2.Create(2, 3);

            var result = Vec2.Clone(a);
            Assert.NotSame(a, result);
            AssertEqualApproximately(result, expected);

            var dest = Vec2.Create();
            var result2 = Vec2.Clone(a, dest);
            Assert.Same(dest, result2);
            AssertEqualApproximately(result2, expected);
        }

        [TestMethod]
        public void Should_Set()
        {
            var expected = Vec2.Create(2, 3);

            var result = Vec2.Set(2, 3);
            AssertEqualApproximately(result, expected);

            var dest = Vec2.Create();
            var result2 = Vec2.Set(2, 3, dest);
            Assert.Same(dest, result2);
            AssertEqualApproximately(result2, expected);
        }

        [TestMethod]
        public void Should_Multiply()
        {
            var a = Vec2.Create(2, 3);
            var b = Vec2.Create(4, 6);
            var expected = Vec2.Create(8, 18);

            var result = Vec2.Multiply(a, b);
            AssertEqualApproximately(result, expected);

            var dest = Vec2.Create();
            var result2 = Vec2.Multiply(a, b, dest);
            Assert.Same(dest, result2);
            AssertEqualApproximately(result2, expected);
        }

        [TestMethod]
        public void Should_Mul()
        {
            var a = Vec2.Create(2, 3);
            var b = Vec2.Create(4, 6);
            var expected = Vec2.Create(8, 18);

            var result = Vec2.Mul(a, b);
            AssertEqualApproximately(result, expected);

            var dest = Vec2.Create();
            var result2 = Vec2.Mul(a, b, dest);
            Assert.Same(dest, result2);
            AssertEqualApproximately(result2, expected);
        }

        [TestMethod]
        public void Should_Divide()
        {
            var a = Vec2.Create(2, 3);
            var b = Vec2.Create(3, 4);
            var expected = Vec2.Create(2f / 3f, 3f / 4f);

            var result = Vec2.Divide(a, b);
            AssertEqualApproximately(result, expected);

            var dest = Vec2.Create();
            var result2 = Vec2.Divide(a, b, dest);
            Assert.Same(dest, result2);
            AssertEqualApproximately(result2, expected);
        }

        [TestMethod]
        public void Should_Div()
        {
            var a = Vec2.Create(2, 3);
            var b = Vec2.Create(3, 4);
            var expected = Vec2.Create(2f / 3f, 3f / 4f);

            var result = Vec2.Div(a, b);
            AssertEqualApproximately(result, expected);

            var dest = Vec2.Create();
            var result2 = Vec2.Div(a, b, dest);
            Assert.Same(dest, result2);
            AssertEqualApproximately(result2, expected);
        }

        [TestMethod]
        public void Should_From_Values()
        {
            var expected = Vec2.Create(2, 3);
            var result = Vec2.FromValues(2, 3);
            AssertEqualApproximately(result, expected);
        }

        //[TestMethod]
        //public void Should_Transform_By_3x3()
        //{
        //    var a = Vec2.Create(2, 3);
        //    // Assuming Mat3 exists and can be created with array
        //    var m = new Mat3(new float[] {
        //        4, 0, 0, 11,
        //        0, 5, 0, 12,
        //        8, 2, 0, 13,
        //    });
        //    var expected = Vec2.Create(16, 17);

        //    var result = Vec2.TransformMat3(a, m);
        //    AssertEqualApproximately(result, expected, 1e-5f);

        //    var dest = Vec2.Create();
        //    var result2 = Vec2.TransformMat3(a, m, dest);
        //    Assert.Same(dest, result2);
        //    AssertEqualApproximately(result2, expected, 1e-5f);
        //}

        //[TestMethod]
        //public void Should_Transform_By_4x4()
        //{
        //    var a = Vec2.Create(2, 3);
        //    // Assuming Mat4 exists and can be created with array
        //    var m = new Mat4(new float[] {
        //        1, 0, 0, 4,
        //        0, 2, 0, 5,
        //        0, 0, 3, 6,
        //        0, 0, 0, 1
        //    });
        //    var expected = Vec2.Create(6, 11);

        //    var result = Vec2.TransformMat4(a, m);
        //    AssertEqualApproximately(result, expected);

        //    var dest = Vec2.Create();
        //    var result2 = Vec2.TransformMat4(a, m, dest);
        //    Assert.Same(dest, result2);
        //    AssertEqualApproximately(result2, expected);
        //}

        [TestMethod]
        public void Should_Zero()
        {
            var result = Vec2.Zero();
            Assert.Equal(0f, result[0]);
            Assert.Equal(0f, result[1]);

            var a = Vec2.Create(2, 3);
            var result2 = Vec2.Zero(a);
            Assert.Same(a, result2);
            Assert.Equal(0f, a[0]);
            Assert.Equal(0f, a[1]);
        }

        [TestMethod]
        public void Should_Rotate()
        {
            // Rotation around world origin [0, 0]
            var a = Vec2.Create(0, 1);
            var b = Vec2.Create(0, 0);
            var expected = Vec2.Create(0, -1);

            var result = Vec2.Rotate(a, b, MathF.PI);
            AssertEqualApproximately(result, expected, 1e-5f);

            var dest = Vec2.Create();
            var result2 = Vec2.Rotate(a, b, MathF.PI, dest);
            Assert.Same(dest, result2);
            AssertEqualApproximately(result2, expected, 1e-5f);

            // Rotation around an arbitrary origin
            var a2 = Vec2.Create(6, -5);
            var b2 = Vec2.Create(0, -5);
            var expected2 = Vec2.Create(-6, -5);

            var result3 = Vec2.Rotate(a2, b2, MathF.PI);
            AssertEqualApproximately(result3, expected2, 1e-5f);

            var dest2 = Vec2.Create();
            var result4 = Vec2.Rotate(a2, b2, MathF.PI, dest2);
            Assert.Same(dest2, result4);
            AssertEqualApproximately(result4, expected2, 1e-5f);
        }

        [TestMethod]
        public void Should_Set_Length()
        {
            var a = Vec2.Create(1, 1);
            var expected = Vec2.Create(10.323759005323593f, 10.323759005323593f);

            var result = Vec2.SetLength(a, 14.6f);
            AssertEqualApproximately(result, expected, 1e-5f);

            var dest = Vec2.Create();
            var result2 = Vec2.SetLength(a, 14.6f, dest);
            Assert.Same(dest, result2);
            AssertEqualApproximately(result2, expected, 1e-5f);
        }

        [TestMethod]
        public void Should_Truncate()
        {
            // Should shorten the vector
            var a = Vec2.Create(10.323759005323593f, 10.323759005323593f);
            var expected = Vec2.Create(2.82842712474619f, 2.82842712474619f);

            var result = Vec2.Truncate(a, 4.0f);
            AssertEqualApproximately(result, expected, 1e-5f);

            var dest = Vec2.Create();
            var result2 = Vec2.Truncate(a, 4.0f, dest);
            Assert.Same(dest, result2);
            AssertEqualApproximately(result2, expected, 1e-5f);

            // Should preserve the vector when shorter than maxLen
            var a2 = Vec2.Create(11, 12);
            var expected2 = Vec2.Create(11, 12);

            var result3 = Vec2.Truncate(a2, 14.6f);
            AssertEqualApproximately(result3, expected2);

            var dest2 = Vec2.Create();
            var result4 = Vec2.Truncate(a2, 14.6f, dest2);
            Assert.Same(dest2, result4);
            AssertEqualApproximately(result4, expected2);
        }

        [TestMethod]
        public void Should_Midpoint()
        {
            // Should return the midpoint
            var a = Vec2.Create(0, 0);
            var b = Vec2.Create(10, 10);
            var expected = Vec2.Create(5, 5);

            var result = Vec2.Midpoint(a, b);
            AssertEqualApproximately(result, expected);

            var dest = Vec2.Create();
            var result2 = Vec2.Midpoint(a, b, dest);
            Assert.Same(dest, result2);
            AssertEqualApproximately(result2, expected);

            // Should handle negatives
            var a2 = Vec2.Create(-10, -20);
            var b2 = Vec2.Create(30, 40);
            var expected2 = Vec2.Create(10, 10);

            var result3 = Vec2.Midpoint(a2, b2);
            AssertEqualApproximately(result3, expected2);

            var dest2 = Vec2.Create();
            var result4 = Vec2.Midpoint(a2, b2, dest2);
            Assert.Same(dest2, result4);
            AssertEqualApproximately(result4, expected2);
        }
    }
}
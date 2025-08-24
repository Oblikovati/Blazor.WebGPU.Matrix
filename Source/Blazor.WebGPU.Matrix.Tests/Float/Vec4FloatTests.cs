using SpawnDev.Blazor.UnitTesting;

namespace Blazor.WebGPU.Matrix.Tests.Float;

[TestClass]
public class Vec4FloatTests
{
    private const float EPSILON = 0.000001f;

    private void AssertEqualApproximately(Vec4 a, Vec4 b)
    {
        Assert.True(MathF.Abs(a[0] - b[0]) < EPSILON);
        Assert.True(MathF.Abs(a[1] - b[1]) < EPSILON);
        Assert.True(MathF.Abs(a[2] - b[2]) < EPSILON);
        Assert.True(MathF.Abs(a[3] - b[3]) < EPSILON);
    }

    private void AssertEqual(Vec4 a, Vec4 b)
    {
        Assert.Equal(a[0], b[0]);
        Assert.Equal(a[1], b[1]);
        Assert.Equal(a[2], b[2]);
        Assert.Equal(a[3], b[3]);
    }

    [Fact]
    public void ShouldAdd()
    {
        var a = Vec4.Create(1, 2, 3, 4);
        var b = Vec4.Create(2, 3, 4, 5);
        var expected = Vec4.Create(3, 5, 7, 9);

        // Test without destination
        var result1 = Vec4.Add(a, b);
        AssertEqual(result1, expected);

        // Test with destination
        var dst = Vec4.Create();
        var result2 = Vec4.Add(a, b, dst);
        Assert.Same(dst, result2);
        AssertEqual(result2, expected);
    }

    [Fact]
    public void ShouldComputeCeil()
    {
        var input = Vec4.Create(1.1f, -1.1f, 2.9f, -4.2f);
        var expected = Vec4.Create(2, -1, 3, -4);

        // Test without destination
        var result1 = Vec4.Ceil(input);
        AssertEqual(result1, expected);

        // Test with destination
        var dst = Vec4.Create();
        var result2 = Vec4.Ceil(input, dst);
        Assert.Same(dst, result2);
        AssertEqual(result2, expected);
    }

    [Fact]
    public void ShouldComputeFloor()
    {
        var input = Vec4.Create(1.1f, -1.1f, 2.9f, -3.1f);
        var expected = Vec4.Create(1, -2, 2, -4);

        // Test without destination
        var result1 = Vec4.Floor(input);
        AssertEqual(result1, expected);

        // Test with destination
        var dst = Vec4.Create();
        var result2 = Vec4.Floor(input, dst);
        Assert.Same(dst, result2);
        AssertEqual(result2, expected);
    }

    [Fact]
    public void ShouldComputeRound()
    {
        var input = Vec4.Create(1.1f, -1.1f, 2.9f, 0.1f);
        var expected = Vec4.Create(1, -1, 3, 0);

        // Test without destination
        var result1 = Vec4.Round(input);
        AssertEqual(result1, expected);

        // Test with destination
        var dst = Vec4.Create();
        var result2 = Vec4.Round(input, dst);
        Assert.Same(dst, result2);
        AssertEqual(result2, expected);
    }

    [Fact]
    public void ShouldClamp()
    {
        // Test case 1
        var input1 = Vec4.Create(2, -1, 0.5f, -4);
        var expected1 = Vec4.Create(1, 0, 0.5f, 0);

        // Test without destination
        var result1a = Vec4.Clamp(input1, 0, 1);
        AssertEqual(result1a, expected1);

        // Test with destination
        var dst1 = Vec4.Create();
        var result1b = Vec4.Clamp(input1, 0, 1, dst1);
        Assert.Same(dst1, result1b);
        AssertEqual(result1b, expected1);

        // Test case 2
        var input2 = Vec4.Create(-22, 50, 2.9f, -9);
        var expected2 = Vec4.Create(-10, 5, 2.9f, -9);

        // Test without destination
        var result2a = Vec4.Clamp(input2, -10, 5);
        AssertEqual(result2a, expected2);

        // Test with destination
        var dst2 = Vec4.Create();
        var result2b = Vec4.Clamp(input2, -10, 5, dst2);
        Assert.Same(dst2, result2b);
        AssertEqual(result2b, expected2);
    }

    [Fact]
    public void ShouldEqualsApproximately()
    {
        var a = Vec4.Create(1, 2, 3, 4);
        var b = Vec4.Create(1, 2, 3, 4);
        var c = Vec4.Create(1 + EPSILON * 0.5f, 2, 3, 4);
        var d = Vec4.Create(1, 2 + EPSILON * 0.5f, 3, 4);
        var e = Vec4.Create(1, 2, 3 + EPSILON * 0.5f, 4);
        var f = Vec4.Create(1, 2, 3, 4 + EPSILON * 0.5f);
        var g = Vec4.Create(1.0001f, 2, 3, 4);
        var h = Vec4.Create(1, 2.0001f, 3, 4);
        var i = Vec4.Create(1, 2, 3.0001f, 4);
        var j = Vec4.Create(1, 2, 3, 4.0001f);

        Assert.True(Vec4.EqualsApproximately(a, b));
        Assert.True(Vec4.EqualsApproximately(a, c));
        Assert.True(Vec4.EqualsApproximately(a, d));
        Assert.True(Vec4.EqualsApproximately(a, e));
        Assert.True(Vec4.EqualsApproximately(a, f));
        Assert.False(Vec4.EqualsApproximately(a, g));
        Assert.False(Vec4.EqualsApproximately(a, h));
        Assert.False(Vec4.EqualsApproximately(a, i));
        Assert.False(Vec4.EqualsApproximately(a, j));
    }

    [Fact]
    public void ShouldEquals()
    {
        var a = Vec4.Create(1, 2, 3, 4);
        var b = Vec4.Create(1, 2, 3, 4);
        var c = Vec4.Create(1 + EPSILON * 0.5f, 2, 3, 4);
        var d = Vec4.Create(1, 2 + EPSILON * 0.5f, 3, 4);
        var e = Vec4.Create(1, 2, 3 + EPSILON * 0.5f, 4);
        var f = Vec4.Create(1, 2, 3, 4 + EPSILON * 0.5f);

        Assert.True(Vec4.Equals(a, b));
        Assert.False(Vec4.Equals(a, c));
        Assert.False(Vec4.Equals(a, d));
        Assert.False(Vec4.Equals(a, e));
        Assert.False(Vec4.Equals(a, f));
    }

    [Fact]
    public void ShouldSubtract()
    {
        var a = Vec4.Create(1, 2, 3, 4);
        var b = Vec4.Create(2, 4, 6, 8);
        var expected = Vec4.Create(-1, -2, -3, -4);

        // Test without destination
        var result1 = Vec4.Subtract(a, b);
        AssertEqual(result1, expected);

        // Test with destination
        var dst = Vec4.Create();
        var result2 = Vec4.Subtract(a, b, dst);
        Assert.Same(dst, result2);
        AssertEqual(result2, expected);
    }

    [Fact]
    public void ShouldSub()
    {
        var a = Vec4.Create(1, 2, 3, 4);
        var b = Vec4.Create(2, 4, 6, 8);
        var expected = Vec4.Create(-1, -2, -3, -4);

        // Test without destination
        var result1 = Vec4.Sub(a, b);
        AssertEqual(result1, expected);

        // Test with destination
        var dst = Vec4.Create();
        var result2 = Vec4.Sub(a, b, dst);
        Assert.Same(dst, result2);
        AssertEqual(result2, expected);
    }

    [Fact]
    public void ShouldLerp()
    {
        var a = Vec4.Create(1, 2, 3, 4);
        var b = Vec4.Create(2, 4, 6, 8);
        var expected = Vec4.Create(1.5f, 3, 4.5f, 6);

        // Test without destination
        var result1 = Vec4.Lerp(a, b, 0.5f);
        AssertEqual(result1, expected);

        // Test with destination
        var dst = Vec4.Create();
        var result2 = Vec4.Lerp(a, b, 0.5f, dst);
        Assert.Same(dst, result2);
        AssertEqual(result2, expected);
    }

    [Fact]
    public void ShouldLerpUnder0()
    {
        var a = Vec4.Create(1, 2, 3, 4);
        var b = Vec4.Create(2, 4, 6, 8);
        var expected = Vec4.Create(0.5f, 1, 1.5f, 2);

        // Test without destination
        var result1 = Vec4.Lerp(a, b, -0.5f);
        AssertEqual(result1, expected);

        // Test with destination
        var dst = Vec4.Create();
        var result2 = Vec4.Lerp(a, b, -0.5f, dst);
        Assert.Same(dst, result2);
        AssertEqual(result2, expected);
    }

    [Fact]
    public void ShouldLerpOver0()
    {
        var a = Vec4.Create(1, 2, 3, 4);
        var b = Vec4.Create(2, 4, 6, 8);
        var expected = Vec4.Create(2.5f, 5, 7.5f, 10);

        // Test without destination
        var result1 = Vec4.Lerp(a, b, 1.5f);
        AssertEqual(result1, expected);

        // Test with destination
        var dst = Vec4.Create();
        var result2 = Vec4.Lerp(a, b, 1.5f, dst);
        Assert.Same(dst, result2);
        AssertEqual(result2, expected);
    }

    [Fact]
    public void ShouldMultiplyByScalar()
    {
        var a = Vec4.Create(1, 2, 3, 4);
        var expected = Vec4.Create(2, 4, 6, 8);

        // Test without destination
        var result1 = Vec4.MulScalar(a, 2);
        AssertEqual(result1, expected);

        // Test with destination
        var dst = Vec4.Create();
        var result2 = Vec4.MulScalar(a, 2, dst);
        Assert.Same(dst, result2);
        AssertEqual(result2, expected);
    }

    [Fact]
    public void ShouldScale()
    {
        var a = Vec4.Create(1, 2, 3, 4);
        var expected = Vec4.Create(2, 4, 6, 8);

        // Test without destination
        var result1 = Vec4.Scale(a, 2);
        AssertEqual(result1, expected);

        // Test with destination
        var dst = Vec4.Create();
        var result2 = Vec4.Scale(a, 2, dst);
        Assert.Same(dst, result2);
        AssertEqual(result2, expected);
    }

    [Fact]
    public void ShouldAddScaled()
    {
        var a = Vec4.Create(1, 2, 3, 4);
        var b = Vec4.Create(2, 4, 6, 8);
        var expected = Vec4.Create(5, 10, 15, 20);

        // Test without destination
        var result1 = Vec4.AddScaled(a, b, 2);
        AssertEqual(result1, expected);

        // Test with destination
        var dst = Vec4.Create();
        var result2 = Vec4.AddScaled(a, b, 2, dst);
        Assert.Same(dst, result2);
        AssertEqual(result2, expected);
    }

    [Fact]
    public void ShouldDivideByScalar()
    {
        var a = Vec4.Create(1, 2, 3, 4);
        var expected = Vec4.Create(0.5f, 1, 1.5f, 2);

        // Test without destination
        var result1 = Vec4.DivScalar(a, 2);
        AssertEqual(result1, expected);

        // Test with destination
        var dst = Vec4.Create();
        var result2 = Vec4.DivScalar(a, 2, dst);
        Assert.Same(dst, result2);
        AssertEqual(result2, expected);
    }

    [Fact]
    public void ShouldInverse()
    {
        var a = Vec4.Create(2, 3, -4, -8);
        var expected = Vec4.Create(1f / 2f, 1f / 3f, 1f / -4f, 1f / -8f);

        // Test without destination
        var result1 = Vec4.Inverse(a);
        AssertEqual(result1, expected);

        // Test with destination
        var dst = Vec4.Create();
        var result2 = Vec4.Inverse(a, dst);
        Assert.Same(dst, result2);
        AssertEqual(result2, expected);
    }

    [Fact]
    public void ShouldComputeDotProduct()
    {
        var a = Vec4.Create(1, 2, 3, 4);
        var b = Vec4.Create(2, 4, 6, 8);
        var expected = 1 * 2 + 2 * 4 + 3 * 6 + 4 * 8;

        var result = Vec4.Dot(a, b);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ShouldComputeLength()
    {
        var a = Vec4.Create(1, 2, 3, 4);
        var expected = MathF.Sqrt(1 * 1 + 2 * 2 + 3 * 3 + 4 * 4);

        var result = Vec4.Length(a);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ShouldComputeLengthSquared()
    {
        var a = Vec4.Create(1, 2, 3, 4);
        var expected = 1 * 1 + 2 * 2 + 3 * 3 + 4 * 4;

        var result = Vec4.LengthSq(a);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ShouldComputeLen()
    {
        var a = Vec4.Create(1, 2, 3, 4);
        var expected = MathF.Sqrt(1 * 1 + 2 * 2 + 3 * 3 + 4 * 4);

        var result = Vec4.Len(a);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ShouldComputeLenSq()
    {
        var a = Vec4.Create(1, 2, 3, 4);
        var expected = 1 * 1 + 2 * 2 + 3 * 3 + 4 * 4;

        var result = Vec4.LenSq(a);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ShouldComputeDistance()
    {
        var a = Vec4.Create(1, 2, 3, 4);
        var b = Vec4.Create(3, 5, 7, 9);
        var expected = MathF.Sqrt(2 * 2 + 3 * 3 + 4 * 4 + 5 * 5);

        var result = Vec4.Distance(a, b);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ShouldComputeDistanceSquared()
    {
        var a = Vec4.Create(1, 2, 3, 4);
        var b = Vec4.Create(3, 5, 7, 9);
        var expected = 2 * 2 + 3 * 3 + 4 * 4 + 5 * 5;

        var result = Vec4.DistanceSq(a, b);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ShouldComputeDist()
    {
        var a = Vec4.Create(1, 2, 3, 4);
        var b = Vec4.Create(3, 5, 7, 9);
        var expected = MathF.Sqrt(2 * 2 + 3 * 3 + 4 * 4 + 5 * 5);

        var result = Vec4.Dist(a, b);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ShouldComputeDistSquared()
    {
        var a = Vec4.Create(1, 2, 3, 4);
        var b = Vec4.Create(3, 5, 7, 9);
        var expected = 2 * 2 + 3 * 3 + 4 * 4 + 5 * 5;

        var result = Vec4.DistSq(a, b);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ShouldNormalize()
    {
        var a = Vec4.Create(1, 2, 3, 4);
        var length = MathF.Sqrt(1 * 1 + 2 * 2 + 3 * 3 + 4 * 4);
        var expected = Vec4.Create(1 / length, 2 / length, 3 / length, 4 / length);

        // Test without destination
        var result1 = Vec4.Normalize(a);
        AssertEqualApproximately(result1, expected);

        // Test with destination
        var dst = Vec4.Create();
        var result2 = Vec4.Normalize(a, dst);
        Assert.Same(dst, result2);
        AssertEqualApproximately(result2, expected);
    }

    [Fact]
    public void ShouldNegate()
    {
        var a = Vec4.Create(1, 2, 3, -4);
        var expected = Vec4.Create(-1, -2, -3, 4);

        // Test without destination
        var result1 = Vec4.Negate(a);
        AssertEqual(result1, expected);

        // Test with destination
        var dst = Vec4.Create();
        var result2 = Vec4.Negate(a, dst);
        Assert.Same(dst, result2);
        AssertEqual(result2, expected);
    }

    [Fact]
    public void ShouldCopy()
    {
        var a = Vec4.Create(1, 2, 3, 4);
        var expected = Vec4.Create(1, 2, 3, 4);

        // Test without destination
        var result1 = Vec4.Copy(a);
        Assert.NotSame(a, result1);
        AssertEqual(result1, expected);

        // Test with destination
        var dst = Vec4.Create();
        var result2 = Vec4.Copy(a, dst);
        Assert.Same(dst, result2);
        AssertEqual(result2, expected);
    }

    [Fact]
    public void ShouldClone()
    {
        var a = Vec4.Create(1, 2, 3, 4);
        var expected = Vec4.Create(1, 2, 3, 4);

        // Test without destination
        var result1 = Vec4.Clone(a);
        Assert.NotSame(a, result1);
        AssertEqual(result1, expected);

        // Test with destination
        var dst = Vec4.Create();
        var result2 = Vec4.Clone(a, dst);
        Assert.Same(dst, result2);
        AssertEqual(result2, expected);
    }

    [Fact]
    public void ShouldSet()
    {
        var expected = Vec4.Create(2, 3, 4, 5);

        // Test without destination
        var result1 = Vec4.Set(2, 3, 4, 5);
        AssertEqual(result1, expected);

        // Test with destination
        var dst = Vec4.Create();
        var result2 = Vec4.Set(2, 3, 4, 5, dst);
        Assert.Same(dst, result2);
        AssertEqual(result2, expected);
    }

    [Fact]
    public void ShouldMultiply()
    {
        var a = Vec4.Create(1, 2, 3, 4);
        var b = Vec4.Create(2, 4, 6, 8);
        var expected = Vec4.Create(2, 8, 18, 32);

        // Test without destination
        var result1 = Vec4.Multiply(a, b);
        AssertEqual(result1, expected);

        // Test with destination
        var dst = Vec4.Create();
        var result2 = Vec4.Multiply(a, b, dst);
        Assert.Same(dst, result2);
        AssertEqual(result2, expected);
    }

    [Fact]
    public void ShouldMul()
    {
        var a = Vec4.Create(1, 2, 3, 4);
        var b = Vec4.Create(2, 4, 6, 8);
        var expected = Vec4.Create(2, 8, 18, 32);

        // Test without destination
        var result1 = Vec4.Mul(a, b);
        AssertEqual(result1, expected);

        // Test with destination
        var dst = Vec4.Create();
        var result2 = Vec4.Mul(a, b, dst);
        Assert.Same(dst, result2);
        AssertEqual(result2, expected);
    }

    [Fact]
    public void ShouldDivide()
    {
        var a = Vec4.Create(1, 2, 3, 4);
        var b = Vec4.Create(2, 3, 4, 5);
        var expected = Vec4.Create(1f / 2f, 2f / 3f, 3f / 4f, 4f / 5f);

        // Test without destination
        var result1 = Vec4.Divide(a, b);
        AssertEqualApproximately(result1, expected);

        // Test with destination
        var dst = Vec4.Create();
        var result2 = Vec4.Divide(a, b, dst);
        Assert.Same(dst, result2);
        AssertEqualApproximately(result2, expected);
    }

    [Fact]
    public void ShouldDiv()
    {
        var a = Vec4.Create(1, 2, 3, 4);
        var b = Vec4.Create(2, 3, 4, 5);
        var expected = Vec4.Create(1f / 2f, 2f / 3f, 3f / 4f, 4f / 5f);

        // Test without destination
        var result1 = Vec4.Div(a, b);
        AssertEqualApproximately(result1, expected);

        // Test with destination
        var dst = Vec4.Create();
        var result2 = Vec4.Div(a, b, dst);
        Assert.Same(dst, result2);
        AssertEqualApproximately(result2, expected);
    }

    [Fact]
    public void ShouldFromValues()
    {
        var expected = Vec4.Create(1, 2, 3, 4);
        var result = Vec4.FromValues(1, 2, 3, 4);
        AssertEqual(result, expected);
    }

    //[Fact]
    //public void ShouldTransformByMat4()
    //{
    //    var v = Vec4.Create(1, 2, 3, 4);
    //    // Create a 4x4 matrix
    //    var m = new float[] {
    //        1, 0, 0, 0,
    //        0, 2, 0, 0,
    //        0, 0, 3, 0,
    //        4, 5, 6, 1
    //    };
    //    var expected = Vec4.Create(17, 24, 33, 4);

    //    // Test without destination
    //    var result1 = Vec4.TransformMat4(v, m);
    //    AssertEqual(result1, expected);

    //    // Test with destination
    //    var dst = Vec4.Create();
    //    var result2 = Vec4.TransformMat4(v, m, dst);
    //    Assert.Same(dst, result2);
    //    AssertEqual(result2, expected);
    //}

    [Fact]
    public void ShouldZero()
    {
        var v = Vec4.Zero();
        AssertEqual(v, Vec4.Create(0, 0, 0, 0));

        var v2 = Vec4.Create(1, 2, 3, 4);
        var vn = Vec4.Zero(v2);
        Assert.Same(v2, vn);
        AssertEqual(v2, Vec4.Create(0, 0, 0, 0));
    }

    [Fact]
    public void ShouldSetLength()
    {
        var a = Vec4.Create(1, 1, 1, 1);
        var len = 14.6f;
        var expected = Vec4.Create(7.3f, 7.3f, 7.3f, 7.3f);

        // Test without destination
        var result1 = Vec4.SetLength(a, len);
        AssertEqualApproximately(result1, expected);

        // Test with destination
        var dst = Vec4.Create();
        var result2 = Vec4.SetLength(a, len, dst);
        Assert.Same(dst, result2);
        AssertEqualApproximately(result2, expected);
    }

    [Fact]
    public void ShouldTruncate_ShortenWhenTooLong()
    {
        var a = Vec4.Create(20, 30, 40, 50);
        var maxLen = 10f;
        var expected = Vec4.Create(
            2.721655269759087f,
            4.082483f, // Approximation for Float32Array precision
            5.443310539518174f,
            6.804138174397716f
        );

        // Test without destination
        var result1 = Vec4.Truncate(a, maxLen);
        AssertEqualApproximately(result1, expected);

        // Test with destination
        var dst = Vec4.Create();
        var result2 = Vec4.Truncate(a, maxLen, dst);
        Assert.Same(dst, result2);
        AssertEqualApproximately(result2, expected);
    }

    [Fact]
    public void ShouldTruncate_PreserveWhenShorterThanMaxLen()
    {
        var a = Vec4.Create(20, 30, 40, 50);
        var maxLen = 100f;
        var expected = Vec4.Create(20, 30, 40, 50);

        // Test without destination
        var result1 = Vec4.Truncate(a, maxLen);
        AssertEqual(result1, expected);

        // Test with destination
        var dst = Vec4.Create();
        var result2 = Vec4.Truncate(a, maxLen, dst);
        Assert.Same(dst, result2);
        AssertEqual(result2, expected);
    }

    [Fact]
    public void ShouldMidpoint()
    {
        var a = Vec4.Create(1, 2, 3, 4);
        var b = Vec4.Create(11, 22, 33, 44);
        var expected = Vec4.Create(6, 12, 18, 24);

        // Test without destination
        var result1 = Vec4.Midpoint(a, b);
        AssertEqual(result1, expected);

        // Test with destination
        var dst = Vec4.Create();
        var result2 = Vec4.Midpoint(a, b, dst);
        Assert.Same(dst, result2);
        AssertEqual(result2, expected);
    }

    [Fact]
    public void ShouldMidpoint_HandleNegatives()
    {
        var a = Vec4.Create(1, 2, 3, 4);
        var b = Vec4.Create(-11, -22, -33, -44);
        var expected = Vec4.Create(-5, -10, -15, -20);

        // Test without destination
        var result1 = Vec4.Midpoint(a, b);
        AssertEqual(result1, expected);

        // Test with destination
        var dst = Vec4.Create();
        var result2 = Vec4.Midpoint(a, b, dst);
        Assert.Same(dst, result2);
        AssertEqual(result2, expected);
    }
}
# Blazor.WebGPU.Matrix

## Fast 3D Math library for WebGPU

* [NuGet Package][nuget]

This is a port of WGPU-Matrix API Version 3.x which can be found here:

* [WGPU-Matrix Website](https://wgpu-matrix.org/)
* [WGPU-Matrix GitHub](https://github.com/greggman/wgpu-matrix)

We tried to keep the API as close as possible but there are some notable differences:

- We adopt C# name conventions: PascalCase for types and methods.
- The javascript arrays (Float32Array, Float64Array) are available over the property {Type}.Array and should be considered transients.
- When creating a Type providing a Float32Array, Float64Array the elements are copied and no reference is kept to the original array.
- The library depends on [SpawnDev.BlazorJS].
- This library only supports Blazor runtime.

## Why another 3D math library?

* Most other 3D math libraries are designed for WebGL, not WebGPU
  * WebGPU uses clip space Z 0 to 1, vs WebGL -1 to 1. So `ortho`, `perspective`, `frustum` are different
  * WebGPU mat3s are 12 floats (padded), WebGL they're 9.
* Many other 3D math libraries are overly verbose
  * compare

    ```csharp
    // Blazor.WebGPU.Matrix
    var t = Mat4.Translation([x, y, z]);
    var p = Mat4.Perspective(fov, aspect, near, far);
    var r = Mat4.RotationX(rad);
    ```

    ```js
    // gl-matrix
    const t = mat4.create();
    mat4.fromTranslation(t, [x, y, z]);

    const p = mat4.create();
    mat4.perspective(p, fov, aspect, near, far);

    const r = mat4.create();
    mat4.fromXRotation(r, rad);
    ```

    Note that if you want to pre-create matrices you can still do this in Blazor.WebGPU.Matrix

    ```csharp
    var t = Mat4.Create();
    Mat4.Translation([x, y, z], t);

    var p = Mat4.Create();
    Mat4.Perspective(fov, aspect, near, far, p);

    var r = Mat4.Create();
    Mat4.RotationX(rad, r);
    ```

## Usage

Since this library depends on [SpawnDev.BlazorJS], you must first setup it into your blazor project.
Follow their own documentation for that.

```csharp

using Blazor.WebGPU.Matrix;

var fov = 60 * MathF.PI / 180
var aspect = width / height;
var near = 0.1;
var far = 1000;
var perspective = Mat4.Perspective(fov, aspect, near, far);

var eye = [3, 5, 10];
var target = [0, 4, 0];
var up = [0, 1, 0];
var view = Mat4.LookAt(eye, target, up);
```

Note: for translation, rotation, and scaling there are 2 versions
of each function. One generates a translation, rotation, or scaling matrix.
The other translates, rotates, or scales a matrix.

```csharp
using Blazor.WebGPU.Matrix;

var t = Mat4.Translation([1, 2, 3]);     // a translation matrix
var r = Mat4.RotationX(MathF.PI * 0.5);  // a rotation matrix
var s = Mat4.Scaling([1, 2, 3]);         // a scaling matrix
```

```csharp
using Blazor.WebGPU.Matrix;

var m = Mat4.Identity();
var t = Mat4.Translate(m, [1, 2, 3]);     // m * translation([1, 2, 3])
var r = Mat4.RotateX(m, MathF.PI * 0.5);  // m * rotationX(Math.PI * 0.5)
var s = Mat4.Scale(m, [1, 2, 3]);         // m * scaling([1, 2, 3])
```

Functions take an optional destination to hold the result.

```csharp
using Blazor.WebGPU.Matrix;

var m = Mat4.Create();              // m = new mat4
Mat4.Identity(m);                   // m = identity
Mat4.Translate(m, [1, 2, 3], m);    // m *= translation([1, 2, 3])
Mat4.RotateX(m, MathF.PI * 0.5, m);  // m *= rotationX(Math.PI * 0.5)
Mat4.Scale(m, [1, 2, 3], m);        // m *= scaling([1, 2, 3])
```

## Notes

[`mat4.perspective`](https://wgpu-matrix.org/docs/functions/mat4.perspective.html),
[`mat4.ortho`](https://wgpu-matrix.org/docs/functions/mat4.ortho.html), and
[`mat4.frustum`](https://wgpu-matrix.org/docs/functions/mat4.frustum.html)
all return matrices with Z clip space from 0 to 1 (unlike most WebGL matrix libraries which return -1 to 1)

[`mat4.create`](https://wgpu-matrix.org/docs/functions/mat4.create.html) makes an all zero matrix if passed no parameters.
If you want an identity matrix call `mat4.identity`

## Important!

`mat3` uses the space of 12 elements

```csharp
// a mat3
new [
  xx, xy, xz, 0
  yx, yy, yz, 0
  zx, zy, zz, 0
]
```

This is because WebGPU requires mat3s to be in this format and since
this library is for WebGPU it makes sense to match so you can manipulate
mat3s in TypeArrays directly.

`vec3` in this library uses 3 floats per but be aware that an array of
`vec3` in a Uniform Block or other structure in WGSL, each vec3 is
padded to 4 floats! In other words, if you declare

```
struct Foo {
  bar: vec3<f32>[3];
};
```

then bar[0] is at byte offset 0, bar[1] at byte offset 16, bar[2] at byte offset 32.

See [the WGSL spec on alignment and size](https://www.w3.org/TR/WGSL/#alignment-and-size).

## Columns vs Rows

WebGPU follows the same conventions as OpenGL, Vulkan, Metal for matrices. Some people call this "column major". 
The issue is the columns of a traditional "math" matrix are stored as rows when declaring a matrix in code.

```csharp
[
  x1, x2, x3, x4,  // <- column 0
  y1, y2, y3, y4,  // <- column 1
  z1, z2, z3, z4,  // <- column 2
  w1, w2, w3, w4,  // <- column 3
]
```

To put it another way, the translation vector is in elements 12, 13, 14

```csharp
[
  xx, xy, xz, 0,  // <- x-axis
  yx, yy, yz, 0,  // <- y-axis
  zx, zy, zz, 0,  // <- z-axis
  tx, ty, tz, 1,  // <- translation
]
```

This issue has confused programmers since at least the early 90s 😌

## Performance vs Convenience

Most functions take an optional destination as the last argument.
If you don't supply it, a new one (vector, matrix) will be created for you.

```csharp
// convenient usage

var persp = Mat4.Perspective(fov, aspect, near, far);
var camera = Mat4.LookAt(eye, target, up);
var view = Mat4.Inverse(camera);
```

```csharp
// performant usage

// at init time
var persp = Mat4.Create();
var camera = Mat4.Create();
var view = Mat4.Create();

// at usage time
Mat4.Perspective(fov, aspect, near, far, persp);
Mat4.LookAt(eye, target, up, camera);
Mat4.Inverse(camera, view);
```

## Development

```sh
git clone https://github.com/Oblikovati/Blazor.WebGPU.Matrix.git
cd Blazor.WebGPU.Matrix\\Source
VisualStudio Blazor.WebGPU.Matrix.sln
```

You can run tests by running Blazor.WebGPU.Test.Runtime Project

Now go to [https://localhost:7286/](https://localhost:7286/).

You can Click 'Run Tests' to run all the tests.
Or Run tests individually.

## Governance

This package is maintained by [Vinicius Miguel]

PRs with bug-fixes and test enhancements are welcome.

## License

[MIT](LICENSE.md)

[nuget]: https://www.nuget.org/packages/Blazor.WebGPU.Matrix
[SpawnDev.BlazorJS]: https://github.com/LostBeard/SpawnDev.BlazorJS
[Vinicius Miguel]: https://github.com/viniciusmiguel
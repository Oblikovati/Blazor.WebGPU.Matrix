using SpawnDev.BlazorJS.JSObjects;

namespace Blazor.WebGPU.Matrix.Internal;

public abstract class BaseArray<T> where T : struct
{
    protected readonly T[] _elements;

    private BaseArray()
    {
        _elements = System.Array.Empty<T>();
    }

    public BaseArray(long capacity)
    {
        _elements = new T[capacity];
        for(long i = 0; i < capacity; i++)
        {
            _elements[i] = default;
        }
    }

    public BaseArray(TypedArray<T> array)
    {
        _elements = new T[array.Length];
        
        for(long i = 0; i < array.Length; i++)
        {
            _elements[i] = array[i];
        }
    }

    public T this[long i]
    {
        get => _elements[i];
        set => _elements[i] = value;
    }

    public abstract TypedArray<T> Array { get; }
}

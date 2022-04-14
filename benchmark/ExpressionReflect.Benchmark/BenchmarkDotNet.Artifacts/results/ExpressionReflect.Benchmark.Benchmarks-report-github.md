``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1645 (21H1/May2021Update)
Intel Core i7-6700K CPU 4.00GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  DefaultJob : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT


```
|                                        Method |       Mean |     Error |    StdDev |     Median | Rank |  Gen 0 | Allocated |
|---------------------------------------------- |-----------:|----------:|----------:|-----------:|-----:|-------:|----------:|
|                      GetPropertyValue_Compile |   1.275 ns | 0.0249 ns | 0.0220 ns |   1.267 ns |    1 |      - |         - |
| GetPropertyValue_Compile_PreferInterpretation |  76.937 ns | 1.4026 ns | 3.2227 ns |  75.955 ns |    2 | 0.0362 |     152 B |
|                   GetPropertyValue_Reflection |  81.173 ns | 1.6533 ns | 3.4143 ns |  79.759 ns |    3 |      - |         - |
|            GetPropertyValue_ExpressionReflect | 278.375 ns | 3.9513 ns | 3.2995 ns | 277.606 ns |    4 | 0.1297 |     544 B |

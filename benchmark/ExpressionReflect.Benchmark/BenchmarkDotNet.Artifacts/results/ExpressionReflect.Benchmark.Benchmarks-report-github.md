``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1586 (21H1/May2021Update)
Intel Core i7-6700K CPU 4.00GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  DefaultJob : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT


```
|                                        Method |       Mean |     Error |    StdDev | Rank |  Gen 0 | Allocated |
|---------------------------------------------- |-----------:|----------:|----------:|-----:|-------:|----------:|
|                      GetPropertyValue_Compile |   1.111 ns | 0.0513 ns | 0.0591 ns |    1 |      - |         - |
| GetPropertyValue_Compile_PreferInterpretation |  75.320 ns | 1.5276 ns | 2.2864 ns |    2 | 0.0362 |     152 B |
|            GetPropertyValue_ExpressionReflect | 281.360 ns | 4.8041 ns | 4.2587 ns |    3 | 0.1297 |     544 B |

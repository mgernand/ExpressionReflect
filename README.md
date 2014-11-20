ExpressionReflect
=================

Why?
----

Provides the ability to "compile" expressions to delegates without using Reflection.Emit but only using reflection.
The created delegate will make use of reflection to evaluate the expression when it is invoked. This is much slower
than executing a compiled delegate of an expression!

This framework is intended to be used where dynamic code creation is not possible. The main purpose is the usage with
Xamarin.iOS due to it's restriction on Reflection.Emit.

How?
----

The expression tree is traversed and respective reflection calls are created and invoked. This is very slow compared 
to compiled expressions, so it should only be used with simple expressions.

Usage
-----

The usage is fairly simple. It's just an extension method on a expression.

```csharp
Expression<Func<Customer, string>> expression = x => x.Firstname;
Func<Customer, string> reflection = expression.Reflect();
string result = reflection.Invoke(customer);
```

The extension method `Reflect()` will return a delegate that will execute the reflection-based expression evaluation
internally.

What is supported?
------------------

The following built-in delegates are supported at the moment:

* `Func<T, TResult>` (all variations)
* `Action` und `Action<T>` (all variations)
* `Predicate<T>`

The following expression types are supported at the moment:

* Property getter
```csharp
x => x.Firstname
```

* Property getter with subsequent method call
```csharp
x => x.Firstname.ToLower();
```

* Method call with return value
```csharp
x => x.CalculateAge()
```

* Method call with return value and subsequent method call
```csharp
x => x.ToString().ToLower();
```

* Method call with return value and expression parameters
```csharp
x => x.CalculateLength(x.Firstname);
```

* Method call with return value, expression parameters and binary expression
```csharp
x => x.Calculate(x.Age + x.Value);
```

* Method call with return value, expression parameters, binary expression and constant
```csharp
x => x.Calculate(x.Age + 100);
```

* Method call with return value, expression parameters, binary expression and local variable
```csharp
int value = 666;
x => x.Calculate(value);
```

* Method call with return value, expression parameters and nested constructor call.
```csharp
int value = 666;
x => x.Calculate(new Customer(value));
```

* Method call with return value, expression parameters and nested method call.
```csharp
x.Calculate(x.CalculateAge());
```

* Method call with return value, expression parameters and local delegate call.
```csharp
Func<int> method = () => 100;
x => x.Calculate(method());
```

* Method call with return value, expression parameters and local delegate call with parameters.
```csharp
Func<int, int> method = x => x + 100;
x => x.Calculate(method(10));
```

* Method call with return value and mixed parameters
```csharp
x => x.CalculateLength(x.Firstname, x, 10);
```

* Constructor call
```csharp
x => new Customer();
```

* Constructor call with subsequent method call
```csharp
x => new Customer();
```

* Constructor call with expression parameters
```csharp
x => new Customer(x.Lastname, x.Firstname);
```

* Constructor call with expression parameters and binary expression
```csharp
x => new Customer(x.Age + x.Value);
```

* Constructor call with expression parameters, binary expression and constant
```csharp
x => new Customer(x.Age + 100);
```

* Constructor call with expression parameters, binary expression and local variable 
```csharp
int value = 666;
x => new Customer(value);
```

* Constructor call with expression parameters and nested costructor call
```csharp
int value = 666;
x => new Customer(new Customer(value));
```

* Constructor call with expression parameters and nested method call.
```csharp
x => new Customer(x.CalculateAge());
```

* Constructor call with expression parameters and local delegate call.
```csharp
Func<int> method = () => 100;
x => new Customer(method());
```

* Constructor call with expression parameters and local delegate call with parameters.
```csharp
Func<int, int> method = x => x + 100;
x => new Customer(method(10));
```

* Constructor call with mixed parameters
```csharp
x => new Customer(x.Lastname, x, 10, x.Firstname);
```

Supported features
------------------

* `Func<T, TResult>`
* `Action` und `Action<T>`
* `Predicate<T>`
* Property getters including indexers
* Field access
* Method calls with mixed parameters
* Constructor invocations with mixed parameters
* Local variables
* Constant expressions
* Local delegates
* Local delegates with parameters (local and constant, binary expression)
* Binary expressions including 'is' operator
* Unary expressions (almost complete)
* Array access
* Array creation (simple arrays)
* Object initializers (objects, lists, dictionaries)
* Ternary conditionals
* Nested expressions (f.e. LINQ extension methods: `x => x.FirstOrDefault(y.Name == "John")`)

All this features can be combined to more complex expressions.

Contributors
------------

Bernhard Richter (seesharper)


Thank you!
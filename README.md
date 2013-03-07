ExpressionReflect
=================

Why?
----

Provides the ability to "compile" expressions to delegates without using Reflection.Emit but only using reflection.
The created delegate will make use of reflection to execute the expression when it is invoked. This is mich slower
than executing a compiled delegate of an expression.

This framework is intended to be used where dynamic code creation is not possible. The may reason is the use with
Xamarin.iOS due to the restriction on Reflection.Emit.

How?
----

On invoking the created delegate the expression tree is traversed and respective reflection calls are created
and invoked. This is very slow compared to compiled expressions, so it should only be used with simple expressions.

Usage
-----

The usage is fairly simple. It's just an extension method on a generic expression:

```csharp
Expression<Func<Customer, string>> expression = x => x.Firstname;
Func<Customer, string> reflection = expression.Reflect();
string result = reflection.Invoke(customer);
```

Reflect() will return a delegate that will invoke the reflection evaluation internally.

What is supported?
------------------

The following built-in delegates are supported at the moment:

* `Func<T, TResult>`

The following expression types are supported at the moment:

* Simple property getter
```csharp
x => x.Firstname
```

* Simple property getter with subsequent method call
```csharp
x => x.Firstname.ToLower();
```

* Simple method call with return value
```csharp
x => x.CalculateAge()
```

* Simple method call with return value and subsequent method call
```csharp
x => x.ToString().ToLower();
```

* Simple method call with return value and expression parameters
```csharp
x => x.CalculateLength(x.Firstname);
```

* Simple method call with return value, expression parameters and binary expression
```csharp
x => x.Calculate(x.Age + x.Value);
```

* Simple method call with return value, expression parameters, binary expression and constant
```csharp
x => x.Calculate(x.Age + 100);
```

* Simple method call with return value, expression parameters, binary expression and local variable
```csharp
int value = 666;
x => x.Calculate(value);
```

* Simple method call with return value, expression parameters and nested constructor call.
```csharp
int value = 666;
x => x.Calculate(new Customer(value));
```

* Simple method call with return value, expression parameters and nested method call.
```csharp
x.Calculate(x.CalculateAge());
```

* Simple method call with return value, expression parameters and local delegate call.
```csharp
Func<int> method = () => 100;
x => x.Calculate(method());
```

* Simple method call with return value, expression parameters and local delegate call with parameters.
```csharp
Func<int, int> method = x => x + 100;
x => x.Calculate(method(10));
```

* Simple method call with return value and mixed parameters
```csharp
x => x.CalculateLength(x.Firstname, x, 10);
```

* Simple constructor call
```csharp
x => new Customer();
```

* Simple constructor call with subsequent method call
```csharp
x => new Customer();
```

* Simple constructor call with expression parameters
```csharp
x => new Customer(x.Lastname, x.Firstname);
```

* Simple constructor call with expression parameters and binary expression
```csharp
x => new Customer(x.Age + x.Value);
```

* Simple constructor call with expression parameters, binary expression and constant
```csharp
x => new Customer(x.Age + 100);
```

* Simple constructor call with expression parameters, binary expression and local variable 
```csharp
int value = 666;
x => new Customer(value);
```

* Simple constructor call with expression parameters and nested costructor call
```csharp
int value = 666;
x => new Customer(new Customer(value));
```

* Simple constructor call with expression parameters and nested method call.
```csharp
x => new Customer(x.CalculateAge());
```

* Simple constructor call with expression parameters and local delegate call.
```csharp
Func<int> method = () => 100;
x => new Customer(method());
```

* Simple constructor call with expression parameters and local delegate call with parameters.
```csharp
Func<int, int> method = x => x + 100;
x => new Customer(method(10));
```

* Simple constructor call with mixed parameters
```csharp
x => new Customer(x.Lastname, x, 10, x.Firstname);
```

Supported features
------------------

* `Func<T, TResult>`
* Property getters including indexers
* Method calls with mixed parameters
* Constructor invocations with mixed parameters
* Local variables
* Constant expressions
* Local delegates
* Local delegates with parameters (local and constant, binary expression)
* Binary expressions (most)
* Unary expressions (most)
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

	```C#
	Expression<Func<Customer, string>> expression = x => x.Firstname;
	Func<Customer, string> reflection = expression.Reflect();
	string result = reflection.Invoke(customer);
	```

Reflect() will return a delegate that will invoke the reflection evaluation internally.

What is supported?
------------------

The following built-in delegates are supported at the moment:

* Func<T, TResult>

The following expression types are supported at the moment:

* Property getter
	```C#
	x => x.Firstname
	```
* Simple method call with return value
	```C#
	x => x.CalculateAge()
	```
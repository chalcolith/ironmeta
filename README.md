The IronMeta parser generator provides a programming language and application for generating pattern matchers on arbitrary streams of objects. It is an implementation of Alessandro Warth's [OMeta](http://tinlizzie.org/ometa/) system in C#.

IronMeta is available under the terms of the [BSD License](https://github.com/kulibali/ironmeta/wiki/License).

## Changelog

The changelog is available in the repo: [CHANGELOG](https://github.com/kulibali/ironmeta/blob/master/CHANGELOG.md).

## Using IronMeta

IronMeta is available on [NuGet](https://www.nuget.org/packages/IronMeta/).  To install it, open the NuGet shell and type `Install-Package IronMeta`, or use the NuGet tools for Visual Studio.  This will install the IronMeta library in your package.

Once you have installed the NuGet package, add a grammar file with the extension .ironmeta to your project.  Then generate a C# class from it.  You can do this in two ways:

- You can install a [Visual Studio extension](https://visualstudiogallery.msdn.microsoft.com/73263c7c-319f-4f9e-a05a-b493094a4eb0) that provides a custom tool for generating C# code from IronMeta files.  You must set the "Custom Tool" property of your IronMeta file to be `IronMetaGenerator`.  Then the C# code will be generated whenever your grammar file changes.  Syntax errors will appear in your Error List.
- `IronMeta.Library.dll` contains an MsBuild task called "IronMetaGenerate".  A simple example of how to use this:

```
      <UsingTask TaskName="IronMetaGenerate" AssemblyFile="path_to\IronMeta.Library.dll" />
      <Target Name="BeforeBuild">
        <IronMetaGenerate Input="MyParser.ironmeta" Output="MyParser.g.cs" Namespace="MyNamespace" Force="true" />
      </Target>
```

- A command-line program `IronMeta.exe` is included in the NuGet package, in the `tools` directory.  The program takes the following arguments:
  - `-o {output}` (optional): Specify the output file name (defaults to `{input}_.g.cs`).
  - `-n {namespace}` (optional): Specify the namespace to use for the generated parser (defaults to the name of the directory the input file is in).
  - `-f` (optional): Force generation even if the input file is older than the output file.
  - `{input}`: Specify the input file name (must end in `.ironmeta`.)

To use an IronMeta-generated parser in your C# program, create a new instance of the generated parser class. Then call the function `GetMatch()` with the input you wish to parse, and the method of the generated parser object that corresponds to the top-level rule you wish to use. This returns an object of type `IronMeta.Matcher.MatchResult`, which contains information about the result of the match, as well as errors that might have ocurred.

The following is a small sample program that uses the Calc demo parser that is included in the source code:

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    namespace MyCalcProject
    {
        class Program
        {
            static void Main(string[] args)
            {
                var parser = new Calc();
                var match = parser.GetMatch("2 * 7", parser.Expression);

                if (match.Success)
                    Console.WriteLine("result: {0}", match.Result); // should print "14"
                else
                    Console.WriteLine("error: {0}", match.Error); // shouldn't happen
            }
        }
    }

## Building from Source

When you have checked out the GitHub repository, you will need to generate your own signing key in `Source\IronMeta.snk`; open a Visual Studio Developer Command Prompt and type:

    sn -k Source\IronMeta.snk

## Features

Although the most common use for IronMeta is to build parsers on streams of text for use in compiling or other text processing, IronMeta can generate pattern matchers (more accurately, transducers) for any input and output type. You can use C# syntax directly in grammar rules to specify objects to match.

- IronMeta-generated parsers use strict Parsing Expression Grammar semantics; they are greedy and use committed choice.
- Generated parsers are implemented as C# partial classes, allowing you to keep ancillary code in a separate file from your grammar.
- You can use anonymously-typed object literals in rules; they are matched by comparing their properties with the input objects'.
- Unrestricted use of C# in semantic conditions and match actions.
- Higher-order rules: you can pass rules (or arbitrary patterns) as parameters, and then use them in a pattern.
- Pattern matching on rule arguments: you can apply different rule bodies depending on the number and types of parameters.
- Flexible variables: variables in an IronMeta rule may be used to:
  - get the input of an expression they are bound to.
  - get the result or result list of an expression they are bound to.
  - match a rule passed as a parameter.
  - pass a rule on to another rule.
- As an enhancement over the base OMeta, IronMeta allows direct and indirect left recursion, using Sérgio Medeiros et al's [algorithm](http://arxiv.org/abs/1207.0443) for all rules, even within parameter matching.

### Current limitations

Error reporting is currently quite rudimentary, only reporting the last error that ocurred at the rightmost position in the input.

Performance is quite slow, as not much optimization has been done to date.

## The IronMeta Language

This section is an informal introduction to the features of the IronMeta language.

It uses the following IronMeta file named `Calc.ironmeta`, which is included in the IronMeta distribution. It can also be found in the `Samples/Calc` directory in the source.

The Calc grammar is much more complex than it needs to be in order to demonstrate some of the advanced functionality of IronMeta.

    // IronMeta Calculator Example

    using System;
    using System.Linq;

    ironmeta Calc<char, int> : IronMeta.Matcher.CharMatcher<int>
    {
        Expression = Additive;

        Additive = Add | Sub | Multiplicative;

        Add = BinaryOp(Additive, '+', Multiplicative) -> { return _IM_Result.Results.Aggregate((total, n) => total + n); };
        Sub = BinaryOp(Additive, '-', Multiplicative) -> { return _IM_Result.Results.Aggregate((total, n) => total - n); };

        Multiplicative = Multiply | Divide;
        Multiplicative = Number(DecimalDigit);

        Multiply = BinaryOp(Multiplicative, "*", Number, DecimalDigit) -> { return _IM_Result.Results.Aggregate((p, n) => p * n); };
        Divide = BinaryOp(Multiplicative, "/", Number, DecimalDigit) -> { return _IM_Result.Results.Aggregate((q, n) => q / n); };

        BinaryOp :first :op :second .?:type = first:a KW(op) second(type):b -> { return new List<int> { a, b }; };

        Number :type = Digits(type):n WS* -> { return n; };

        Digits :type = Digits(type):a type:b -> { return a*10 + b; };
        Digits :type = type;

        DecimalDigit = .:c ?( (char)c >= '0' && (char)c <= '9' ) -> { return (char)c - '0'; };
        KW :str = str WS*;
        WS = ' ' | '\n' | '\r' | '\t';
    }

We will go through this example line by line to introduce the IronMeta language:

## Comments

    // IronMeta Calculator Example

You may include comments anywhere in the IronMeta file. They may also be in the C-style form:

    /* C-Style Comment */

## Preamble

    using System;
    using System.Linq;

You can include C# `using `statements at the beginning of an IronMeta file. IronMeta will automatically add using statements to its output to include the namespaces it needs.

    ## Parser Declaration

    ironmeta Calc<char, int> : IronMeta.Matcher.CharMatcher<int>

An IronMeta parser always starts with the keyword `ironmeta`. Then comes the name of the parser (`Calc`, in this case), and the input and output types. The generated parser will take as input an `IEnumerable` of the input type, and return as output an object of the output type.

In this case, the Calc parser will operate on a stream of `char` values, and output an `int` value.

> Note: you must always include the input and output types.

You may also optionally include a base class:

    : IronMeta.Matcher.CharMatcher<int>

If you do not include a base class, your parser will inherit directly from `IronMeta.Matcher.Matcher`. The `IronMeta.Matcher.CharMatcher` class provides some specialized functionality for dealing with streams of characters.

## Rules

    Expression = Additive;

An IronMeta rule consists of a name, an pattern for matching parameters, a `=`, a pattern for matching against the main input, and a terminating semicolon `;` (for folks used to C#) or comma `,` (for folks used to OMeta):

In this case, the rule `Expression` has no parameters, and matches by calling another rule, `Additive`.

## Matching Input

You can use the period `.` to match any item of input, or you can use arbitrary C# expressions. The C# expressions may be a string literal, a character literal, a regular expression, an object created using the `new` keyword, or any other expression that is surrounded by curly braces:

    MyPattern = 'a' "b" {3.14159} {new MyClass()};

IronMeta will use the standard C# `object.Equals()` method to match the inputs.

### Regular Expressions

If your input type is `char`, you can use simple regular expressions:

    MyPattern = /a?bc(def+|ghi)(kl)*/;

You can use the following constructs in regular expressions:

- One or more single characters, e.g. `/abc/`.  The following syntax characters must be escaped if you want to match them: `|`, `(`, `)`, `[`, `]`, `\`, `+`, `*`.
- Categories: `\s` matches whitespace, `\d` matches any Unicode digit, `\w` matches any Unicode letter, `\p{Cc}` matches any character in given Unicode general category, e.g. `Lu`, `Nd`.
- Disjunctions: `/abc|def/`.
- Classes: `/[abcd-g]/` (syntax characters must be escaped here as well).  You can use negative categories: `/[^xyz]/`.
- `+` matches one or more elements, star `*` matches zero or more, and `?` matches zero or one.  As usual, these bind tighter than disjunction but looser than sequence.
- `()` will group sequences.

### Matching Anonymous Objects

You can also use anonymous object syntax (you don't need to surround the whole new expression with braces in this case):

    MyPattern = new { Name="MyName", Value="MyValue" }  new { Name="MyName", Value="MySecondValue" };

Literals that you define with anonymous types will be matched according to their public properties; if an input object has the same properties with the same values, it will be considered equal to the anonymous object.

### Matching Sequences

The pattern literal can also be an `IEnumerable` of the input type, including C# strings for sequences of characters.

This eliminates the need for the OMeta `token` function; just use a string literal, or if you are matching on something other than characters, use a list:

    MyPattern = {new List<MyInputType>{ a, b, c }};

## Sequence and Disjunction

    Additive = Add | Sub | Multiplicative;

As is probably obvious from the other rules, you write a sequence of patterns by simply writing them one after the other, separated by whitespace.

To specify a choice between alternatives, separate them with `|`.

> Note: unlike in other parser generator formalisms, separating expressions with a carriage return does NOT mean they are alternatives! You must always use the `|`.

## Other Operators

You can modify the meaning of patterns with the following operators that appear after an expression:

- `?` will match zero or one time.
- `*` will match zero or more times.
- `+` will match one or more times.
- `{N}` will match N times.
- `{Min,Max}` will match at least Min times, and at most Max times.

These operators are all greedy –- they will match as many times a possible and then return that result.

You can stop them from matching by using the prefix operators:

- `&` as a prefix will match an expression but NOT advance the match position. This allows for unlimited lookahead.
- `~` as a prefix will match if the expression does NOT match. It will not advance the match position.

## Conditions and Actions

    DecimalDigit = .:c ?( (char)c >= '0' && (char)c <= '9' ) -> { return (char)c - '0'; };

Here things get more interesting. This rule has only one expression, the period `.`. This will match a single item of input. It is then bound to the variable `c` by means of the colon `:`.

> Note: you can leave out the period if you are binding to a variable; that is, `:c` is equivalent to `.:c`. However, this rule will not necessarily match any character, because it contains a condition. A condition is written with `?` followed immediately by a C# expression in parentheses. The C# expression must evaluate to a bool value. Once the expression matches (in this case it will match anything), it is bound to the variable `c`, which is then available for use in your C# code.

The rule also contains an action. Actions are written with `->` followed by a C# block surrounded by curly braces. This block must contain a return statement that returns a value of the output type, or a `List<>` of the output type.

> Note: if you do not provide an action for the expression, it will simply return the results of its patterns, as a list. Matching a single item will return `default(TResult)` by default, or you can pass a delegate or lambda function to the matcher when you create it that will convert values of the input type to the output type.
  Be aware that an action only applies to the last expression in an OR expression. So the action in the following:

    MyRule = One | Two | Three -> { my action };

will only run if the expression `Three `matches! If you want an action to apply on an OR, use parentheses:

    MyRule = (One | Two | Three) -> { my action };

## Variables

    Digits :type = Digits(type):a type:b -> { return a*10 + b; };

Upon a successful match, variables will contain information about the results of the match of the expression they are bound to. In this example, because `a` & `b` are used in an expression containing an integer, they will automatically evaluate to the results of their expressions, because the result type of the Calc grammar is `int`.

IronMeta variables are very flexible. They contain implicit cast operators to:

- A single value of the input type: this will return the last item in the list of results of the expression that the variable is bound to.
- A single value of the output type.
- A `List<>` of the input type.
- A `List<>` of the output type.

If your input and output types are the same, the implicit cast operators will only return the inputs, and you will need to use the explicit variable properties:

- `c.Inputs` returns the list of inputs that the parse pattern matched.
- `c.Results` returns the list of results that resulted from the match.
- `c.StartIndex` returns the index in the input stream at which the pattern started matching.
- `c.NextIndex` returns the first index in the input stream after the pattern match ended.

You can also use variables in a pattern, in which case they will match whatever input they matched when they were bound. Or, if they were bound to a rule in a parameter pattern (see below), they will call that rule. You can even pass parameters to them.

### Built-In Variables

IronMeta automatically defines a variable for use in your C# code: `_IM_Result` is bound to the entire expression that your condition or action applies to.

## Multiple Rule Bodies

    Multiplicative = Multiply | Divide;
    Multiplicative = Number(DecimalDigit);

You can have multiple rule bodies; their patterns will be combined in one overall OR when that rule is called.

## Parameters

    Add = BinaryOp(Additive, '+', Multiplicative) -> { return _IM_Result.Results.Aggregate((total, n) => total + n); };

This rule shows that you can pass parameters to a rule. You can pass literal match patterns, rule names, or variables.

    BinaryOp :first :op :second .?:type = first:a KW(op) second(type):b -> { return new List<int> { a, b }; };

This rule demonstrates how to match parameters. The parameter part of a rule is actually a matching pattern no different than that on the right-hand side of the `=`. Using this fact, plus the ability to specify multiple rules with the same name, you can write rules that match differently depending on the number and kind of parameters they are passed.

## Rules as Arguments

    Add = BinaryOp(Additive, '+', Multiplicative) -> { return _IM_Result.Results.Aggregate((total, n) => total + n); };
    BinaryOp :first :op :second .?:type = first:a KW(op) second(type):b -> { return new List<int> { a, b }; };

These rules show that you can pass rules as parameters to other rules. To match against them, just capture them in a variable in your parameter pattern, and then use the variable as an expression in your pattern. You can pass parameters as usual.

## Patterns as Arguments

You can also pass arbitrary patterns as arguments.  Variables from the outer rule that you use in the argument pattern will be passed to the inner pattern when matching.

## List Folding

    KW :str = str Whitespace*;

If you look at the rules that call this rule (indirectly through the BinaryOp rule), you'll see that they pass both a single character and a string:

    Sub = BinaryOp(Additive, '-', Multiplicative) -> { return _IM_Result.Results.Aggregate((total, n) => total - n); };
    Divide = BinaryOp(Multiplicative, "/", Number, DecimalDigit) -> { return _IM_Result.Results.Aggregate((q, n) => q / n); };

When matching against variables captured in parameters, variables containing single items or variables containing lists will match correctly.

## Rule Inheritance

IronMeta parsers are regular C# classes, so they can inherit from other parsers and call their rules. You must preface rules you wish to override by the C# keyword `virtual`. You can override rules in a base class by prefixing the rule definition by the keyword `override`, or hide non-virtual rules by means of the `new` keyword.

    ironmeta DerivedParser<char, Node> : BaseParser<Node>
    {
        virtual Expression1 = ...;
        override Expression2 = ...;
        new Expression3 = ...;
    }

## Rule Encapsulation

You can also refer to rules in a completely unrelated grammar (as long as the input and output types are the same) by declaring initialized members of the other grammar's class and referring to those members' rule functions.

    public partial class MyParser
    {
        private OtherParser other_parser = new OtherParser();
    }

    ironmeta MyParser<char, int>: IronMeta.Matcher.Matcher<char, int>
    {
        Rule = "foo" other_parser.OtherRule "bar";
    }

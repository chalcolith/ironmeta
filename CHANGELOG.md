#### 4.4.6: May 24, 2021:
- Some cleanup and added .NET 5
- Merge fixes and tests for `Slice` from @steve7411.

#### 4.4.2: June 3, 2019:
- Fixed #25: correctly emit escaped double-quotes for regexes.

#### 4.4.1: October 24, 2018:
- Added cache for checking anonymous types; thanks to @AndreyZakatov
- Updated VS extension to VS2019

#### 4.4.0: January 1, 2018:
- Updated Visual Studio extension to VS2017

#### 4.3.1: December 29, 2017:
- Fixed #19: Regex matches broken

#### 4.3: March 29, 2016:
- Now supports passing arbitrary patterns as rule arguments.

#### 4.1: June 30, 2015:
- Now supports simple regular expressions in rule bodies.

#### 4.0: June 23, 2015:
- Refactored for Visual Studio 2015.
- No longer tries to automatically install the Visual Studio Extension.

#### 3.8: September 25, 2014:
- Numerous bug-fixes and tweaks to the NuGet package.

#### 3.2: October 22, 2013:
- Memoization of left-recursive productions more memory efficient (thanks to pragmatrix)
- Plugin now installs in VS 2013.

#### 3.0: April 9, 2013: VS 2012 and NuGet package.
- Now set up to build with Visual Studio 2012.
- Now distributed as a NuGet package.

#### 2.3: February 27, 2012: Combining parsers.
- Made generated code more general so it is now possible to combine parsers by inheritance or encapsulation.
- Added the ability to use anonymous object literals in rules. They match by comparing their public properties with the input object's properties.
- Fixed a bug where string and char literals were not correctly handled in parsers whose input was not of type char.

#### 2.2: February 21, 2012: Miscellaneous bug fixes.
- Fixed an off-by-one error in input enumerables.
- Now compiles with Mono.

#### 2.1: July 1, 2011: Much refactoring, miscellaneous bug fixes.
- Better error handling.
- Added `IronMeta.Matcher.CharMatcher.Input()` and `IronMeta.Matcher.CharMatcher.Trimmed()` for more convenient string handling.
- Added min/max repeats syntax (e.g. `'a' {1, 3}`).

#### 2.0: December 4, 2010: Many internal improvements.
- Removed general backtracking functionality in favor of strict PEG semantics only.
- Changed from Warth et al's left-recursion to Sergio Queiroz de Medeiros's much simpler and more general one.
- Massive speedup.

#### 1.4: July 18, 2009: Bug fixes and samples.
- Fixed a bug when passing rules in a base class to another rule.
- Added a sample that parses a data file and produces an XmlDocument.

#### 1.3: June 2, 2009: Optimization pass.
- IronMeta is now about an order of magnitude faster.
- Fixed a bug when handling C# string literals.

#### 1.2: June 1, 2009: Bug fixes and miscellaneous enhancements.
- Added a simple interactive shell to the Calc program.
- Fixed a bug that caused redundant evaluation of involved rules.
- Fixed a bug with parsers that don't declare a base class.
- Added "::=" as alternative to "=" for rules.
- Changed condition operator to single question mark to conform to other OMetas.
- C# object literals must be surrounded by curly braces.

#### 1.1:
- Can now handle IEnumerable literals (e.g. strings, for a matcher of characters).

#### 1.0: May 16, 2009:
- Initial release

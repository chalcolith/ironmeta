2.3: February 27, 2012: Combining parsers.
	- Made generated code more general so it is now possible to combine parsers by inheritance or encapsulation.
	- Added the ability to use anonymous object literals in rules.  They match by comparing their public properties with the input object's properties.
	- Fixed a bug where string and char literals were not correctly handled in parsers whose input was not of type \c char.

2.2: February 21, 2012: Miscellaneous bug fixes.
    - Fixed an off-by-one error in input enumerables.
	- Now compiles with Mono.
	
2.1: July 1, 2011: Much refactoring, miscellaneous bug fixes.
    - Better error handling.
	- Added IronMeta.Matcher.CharMatcher.Input() and IronMeta.Matcher.CharMatcher.Trimmed() for more convenient string handling.
	- Added min/max repeats syntax (e.g. \c 'a' \c {1, \c 3}).

2.0: December 4, 2010: Many internal improvements.
    - Removed general backtracking functionality in favor of strict PEG semantics only.
    - Changed from Warth et al's left-recursion to Sergio Queiroz de Medeiros's much simpler and more general one.
    - Massive speedup.

1.4: July 18, 2009: Bug fixes and samples.
    - Fixed a bug when passing rules in a base class to another rule.
    - Added a sample that parses a data file and produces an XmlDocument.

1.3: June 2, 2009: Optimization pass.
    - IronMeta is now about an order of magnitude faster.
    - Fixed a bug when handling C# string literals.

1.2: June 1, 2009: Bug fixes and miscellaneous enhancements.	
    - Added a simple interactive shell to the Calc program.
    - Fixed a bug that caused redundant evaluation of involved rules.
    - Fixed a bug with parsers that don't declare a base class.
    - Added "::=" as alternative to "=" for rules.
    - Changed condition operator to single question mark to conform to other OMetas.
    - C# object literals must be surrounded by curly braces.

1.1:
    - Can now handle IEnumerable literals (e.g. strings, for a matcher of characters).

1.0: May 16, 2009: Initial release

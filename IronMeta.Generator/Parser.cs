//
// IronMeta Parser Parser; Generated 12/04/2011 11:46:49 PM UTC
//

using System;
using System.Collections.Generic;
using System.Linq;
using IronMeta.Matcher;
using IronMeta.Generator;
using IronMeta.Generator.AST;

namespace IronMeta.Generator
{

    using _Parser_Inputs = IEnumerable<char>;
    using _Parser_Results = IEnumerable<ASTNode<_Parser_Item>>;
    using _Parser_Args = IEnumerable<_Parser_Item>;
    using _Parser_Rule = System.Action<int, IEnumerable<_Parser_Item>>;
    using _Parser_Base = IronMeta.Matcher.Matcher<char, ASTNode<_Parser_Item>, _Parser_Item>;


    public class _Parser_Item : IronMeta.Matcher.MatchItem<char, ASTNode<_Parser_Item>, _Parser_Item>
    {
        public _Parser_Item() { }
        public _Parser_Item(char input) : base(input) { }
        public _Parser_Item(char input, ASTNode<_Parser_Item> result) : base(input, result) { }
        public _Parser_Item(_Parser_Inputs inputs) : base(inputs) { }
        public _Parser_Item(_Parser_Inputs inputs, _Parser_Results results) : base(inputs, results) { }
        public _Parser_Item(int start, int next, _Parser_Inputs inputs, _Parser_Results results, bool relative) : base(start, next, inputs, results, relative) { }
        public _Parser_Item(int start, _Parser_Inputs inputs) : base(start, start, inputs, Enumerable.Empty<ASTNode<_Parser_Item>>(), true) { }
        public _Parser_Item(_Parser_Rule production) : base(production) { }

        public static implicit operator List<char>(_Parser_Item item) { return item != null ? item.Inputs.ToList() : new List<char>(); }
        public static implicit operator char(_Parser_Item item) { return item != null ? item.Inputs.LastOrDefault() : default(char); }
        public static implicit operator List<ASTNode<_Parser_Item>>(_Parser_Item item) { return item != null ? item.Results.ToList() : new List<ASTNode<_Parser_Item>>(); }
        public static implicit operator ASTNode<_Parser_Item>(_Parser_Item item) { return item != null ? item.Results.LastOrDefault() : default(ASTNode<_Parser_Item>); }
    }

    public partial class Parser : IronMeta.Matcher.CharMatcher<ASTNode<_Parser_Item>, _Parser_Item>
    {
        public Parser()
            : base()
        { }

        public void IronMetaFile(int _index, _Parser_Args _args)
        {

            _Parser_Item p = null;
            _Parser_Item g = null;

            // AND 1
            int _start_i1 = _index;

            // AND 2
            int _start_i2 = _index;

            // AND 3
            int _start_i3 = _index;

            // AND 4
            int _start_i4 = _index;

            // LITERAL '\ufeff'
            _ParseLiteralChar(ref _index, '\ufeff');

            // QUES
            if (_results.Peek() == null) { _results.Pop(); _results.Push(new _Parser_Item(_index, _input_enumerable)); }

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label4; }

            // CALLORVAR SP
            _Parser_Item _r7;

            _r7 = _MemoCall("SP", _index, SP, null);

            if (_r7 != null) _index = _r7.NextIndex;

        label4: // AND
            var _r4_2 = _results.Pop();
            var _r4_1 = _results.Pop();

            if (_r4_1 != null && _r4_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i4, _index, _input_enumerable, _r4_1.Results.Concat(_r4_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i4;
            }

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label3; }

            // CALLORVAR Preamble
            _Parser_Item _r10;

            _r10 = _MemoCall("Preamble", _index, Preamble, null);

            if (_r10 != null) _index = _r10.NextIndex;

            // QUES
            if (_results.Peek() == null) { _results.Pop(); _results.Push(new _Parser_Item(_index, _input_enumerable)); }

            // BIND p
            p = _results.Peek();

        label3: // AND
            var _r3_2 = _results.Pop();
            var _r3_1 = _results.Pop();

            if (_r3_1 != null && _r3_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i3, _index, _input_enumerable, _r3_1.Results.Concat(_r3_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i3;
            }

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label2; }

            // CALLORVAR Grammar
            _Parser_Item _r12;

            _r12 = _MemoCall("Grammar", _index, Grammar, null);

            if (_r12 != null) _index = _r12.NextIndex;

            // BIND g
            g = _results.Peek();

        label2: // AND
            var _r2_2 = _results.Pop();
            var _r2_1 = _results.Pop();

            if (_r2_1 != null && _r2_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i2, _index, _input_enumerable, _r2_1.Results.Concat(_r2_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i2;
            }

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label1; }

            // CALLORVAR EOF
            _Parser_Item _r13;

            _r13 = _MemoCall("EOF", _index, EOF, null);

            if (_r13 != null) _index = _r13.NextIndex;

        label1: // AND
            var _r1_2 = _results.Pop();
            var _r1_1 = _results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i1, _index, _input_enumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i1;
            }

            // ACT
            var _r0 = _results.Peek();
            if (_r0 != null)
            {
                _results.Pop();
                _results.Push( new _Parser_Item(_r0.StartIndex, _r0.NextIndex, _input_enumerable, _Thunk(_IM_Result => { return new GrammarFile<_Parser_Item>(p, g); }, _r0), true) );
            }

        }


        public void Preamble(int _index, _Parser_Args _args)
        {

            _Parser_Item u = null;

            // PLUS 2
            int _start_i2 = _index;
            var _res2 = Enumerable.Empty<ASTNode<_Parser_Item>>();
        label2:

            // CALLORVAR Using
            _Parser_Item _r3;

            _r3 = _MemoCall("Using", _index, Using, null);

            if (_r3 != null) _index = _r3.NextIndex;

            // PLUS 2
            var _r2 = _results.Pop();
            if (_r2 != null)
            {
                _res2 = _res2.Concat(_r2.Results);
                goto label2;
            }
            else
            {
                if (_index > _start_i2)
                    _results.Push(new _Parser_Item(_start_i2, _index, _input_enumerable, _res2.Where(_NON_NULL), true));
                else
                    _results.Push(null);
            }

            // BIND u
            u = _results.Peek();

            // ACT
            var _r0 = _results.Peek();
            if (_r0 != null)
            {
                _results.Pop();
                _results.Push( new _Parser_Item(_r0.StartIndex, _r0.NextIndex, _input_enumerable, _Thunk(_IM_Result => { return new Preamble<_Parser_Item>(u.Results); }, _r0), true) );
            }

        }


        public void Using(int _index, _Parser_Args _args)
        {

            _Parser_Item name = null;

            // AND 1
            int _start_i1 = _index;

            // AND 2
            int _start_i2 = _index;

            // AND 3
            int _start_i3 = _index;

            // CALLORVAR USING
            _Parser_Item _r4;

            _r4 = _MemoCall("USING", _index, USING, null);

            if (_r4 != null) _index = _r4.NextIndex;

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label3; }

            // AND 6
            int _start_i6 = _index;

            // CALLORVAR Ident
            _Parser_Item _r7;

            _r7 = _MemoCall("Ident", _index, Ident, null);

            if (_r7 != null) _index = _r7.NextIndex;

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label6; }

            // STAR 8
            int _start_i8 = _index;
            var _res8 = Enumerable.Empty<ASTNode<_Parser_Item>>();
        label8:

            // AND 9
            int _start_i9 = _index;

            // CALLORVAR DOT
            _Parser_Item _r10;

            _r10 = _MemoCall("DOT", _index, DOT, null);

            if (_r10 != null) _index = _r10.NextIndex;

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label9; }

            // CALLORVAR Ident
            _Parser_Item _r11;

            _r11 = _MemoCall("Ident", _index, Ident, null);

            if (_r11 != null) _index = _r11.NextIndex;

        label9: // AND
            var _r9_2 = _results.Pop();
            var _r9_1 = _results.Pop();

            if (_r9_1 != null && _r9_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i9, _index, _input_enumerable, _r9_1.Results.Concat(_r9_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i9;
            }

            // STAR 8
            var _r8 = _results.Pop();
            if (_r8 != null)
            {
                _res8 = _res8.Concat(_r8.Results);
                goto label8;
            }
            else
            {
                _results.Push(new _Parser_Item(_start_i8, _index, _input_enumerable, _res8.Where(_NON_NULL), true));
            }

        label6: // AND
            var _r6_2 = _results.Pop();
            var _r6_1 = _results.Pop();

            if (_r6_1 != null && _r6_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i6, _index, _input_enumerable, _r6_1.Results.Concat(_r6_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i6;
            }

            // BIND name
            name = _results.Peek();

        label3: // AND
            var _r3_2 = _results.Pop();
            var _r3_1 = _results.Pop();

            if (_r3_1 != null && _r3_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i3, _index, _input_enumerable, _r3_1.Results.Concat(_r3_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i3;
            }

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label2; }

            // CALLORVAR SP
            _Parser_Item _r12;

            _r12 = _MemoCall("SP", _index, SP, null);

            if (_r12 != null) _index = _r12.NextIndex;

        label2: // AND
            var _r2_2 = _results.Pop();
            var _r2_1 = _results.Pop();

            if (_r2_1 != null && _r2_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i2, _index, _input_enumerable, _r2_1.Results.Concat(_r2_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i2;
            }

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label1; }

            // CALLORVAR SEMI
            _Parser_Item _r13;

            _r13 = _MemoCall("SEMI", _index, SEMI, null);

            if (_r13 != null) _index = _r13.NextIndex;

        label1: // AND
            var _r1_2 = _results.Pop();
            var _r1_1 = _results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i1, _index, _input_enumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i1;
            }

            // ACT
            var _r0 = _results.Peek();
            if (_r0 != null)
            {
                _results.Pop();
                _results.Push( new _Parser_Item(_r0.StartIndex, _r0.NextIndex, _input_enumerable, _Thunk(_IM_Result => { return new Using<_Parser_Item>(name); }, _r0), true) );
            }

        }


        public void Grammar(int _index, _Parser_Args _args)
        {

            _Parser_Item name = null;
            _Parser_Item tinput = null;
            _Parser_Item tresult = null;
            _Parser_Item baseclass = null;
            _Parser_Item rules = null;

            // AND 1
            int _start_i1 = _index;

            // AND 2
            int _start_i2 = _index;

            // AND 3
            int _start_i3 = _index;

            // AND 4
            int _start_i4 = _index;

            // AND 5
            int _start_i5 = _index;

            // AND 6
            int _start_i6 = _index;

            // AND 7
            int _start_i7 = _index;

            // AND 8
            int _start_i8 = _index;

            // AND 9
            int _start_i9 = _index;

            // AND 10
            int _start_i10 = _index;

            // AND 11
            int _start_i11 = _index;

            // CALLORVAR IRONMETA
            _Parser_Item _r12;

            _r12 = _MemoCall("IRONMETA", _index, IRONMETA, null);

            if (_r12 != null) _index = _r12.NextIndex;

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label11; }

            // CALLORVAR Identifier
            _Parser_Item _r14;

            _r14 = _MemoCall("Identifier", _index, Identifier, null);

            if (_r14 != null) _index = _r14.NextIndex;

            // BIND name
            name = _results.Peek();

        label11: // AND
            var _r11_2 = _results.Pop();
            var _r11_1 = _results.Pop();

            if (_r11_1 != null && _r11_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i11, _index, _input_enumerable, _r11_1.Results.Concat(_r11_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i11;
            }

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label10; }

            // CALLORVAR LESS
            _Parser_Item _r15;

            _r15 = _MemoCall("LESS", _index, LESS, null);

            if (_r15 != null) _index = _r15.NextIndex;

        label10: // AND
            var _r10_2 = _results.Pop();
            var _r10_1 = _results.Pop();

            if (_r10_1 != null && _r10_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i10, _index, _input_enumerable, _r10_1.Results.Concat(_r10_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i10;
            }

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label9; }

            // CALLORVAR GenericId
            _Parser_Item _r17;

            _r17 = _MemoCall("GenericId", _index, GenericId, null);

            if (_r17 != null) _index = _r17.NextIndex;

            // BIND tinput
            tinput = _results.Peek();

        label9: // AND
            var _r9_2 = _results.Pop();
            var _r9_1 = _results.Pop();

            if (_r9_1 != null && _r9_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i9, _index, _input_enumerable, _r9_1.Results.Concat(_r9_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i9;
            }

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label8; }

            // CALLORVAR COMMA
            _Parser_Item _r18;

            _r18 = _MemoCall("COMMA", _index, COMMA, null);

            if (_r18 != null) _index = _r18.NextIndex;

        label8: // AND
            var _r8_2 = _results.Pop();
            var _r8_1 = _results.Pop();

            if (_r8_1 != null && _r8_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i8, _index, _input_enumerable, _r8_1.Results.Concat(_r8_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i8;
            }

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label7; }

            // CALLORVAR GenericId
            _Parser_Item _r20;

            _r20 = _MemoCall("GenericId", _index, GenericId, null);

            if (_r20 != null) _index = _r20.NextIndex;

            // BIND tresult
            tresult = _results.Peek();

        label7: // AND
            var _r7_2 = _results.Pop();
            var _r7_1 = _results.Pop();

            if (_r7_1 != null && _r7_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i7, _index, _input_enumerable, _r7_1.Results.Concat(_r7_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i7;
            }

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label6; }

            // CALLORVAR GREATER
            _Parser_Item _r21;

            _r21 = _MemoCall("GREATER", _index, GREATER, null);

            if (_r21 != null) _index = _r21.NextIndex;

        label6: // AND
            var _r6_2 = _results.Pop();
            var _r6_1 = _results.Pop();

            if (_r6_1 != null && _r6_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i6, _index, _input_enumerable, _r6_1.Results.Concat(_r6_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i6;
            }

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label5; }

            // CALLORVAR COLON
            _Parser_Item _r22;

            _r22 = _MemoCall("COLON", _index, COLON, null);

            if (_r22 != null) _index = _r22.NextIndex;

        label5: // AND
            var _r5_2 = _results.Pop();
            var _r5_1 = _results.Pop();

            if (_r5_1 != null && _r5_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i5, _index, _input_enumerable, _r5_1.Results.Concat(_r5_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i5;
            }

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label4; }

            // CALLORVAR GenericId
            _Parser_Item _r24;

            _r24 = _MemoCall("GenericId", _index, GenericId, null);

            if (_r24 != null) _index = _r24.NextIndex;

            // BIND baseclass
            baseclass = _results.Peek();

        label4: // AND
            var _r4_2 = _results.Pop();
            var _r4_1 = _results.Pop();

            if (_r4_1 != null && _r4_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i4, _index, _input_enumerable, _r4_1.Results.Concat(_r4_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i4;
            }

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label3; }

            // CALLORVAR BRA
            _Parser_Item _r25;

            _r25 = _MemoCall("BRA", _index, BRA, null);

            if (_r25 != null) _index = _r25.NextIndex;

        label3: // AND
            var _r3_2 = _results.Pop();
            var _r3_1 = _results.Pop();

            if (_r3_1 != null && _r3_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i3, _index, _input_enumerable, _r3_1.Results.Concat(_r3_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i3;
            }

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label2; }

            // STAR 27
            int _start_i27 = _index;
            var _res27 = Enumerable.Empty<ASTNode<_Parser_Item>>();
        label27:

            // CALLORVAR Rule
            _Parser_Item _r28;

            _r28 = _MemoCall("Rule", _index, Rule, null);

            if (_r28 != null) _index = _r28.NextIndex;

            // STAR 27
            var _r27 = _results.Pop();
            if (_r27 != null)
            {
                _res27 = _res27.Concat(_r27.Results);
                goto label27;
            }
            else
            {
                _results.Push(new _Parser_Item(_start_i27, _index, _input_enumerable, _res27.Where(_NON_NULL), true));
            }

            // BIND rules
            rules = _results.Peek();

        label2: // AND
            var _r2_2 = _results.Pop();
            var _r2_1 = _results.Pop();

            if (_r2_1 != null && _r2_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i2, _index, _input_enumerable, _r2_1.Results.Concat(_r2_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i2;
            }

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label1; }

            // CALLORVAR KET
            _Parser_Item _r29;

            _r29 = _MemoCall("KET", _index, KET, null);

            if (_r29 != null) _index = _r29.NextIndex;

        label1: // AND
            var _r1_2 = _results.Pop();
            var _r1_1 = _results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i1, _index, _input_enumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i1;
            }

            // ACT
            var _r0 = _results.Peek();
            if (_r0 != null)
            {
                _results.Pop();
                _results.Push( new _Parser_Item(_r0.StartIndex, _r0.NextIndex, _input_enumerable, _Thunk(_IM_Result => { return new Grammar<_Parser_Item>(name, tinput, tresult, baseclass, rules.Results); }, _r0), true) );
            }

        }


        public void Rule(int _index, _Parser_Args _args)
        {

            _Parser_Item ovr = null;
            _Parser_Item name = null;
            _Parser_Item parms = null;
            _Parser_Item body = null;

            // AND 1
            int _start_i1 = _index;

            // AND 2
            int _start_i2 = _index;

            // AND 3
            int _start_i3 = _index;

            // AND 4
            int _start_i4 = _index;

            // AND 5
            int _start_i5 = _index;

            // CALLORVAR OVERRIDE
            _Parser_Item _r8;

            _r8 = _MemoCall("OVERRIDE", _index, OVERRIDE, null);

            if (_r8 != null) _index = _r8.NextIndex;

            // QUES
            if (_results.Peek() == null) { _results.Pop(); _results.Push(new _Parser_Item(_index, _input_enumerable)); }

            // BIND ovr
            ovr = _results.Peek();

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label5; }

            // CALLORVAR Identifier
            _Parser_Item _r10;

            _r10 = _MemoCall("Identifier", _index, Identifier, null);

            if (_r10 != null) _index = _r10.NextIndex;

            // BIND name
            name = _results.Peek();

        label5: // AND
            var _r5_2 = _results.Pop();
            var _r5_1 = _results.Pop();

            if (_r5_1 != null && _r5_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i5, _index, _input_enumerable, _r5_1.Results.Concat(_r5_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i5;
            }

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label4; }

            // CALLORVAR Disjunction
            _Parser_Item _r13;

            _r13 = _MemoCall("Disjunction", _index, Disjunction, null);

            if (_r13 != null) _index = _r13.NextIndex;

            // QUES
            if (_results.Peek() == null) { _results.Pop(); _results.Push(new _Parser_Item(_index, _input_enumerable)); }

            // BIND parms
            parms = _results.Peek();

        label4: // AND
            var _r4_2 = _results.Pop();
            var _r4_1 = _results.Pop();

            if (_r4_1 != null && _r4_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i4, _index, _input_enumerable, _r4_1.Results.Concat(_r4_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i4;
            }

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label3; }

            // CALLORVAR EQUALS
            _Parser_Item _r14;

            _r14 = _MemoCall("EQUALS", _index, EQUALS, null);

            if (_r14 != null) _index = _r14.NextIndex;

        label3: // AND
            var _r3_2 = _results.Pop();
            var _r3_1 = _results.Pop();

            if (_r3_1 != null && _r3_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i3, _index, _input_enumerable, _r3_1.Results.Concat(_r3_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i3;
            }

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label2; }

            // CALLORVAR Disjunction
            _Parser_Item _r16;

            _r16 = _MemoCall("Disjunction", _index, Disjunction, null);

            if (_r16 != null) _index = _r16.NextIndex;

            // BIND body
            body = _results.Peek();

        label2: // AND
            var _r2_2 = _results.Pop();
            var _r2_1 = _results.Pop();

            if (_r2_1 != null && _r2_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i2, _index, _input_enumerable, _r2_1.Results.Concat(_r2_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i2;
            }

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label1; }

            // CALLORVAR SEMI
            _Parser_Item _r17;

            _r17 = _MemoCall("SEMI", _index, SEMI, null);

            if (_r17 != null) _index = _r17.NextIndex;

        label1: // AND
            var _r1_2 = _results.Pop();
            var _r1_1 = _results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i1, _index, _input_enumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i1;
            }

            // ACT
            var _r0 = _results.Peek();
            if (_r0 != null)
            {
                _results.Pop();
                _results.Push( new _Parser_Item(_r0.StartIndex, _r0.NextIndex, _input_enumerable, _Thunk(_IM_Result => { return new Rule<_Parser_Item>(name, parms.Inputs.Any() ? new Args<_Parser_Item>(parms, body) : (AST.ASTNode<_Parser_Item>)body, ovr.Inputs.Any()); }, _r0), true) );
            }

        }


        public void Disjunction(int _index, _Parser_Args _args)
        {

            // AND 1
            int _start_i1 = _index;

            // CALLORVAR ActionExp
            _Parser_Item _r2;

            _r2 = _MemoCall("ActionExp", _index, ActionExp, null);

            if (_r2 != null) _index = _r2.NextIndex;

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label1; }

            // STAR 3
            int _start_i3 = _index;
            var _res3 = Enumerable.Empty<ASTNode<_Parser_Item>>();
        label3:

            // AND 4
            int _start_i4 = _index;

            // CALLORVAR OR
            _Parser_Item _r5;

            _r5 = _MemoCall("OR", _index, OR, null);

            if (_r5 != null) _index = _r5.NextIndex;

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label4; }

            // CALLORVAR ActionExp
            _Parser_Item _r6;

            _r6 = _MemoCall("ActionExp", _index, ActionExp, null);

            if (_r6 != null) _index = _r6.NextIndex;

        label4: // AND
            var _r4_2 = _results.Pop();
            var _r4_1 = _results.Pop();

            if (_r4_1 != null && _r4_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i4, _index, _input_enumerable, _r4_1.Results.Concat(_r4_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i4;
            }

            // STAR 3
            var _r3 = _results.Pop();
            if (_r3 != null)
            {
                _res3 = _res3.Concat(_r3.Results);
                goto label3;
            }
            else
            {
                _results.Push(new _Parser_Item(_start_i3, _index, _input_enumerable, _res3.Where(_NON_NULL), true));
            }

        label1: // AND
            var _r1_2 = _results.Pop();
            var _r1_1 = _results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i1, _index, _input_enumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i1;
            }

            // ACT
            var _r0 = _results.Peek();
            if (_r0 != null)
            {
                _results.Pop();
                _results.Push( new _Parser_Item(_r0.StartIndex, _r0.NextIndex, _input_enumerable, _Thunk(_IM_Result => { return _IM_Result.Results
                    .Where(node => node != null)
                    .Aggregate<ASTNode<_Parser_Item>, ASTNode<_Parser_Item>>(null, (prev, cur) => prev != null ? new Or<_Parser_Item>(prev, cur) : cur); }, _r0), true) );
            }

        }


        public void ActionExp(int _index, _Parser_Args _args)
        {

            _Parser_Item body = null;
            _Parser_Item action = null;

            // OR 0
            int _start_i0 = _index;

            // OR 1
            int _start_i1 = _index;

            // CALLORVAR FailExp
            _Parser_Item _r2;

            _r2 = _MemoCall("FailExp", _index, FailExp, null);

            if (_r2 != null) _index = _r2.NextIndex;

            // OR shortcut
            if (_results.Peek() == null) { _results.Pop(); _index = _start_i1; } else goto label1;

            // AND 4
            int _start_i4 = _index;

            // AND 5
            int _start_i5 = _index;

            // AND 6
            int _start_i6 = _index;

            // CALLORVAR SequenceExp
            _Parser_Item _r8;

            _r8 = _MemoCall("SequenceExp", _index, SequenceExp, null);

            if (_r8 != null) _index = _r8.NextIndex;

            // BIND body
            body = _results.Peek();

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label6; }

            // CALLORVAR ACTION
            _Parser_Item _r9;

            _r9 = _MemoCall("ACTION", _index, ACTION, null);

            if (_r9 != null) _index = _r9.NextIndex;

        label6: // AND
            var _r6_2 = _results.Pop();
            var _r6_1 = _results.Pop();

            if (_r6_1 != null && _r6_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i6, _index, _input_enumerable, _r6_1.Results.Concat(_r6_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i6;
            }

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label5; }

            // LOOK 10
            int _start_i10 = _index;

            // CALLORVAR BRA
            _Parser_Item _r11;

            _r11 = _MemoCall("BRA", _index, BRA, null);

            if (_r11 != null) _index = _r11.NextIndex;

            // LOOK 10
            var _r10 = _results.Pop();
            _results.Push( _r10 != null ? new _Parser_Item(_start_i10, _input_enumerable) : null );
            _index = _start_i10;

        label5: // AND
            var _r5_2 = _results.Pop();
            var _r5_1 = _results.Pop();

            if (_r5_1 != null && _r5_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i5, _index, _input_enumerable, _r5_1.Results.Concat(_r5_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i5;
            }

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label4; }

            // CALLORVAR CSharpCode
            _Parser_Item _r13;

            _r13 = _MemoCall("CSharpCode", _index, CSharpCode, null);

            if (_r13 != null) _index = _r13.NextIndex;

            // BIND action
            action = _results.Peek();

        label4: // AND
            var _r4_2 = _results.Pop();
            var _r4_1 = _results.Pop();

            if (_r4_1 != null && _r4_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i4, _index, _input_enumerable, _r4_1.Results.Concat(_r4_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i4;
            }

            // ACT
            var _r3 = _results.Peek();
            if (_r3 != null)
            {
                _results.Pop();
                _results.Push( new _Parser_Item(_r3.StartIndex, _r3.NextIndex, _input_enumerable, _Thunk(_IM_Result => { return new Act<_Parser_Item>(body, action); }, _r3), true) );
            }

        label1: // OR
            int _dummy_i1 = _index; // no-op for label

            // OR shortcut
            if (_results.Peek() == null) { _results.Pop(); _index = _start_i0; } else goto label0;

            // CALLORVAR SequenceExp
            _Parser_Item _r14;

            _r14 = _MemoCall("SequenceExp", _index, SequenceExp, null);

            if (_r14 != null) _index = _r14.NextIndex;

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void FailExp(int _index, _Parser_Args _args)
        {

            _Parser_Item msg = null;

            // AND 1
            int _start_i1 = _index;

            // CALLORVAR BANG
            _Parser_Item _r2;

            _r2 = _MemoCall("BANG", _index, BANG, null);

            if (_r2 != null) _index = _r2.NextIndex;

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label1; }

            // AND 4
            int _start_i4 = _index;

            // LOOK 5
            int _start_i5 = _index;

            // LITERAL '\"'
            _ParseLiteralChar(ref _index, '\"');

            // LOOK 5
            var _r5 = _results.Pop();
            _results.Push( _r5 != null ? new _Parser_Item(_start_i5, _input_enumerable) : null );
            _index = _start_i5;

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label4; }

            // CALLORVAR CSharpCode
            _Parser_Item _r8;

            _r8 = _MemoCall("CSharpCode", _index, CSharpCode, null);

            if (_r8 != null) _index = _r8.NextIndex;

            // BIND msg
            msg = _results.Peek();

        label4: // AND
            var _r4_2 = _results.Pop();
            var _r4_1 = _results.Pop();

            if (_r4_1 != null && _r4_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i4, _index, _input_enumerable, _r4_1.Results.Concat(_r4_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i4;
            }

            // QUES
            if (_results.Peek() == null) { _results.Pop(); _results.Push(new _Parser_Item(_index, _input_enumerable)); }

        label1: // AND
            var _r1_2 = _results.Pop();
            var _r1_1 = _results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i1, _index, _input_enumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i1;
            }

            // ACT
            var _r0 = _results.Peek();
            if (_r0 != null)
            {
                _results.Pop();
                _results.Push( new _Parser_Item(_r0.StartIndex, _r0.NextIndex, _input_enumerable, _Thunk(_IM_Result => { return new Fail<_Parser_Item>(msg); }, _r0), true) );
            }

        }


        public void SequenceExp(int _index, _Parser_Args _args)
        {

            // PLUS 1
            int _start_i1 = _index;
            var _res1 = Enumerable.Empty<ASTNode<_Parser_Item>>();
        label1:

            // CALLORVAR ConditionExp
            _Parser_Item _r2;

            _r2 = _MemoCall("ConditionExp", _index, ConditionExp, null);

            if (_r2 != null) _index = _r2.NextIndex;

            // PLUS 1
            var _r1 = _results.Pop();
            if (_r1 != null)
            {
                _res1 = _res1.Concat(_r1.Results);
                goto label1;
            }
            else
            {
                if (_index > _start_i1)
                    _results.Push(new _Parser_Item(_start_i1, _index, _input_enumerable, _res1.Where(_NON_NULL), true));
                else
                    _results.Push(null);
            }

            // ACT
            var _r0 = _results.Peek();
            if (_r0 != null)
            {
                _results.Pop();
                _results.Push( new _Parser_Item(_r0.StartIndex, _r0.NextIndex, _input_enumerable, _Thunk(_IM_Result => { return _IM_Result.Results
                    .Where(node => node != null)
                    .Aggregate<ASTNode<_Parser_Item>, ASTNode<_Parser_Item>>(null, (prev, cur) => prev != null ? new And<_Parser_Item>(prev, cur) : cur); }, _r0), true) );
            }

        }


        public void ConditionExp(int _index, _Parser_Args _args)
        {

            _Parser_Item body = null;
            _Parser_Item cond = null;

            // OR 0
            int _start_i0 = _index;

            // AND 2
            int _start_i2 = _index;

            // AND 3
            int _start_i3 = _index;

            // AND 4
            int _start_i4 = _index;

            // CALLORVAR BoundTerm
            _Parser_Item _r6;

            _r6 = _MemoCall("BoundTerm", _index, BoundTerm, null);

            if (_r6 != null) _index = _r6.NextIndex;

            // BIND body
            body = _results.Peek();

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label4; }

            // CALLORVAR QUES
            _Parser_Item _r7;

            _r7 = _MemoCall("QUES", _index, QUES, null);

            if (_r7 != null) _index = _r7.NextIndex;

        label4: // AND
            var _r4_2 = _results.Pop();
            var _r4_1 = _results.Pop();

            if (_r4_1 != null && _r4_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i4, _index, _input_enumerable, _r4_1.Results.Concat(_r4_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i4;
            }

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label3; }

            // LOOK 8
            int _start_i8 = _index;

            // CALLORVAR OPEN
            _Parser_Item _r9;

            _r9 = _MemoCall("OPEN", _index, OPEN, null);

            if (_r9 != null) _index = _r9.NextIndex;

            // LOOK 8
            var _r8 = _results.Pop();
            _results.Push( _r8 != null ? new _Parser_Item(_start_i8, _input_enumerable) : null );
            _index = _start_i8;

        label3: // AND
            var _r3_2 = _results.Pop();
            var _r3_1 = _results.Pop();

            if (_r3_1 != null && _r3_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i3, _index, _input_enumerable, _r3_1.Results.Concat(_r3_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i3;
            }

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label2; }

            // CALLORVAR CSharpCode
            _Parser_Item _r11;

            _r11 = _MemoCall("CSharpCode", _index, CSharpCode, null);

            if (_r11 != null) _index = _r11.NextIndex;

            // BIND cond
            cond = _results.Peek();

        label2: // AND
            var _r2_2 = _results.Pop();
            var _r2_1 = _results.Pop();

            if (_r2_1 != null && _r2_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i2, _index, _input_enumerable, _r2_1.Results.Concat(_r2_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i2;
            }

            // ACT
            var _r1 = _results.Peek();
            if (_r1 != null)
            {
                _results.Pop();
                _results.Push( new _Parser_Item(_r1.StartIndex, _r1.NextIndex, _input_enumerable, _Thunk(_IM_Result => { return new Cond<_Parser_Item>(body, cond); }, _r1), true) );
            }

            // OR shortcut
            if (_results.Peek() == null) { _results.Pop(); _index = _start_i0; } else goto label0;

            // CALLORVAR BoundTerm
            _Parser_Item _r12;

            _r12 = _MemoCall("BoundTerm", _index, BoundTerm, null);

            if (_r12 != null) _index = _r12.NextIndex;

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void BoundTerm(int _index, _Parser_Args _args)
        {

            _Parser_Item exp = null;
            _Parser_Item id = null;

            // OR 0
            int _start_i0 = _index;

            // OR 1
            int _start_i1 = _index;

            // AND 3
            int _start_i3 = _index;

            // AND 4
            int _start_i4 = _index;

            // CALLORVAR PrefixedTerm
            _Parser_Item _r6;

            _r6 = _MemoCall("PrefixedTerm", _index, PrefixedTerm, null);

            if (_r6 != null) _index = _r6.NextIndex;

            // BIND exp
            exp = _results.Peek();

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label4; }

            // CALLORVAR COLON
            _Parser_Item _r7;

            _r7 = _MemoCall("COLON", _index, COLON, null);

            if (_r7 != null) _index = _r7.NextIndex;

        label4: // AND
            var _r4_2 = _results.Pop();
            var _r4_1 = _results.Pop();

            if (_r4_1 != null && _r4_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i4, _index, _input_enumerable, _r4_1.Results.Concat(_r4_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i4;
            }

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label3; }

            // CALLORVAR Identifier
            _Parser_Item _r9;

            _r9 = _MemoCall("Identifier", _index, Identifier, null);

            if (_r9 != null) _index = _r9.NextIndex;

            // BIND id
            id = _results.Peek();

        label3: // AND
            var _r3_2 = _results.Pop();
            var _r3_1 = _results.Pop();

            if (_r3_1 != null && _r3_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i3, _index, _input_enumerable, _r3_1.Results.Concat(_r3_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i3;
            }

            // ACT
            var _r2 = _results.Peek();
            if (_r2 != null)
            {
                _results.Pop();
                _results.Push( new _Parser_Item(_r2.StartIndex, _r2.NextIndex, _input_enumerable, _Thunk(_IM_Result => { return new Bind<_Parser_Item>(exp, id); }, _r2), true) );
            }

            // OR shortcut
            if (_results.Peek() == null) { _results.Pop(); _index = _start_i1; } else goto label1;

            // AND 11
            int _start_i11 = _index;

            // CALLORVAR COLON
            _Parser_Item _r12;

            _r12 = _MemoCall("COLON", _index, COLON, null);

            if (_r12 != null) _index = _r12.NextIndex;

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label11; }

            // CALLORVAR Identifier
            _Parser_Item _r14;

            _r14 = _MemoCall("Identifier", _index, Identifier, null);

            if (_r14 != null) _index = _r14.NextIndex;

            // BIND id
            id = _results.Peek();

        label11: // AND
            var _r11_2 = _results.Pop();
            var _r11_1 = _results.Pop();

            if (_r11_1 != null && _r11_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i11, _index, _input_enumerable, _r11_1.Results.Concat(_r11_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i11;
            }

            // ACT
            var _r10 = _results.Peek();
            if (_r10 != null)
            {
                _results.Pop();
                _results.Push( new _Parser_Item(_r10.StartIndex, _r10.NextIndex, _input_enumerable, _Thunk(_IM_Result => { return new Bind<_Parser_Item>(new Any<_Parser_Item>(), id); }, _r10), true) );
            }

        label1: // OR
            int _dummy_i1 = _index; // no-op for label

            // OR shortcut
            if (_results.Peek() == null) { _results.Pop(); _index = _start_i0; } else goto label0;

            // CALLORVAR PrefixedTerm
            _Parser_Item _r15;

            _r15 = _MemoCall("PrefixedTerm", _index, PrefixedTerm, null);

            if (_r15 != null) _index = _r15.NextIndex;

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void PrefixedTerm(int _index, _Parser_Args _args)
        {

            // OR 0
            int _start_i0 = _index;

            // OR 1
            int _start_i1 = _index;

            // CALLORVAR LookTerm
            _Parser_Item _r2;

            _r2 = _MemoCall("LookTerm", _index, LookTerm, null);

            if (_r2 != null) _index = _r2.NextIndex;

            // OR shortcut
            if (_results.Peek() == null) { _results.Pop(); _index = _start_i1; } else goto label1;

            // CALLORVAR NotTerm
            _Parser_Item _r3;

            _r3 = _MemoCall("NotTerm", _index, NotTerm, null);

            if (_r3 != null) _index = _r3.NextIndex;

        label1: // OR
            int _dummy_i1 = _index; // no-op for label

            // OR shortcut
            if (_results.Peek() == null) { _results.Pop(); _index = _start_i0; } else goto label0;

            // CALLORVAR PostfixedTerm
            _Parser_Item _r4;

            _r4 = _MemoCall("PostfixedTerm", _index, PostfixedTerm, null);

            if (_r4 != null) _index = _r4.NextIndex;

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void LookTerm(int _index, _Parser_Args _args)
        {

            _Parser_Item exp = null;

            // AND 1
            int _start_i1 = _index;

            // CALLORVAR AND_PRE
            _Parser_Item _r2;

            _r2 = _MemoCall("AND_PRE", _index, AND_PRE, null);

            if (_r2 != null) _index = _r2.NextIndex;

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label1; }

            // CALLORVAR PostfixedTerm
            _Parser_Item _r4;

            _r4 = _MemoCall("PostfixedTerm", _index, PostfixedTerm, null);

            if (_r4 != null) _index = _r4.NextIndex;

            // BIND exp
            exp = _results.Peek();

        label1: // AND
            var _r1_2 = _results.Pop();
            var _r1_1 = _results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i1, _index, _input_enumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i1;
            }

            // ACT
            var _r0 = _results.Peek();
            if (_r0 != null)
            {
                _results.Pop();
                _results.Push( new _Parser_Item(_r0.StartIndex, _r0.NextIndex, _input_enumerable, _Thunk(_IM_Result => { return new Look<_Parser_Item>(exp); }, _r0), true) );
            }

        }


        public void NotTerm(int _index, _Parser_Args _args)
        {

            _Parser_Item exp = null;

            // AND 1
            int _start_i1 = _index;

            // CALLORVAR NOT_PRE
            _Parser_Item _r2;

            _r2 = _MemoCall("NOT_PRE", _index, NOT_PRE, null);

            if (_r2 != null) _index = _r2.NextIndex;

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label1; }

            // CALLORVAR PostfixedTerm
            _Parser_Item _r4;

            _r4 = _MemoCall("PostfixedTerm", _index, PostfixedTerm, null);

            if (_r4 != null) _index = _r4.NextIndex;

            // BIND exp
            exp = _results.Peek();

        label1: // AND
            var _r1_2 = _results.Pop();
            var _r1_1 = _results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i1, _index, _input_enumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i1;
            }

            // ACT
            var _r0 = _results.Peek();
            if (_r0 != null)
            {
                _results.Pop();
                _results.Push( new _Parser_Item(_r0.StartIndex, _r0.NextIndex, _input_enumerable, _Thunk(_IM_Result => { return new Not<_Parser_Item>(exp); }, _r0), true) );
            }

        }


        public void PostfixedTerm(int _index, _Parser_Args _args)
        {

            // OR 0
            int _start_i0 = _index;

            // OR 1
            int _start_i1 = _index;

            // OR 2
            int _start_i2 = _index;

            // CALLORVAR StarTerm
            _Parser_Item _r3;

            _r3 = _MemoCall("StarTerm", _index, StarTerm, null);

            if (_r3 != null) _index = _r3.NextIndex;

            // OR shortcut
            if (_results.Peek() == null) { _results.Pop(); _index = _start_i2; } else goto label2;

            // CALLORVAR PlusTerm
            _Parser_Item _r4;

            _r4 = _MemoCall("PlusTerm", _index, PlusTerm, null);

            if (_r4 != null) _index = _r4.NextIndex;

        label2: // OR
            int _dummy_i2 = _index; // no-op for label

            // OR shortcut
            if (_results.Peek() == null) { _results.Pop(); _index = _start_i1; } else goto label1;

            // CALLORVAR QuesTerm
            _Parser_Item _r5;

            _r5 = _MemoCall("QuesTerm", _index, QuesTerm, null);

            if (_r5 != null) _index = _r5.NextIndex;

        label1: // OR
            int _dummy_i1 = _index; // no-op for label

            // OR shortcut
            if (_results.Peek() == null) { _results.Pop(); _index = _start_i0; } else goto label0;

            // CALLORVAR Term
            _Parser_Item _r6;

            _r6 = _MemoCall("Term", _index, Term, null);

            if (_r6 != null) _index = _r6.NextIndex;

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void StarTerm(int _index, _Parser_Args _args)
        {

            _Parser_Item exp = null;

            // AND 1
            int _start_i1 = _index;

            // CALLORVAR Term
            _Parser_Item _r3;

            _r3 = _MemoCall("Term", _index, Term, null);

            if (_r3 != null) _index = _r3.NextIndex;

            // BIND exp
            exp = _results.Peek();

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label1; }

            // CALLORVAR STAR
            _Parser_Item _r4;

            _r4 = _MemoCall("STAR", _index, STAR, null);

            if (_r4 != null) _index = _r4.NextIndex;

        label1: // AND
            var _r1_2 = _results.Pop();
            var _r1_1 = _results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i1, _index, _input_enumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i1;
            }

            // ACT
            var _r0 = _results.Peek();
            if (_r0 != null)
            {
                _results.Pop();
                _results.Push( new _Parser_Item(_r0.StartIndex, _r0.NextIndex, _input_enumerable, _Thunk(_IM_Result => { return new Star<_Parser_Item>(exp); }, _r0), true) );
            }

        }


        public void PlusTerm(int _index, _Parser_Args _args)
        {

            _Parser_Item exp = null;

            // AND 1
            int _start_i1 = _index;

            // CALLORVAR Term
            _Parser_Item _r3;

            _r3 = _MemoCall("Term", _index, Term, null);

            if (_r3 != null) _index = _r3.NextIndex;

            // BIND exp
            exp = _results.Peek();

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label1; }

            // CALLORVAR PLUS
            _Parser_Item _r4;

            _r4 = _MemoCall("PLUS", _index, PLUS, null);

            if (_r4 != null) _index = _r4.NextIndex;

        label1: // AND
            var _r1_2 = _results.Pop();
            var _r1_1 = _results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i1, _index, _input_enumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i1;
            }

            // ACT
            var _r0 = _results.Peek();
            if (_r0 != null)
            {
                _results.Pop();
                _results.Push( new _Parser_Item(_r0.StartIndex, _r0.NextIndex, _input_enumerable, _Thunk(_IM_Result => { return new Plus<_Parser_Item>(exp); }, _r0), true) );
            }

        }


        public void QuesTerm(int _index, _Parser_Args _args)
        {

            _Parser_Item exp = null;

            // AND 1
            int _start_i1 = _index;

            // AND 2
            int _start_i2 = _index;

            // AND 3
            int _start_i3 = _index;

            // CALLORVAR Term
            _Parser_Item _r5;

            _r5 = _MemoCall("Term", _index, Term, null);

            if (_r5 != null) _index = _r5.NextIndex;

            // BIND exp
            exp = _results.Peek();

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label3; }

            // CALLORVAR QUES
            _Parser_Item _r6;

            _r6 = _MemoCall("QUES", _index, QUES, null);

            if (_r6 != null) _index = _r6.NextIndex;

        label3: // AND
            var _r3_2 = _results.Pop();
            var _r3_1 = _results.Pop();

            if (_r3_1 != null && _r3_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i3, _index, _input_enumerable, _r3_1.Results.Concat(_r3_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i3;
            }

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label2; }

            // NOT 7
            int _start_i7 = _index;

            // CALLORVAR OPEN
            _Parser_Item _r8;

            _r8 = _MemoCall("OPEN", _index, OPEN, null);

            if (_r8 != null) _index = _r8.NextIndex;

            // NOT 7
            var _r7 = _results.Pop();
            _results.Push( _r7 == null ? new _Parser_Item(_start_i7, _input_enumerable) : null);
            _index = _start_i7;

        label2: // AND
            var _r2_2 = _results.Pop();
            var _r2_1 = _results.Pop();

            if (_r2_1 != null && _r2_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i2, _index, _input_enumerable, _r2_1.Results.Concat(_r2_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i2;
            }

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label1; }

            // CALLORVAR SP
            _Parser_Item _r9;

            _r9 = _MemoCall("SP", _index, SP, null);

            if (_r9 != null) _index = _r9.NextIndex;

        label1: // AND
            var _r1_2 = _results.Pop();
            var _r1_1 = _results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i1, _index, _input_enumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i1;
            }

            // ACT
            var _r0 = _results.Peek();
            if (_r0 != null)
            {
                _results.Pop();
                _results.Push( new _Parser_Item(_r0.StartIndex, _r0.NextIndex, _input_enumerable, _Thunk(_IM_Result => { return new Ques<_Parser_Item>(exp); }, _r0), true) );
            }

        }


        public void Term(int _index, _Parser_Args _args)
        {

            // OR 0
            int _start_i0 = _index;

            // OR 1
            int _start_i1 = _index;

            // OR 2
            int _start_i2 = _index;

            // OR 3
            int _start_i3 = _index;

            // OR 4
            int _start_i4 = _index;

            // CALLORVAR InputClass
            _Parser_Item _r5;

            _r5 = _MemoCall("InputClass", _index, InputClass, null);

            if (_r5 != null) _index = _r5.NextIndex;

            // OR shortcut
            if (_results.Peek() == null) { _results.Pop(); _index = _start_i4; } else goto label4;

            // CALLORVAR ParenTerm
            _Parser_Item _r6;

            _r6 = _MemoCall("ParenTerm", _index, ParenTerm, null);

            if (_r6 != null) _index = _r6.NextIndex;

        label4: // OR
            int _dummy_i4 = _index; // no-op for label

            // OR shortcut
            if (_results.Peek() == null) { _results.Pop(); _index = _start_i3; } else goto label3;

            // CALLORVAR RuleCall
            _Parser_Item _r7;

            _r7 = _MemoCall("RuleCall", _index, RuleCall, null);

            if (_r7 != null) _index = _r7.NextIndex;

        label3: // OR
            int _dummy_i3 = _index; // no-op for label

            // OR shortcut
            if (_results.Peek() == null) { _results.Pop(); _index = _start_i2; } else goto label2;

            // CALLORVAR CallOrVar
            _Parser_Item _r8;

            _r8 = _MemoCall("CallOrVar", _index, CallOrVar, null);

            if (_r8 != null) _index = _r8.NextIndex;

        label2: // OR
            int _dummy_i2 = _index; // no-op for label

            // OR shortcut
            if (_results.Peek() == null) { _results.Pop(); _index = _start_i1; } else goto label1;

            // CALLORVAR Literal
            _Parser_Item _r9;

            _r9 = _MemoCall("Literal", _index, Literal, null);

            if (_r9 != null) _index = _r9.NextIndex;

        label1: // OR
            int _dummy_i1 = _index; // no-op for label

            // OR shortcut
            if (_results.Peek() == null) { _results.Pop(); _index = _start_i0; } else goto label0;

            // CALLORVAR AnyTerm
            _Parser_Item _r10;

            _r10 = _MemoCall("AnyTerm", _index, AnyTerm, null);

            if (_r10 != null) _index = _r10.NextIndex;

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void ParenTerm(int _index, _Parser_Args _args)
        {

            _Parser_Item exp = null;

            // AND 1
            int _start_i1 = _index;

            // AND 2
            int _start_i2 = _index;

            // CALLORVAR OPEN
            _Parser_Item _r3;

            _r3 = _MemoCall("OPEN", _index, OPEN, null);

            if (_r3 != null) _index = _r3.NextIndex;

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label2; }

            // CALLORVAR Disjunction
            _Parser_Item _r5;

            _r5 = _MemoCall("Disjunction", _index, Disjunction, null);

            if (_r5 != null) _index = _r5.NextIndex;

            // BIND exp
            exp = _results.Peek();

        label2: // AND
            var _r2_2 = _results.Pop();
            var _r2_1 = _results.Pop();

            if (_r2_1 != null && _r2_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i2, _index, _input_enumerable, _r2_1.Results.Concat(_r2_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i2;
            }

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label1; }

            // CALLORVAR CLOSE
            _Parser_Item _r6;

            _r6 = _MemoCall("CLOSE", _index, CLOSE, null);

            if (_r6 != null) _index = _r6.NextIndex;

        label1: // AND
            var _r1_2 = _results.Pop();
            var _r1_1 = _results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i1, _index, _input_enumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i1;
            }

            // ACT
            var _r0 = _results.Peek();
            if (_r0 != null)
            {
                _results.Pop();
                _results.Push( new _Parser_Item(_r0.StartIndex, _r0.NextIndex, _input_enumerable, _Thunk(_IM_Result => { return exp; }, _r0), true) );
            }

        }


        public void AnyTerm(int _index, _Parser_Args _args)
        {

            // CALLORVAR PERIOD
            _Parser_Item _r1;

            _r1 = _MemoCall("PERIOD", _index, PERIOD, null);

            if (_r1 != null) _index = _r1.NextIndex;

            // ACT
            var _r0 = _results.Peek();
            if (_r0 != null)
            {
                _results.Pop();
                _results.Push( new _Parser_Item(_r0.StartIndex, _r0.NextIndex, _input_enumerable, _Thunk(_IM_Result => { return new Any<_Parser_Item>(); }, _r0), true) );
            }

        }


        public void RuleCall(int _index, _Parser_Args _args)
        {

            _Parser_Item name = null;
            _Parser_Item p = null;

            // AND 1
            int _start_i1 = _index;

            // AND 2
            int _start_i2 = _index;

            // AND 3
            int _start_i3 = _index;

            // CALLORVAR Ident
            _Parser_Item _r5;

            _r5 = _MemoCall("Ident", _index, Ident, null);

            if (_r5 != null) _index = _r5.NextIndex;

            // BIND name
            name = _results.Peek();

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label3; }

            // CALLORVAR OPEN
            _Parser_Item _r6;

            _r6 = _MemoCall("OPEN", _index, OPEN, null);

            if (_r6 != null) _index = _r6.NextIndex;

        label3: // AND
            var _r3_2 = _results.Pop();
            var _r3_1 = _results.Pop();

            if (_r3_1 != null && _r3_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i3, _index, _input_enumerable, _r3_1.Results.Concat(_r3_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i3;
            }

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label2; }

            // CALLORVAR ParameterList
            _Parser_Item _r9;

            _r9 = _MemoCall("ParameterList", _index, ParameterList, null);

            if (_r9 != null) _index = _r9.NextIndex;

            // QUES
            if (_results.Peek() == null) { _results.Pop(); _results.Push(new _Parser_Item(_index, _input_enumerable)); }

            // BIND p
            p = _results.Peek();

        label2: // AND
            var _r2_2 = _results.Pop();
            var _r2_1 = _results.Pop();

            if (_r2_1 != null && _r2_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i2, _index, _input_enumerable, _r2_1.Results.Concat(_r2_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i2;
            }

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label1; }

            // CALLORVAR CLOSE
            _Parser_Item _r10;

            _r10 = _MemoCall("CLOSE", _index, CLOSE, null);

            if (_r10 != null) _index = _r10.NextIndex;

        label1: // AND
            var _r1_2 = _results.Pop();
            var _r1_1 = _results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i1, _index, _input_enumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i1;
            }

            // ACT
            var _r0 = _results.Peek();
            if (_r0 != null)
            {
                _results.Pop();
                _results.Push( new _Parser_Item(_r0.StartIndex, _r0.NextIndex, _input_enumerable, _Thunk(_IM_Result => { return new Call<_Parser_Item>(name, p.Results); }, _r0), true) );
            }

        }


        public void ParameterList(int _index, _Parser_Args _args)
        {

            // AND 1
            int _start_i1 = _index;

            // CALLORVAR Parameter
            _Parser_Item _r2;

            _r2 = _MemoCall("Parameter", _index, Parameter, null);

            if (_r2 != null) _index = _r2.NextIndex;

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label1; }

            // STAR 3
            int _start_i3 = _index;
            var _res3 = Enumerable.Empty<ASTNode<_Parser_Item>>();
        label3:

            // AND 4
            int _start_i4 = _index;

            // CALLORVAR COMMA
            _Parser_Item _r5;

            _r5 = _MemoCall("COMMA", _index, COMMA, null);

            if (_r5 != null) _index = _r5.NextIndex;

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label4; }

            // CALLORVAR Parameter
            _Parser_Item _r6;

            _r6 = _MemoCall("Parameter", _index, Parameter, null);

            if (_r6 != null) _index = _r6.NextIndex;

        label4: // AND
            var _r4_2 = _results.Pop();
            var _r4_1 = _results.Pop();

            if (_r4_1 != null && _r4_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i4, _index, _input_enumerable, _r4_1.Results.Concat(_r4_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i4;
            }

            // STAR 3
            var _r3 = _results.Pop();
            if (_r3 != null)
            {
                _res3 = _res3.Concat(_r3.Results);
                goto label3;
            }
            else
            {
                _results.Push(new _Parser_Item(_start_i3, _index, _input_enumerable, _res3.Where(_NON_NULL), true));
            }

        label1: // AND
            var _r1_2 = _results.Pop();
            var _r1_1 = _results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i1, _index, _input_enumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i1;
            }

            // ACT
            var _r0 = _results.Peek();
            if (_r0 != null)
            {
                _results.Pop();
                _results.Push( new _Parser_Item(_r0.StartIndex, _r0.NextIndex, _input_enumerable, _Thunk(_IM_Result => { return _IM_Result.Results.Where(node => node != null); }, _r0), true) );
            }

        }


        public void Parameter(int _index, _Parser_Args _args)
        {

            // OR 0
            int _start_i0 = _index;

            // CALLORVAR CallOrVar
            _Parser_Item _r1;

            _r1 = _MemoCall("CallOrVar", _index, CallOrVar, null);

            if (_r1 != null) _index = _r1.NextIndex;

            // OR shortcut
            if (_results.Peek() == null) { _results.Pop(); _index = _start_i0; } else goto label0;

            // CALLORVAR Literal
            _Parser_Item _r2;

            _r2 = _MemoCall("Literal", _index, Literal, null);

            if (_r2 != null) _index = _r2.NextIndex;

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void CallOrVar(int _index, _Parser_Args _args)
        {

            _Parser_Item id = null;

            // CALLORVAR Identifier
            _Parser_Item _r2;

            _r2 = _MemoCall("Identifier", _index, Identifier, null);

            if (_r2 != null) _index = _r2.NextIndex;

            // BIND id
            id = _results.Peek();

            // ACT
            var _r0 = _results.Peek();
            if (_r0 != null)
            {
                _results.Pop();
                _results.Push( new _Parser_Item(_r0.StartIndex, _r0.NextIndex, _input_enumerable, _Thunk(_IM_Result => { return new CallOrVar<_Parser_Item>(id); }, _r0), true) );
            }

        }


        public void Literal(int _index, _Parser_Args _args)
        {

            _Parser_Item code = null;

            // AND 1
            int _start_i1 = _index;

            // LOOK 2
            int _start_i2 = _index;

            // OR 3
            int _start_i3 = _index;

            // OR 4
            int _start_i4 = _index;

            // LITERAL '\x22'
            _ParseLiteralChar(ref _index, '\x22');

            // OR shortcut
            if (_results.Peek() == null) { _results.Pop(); _index = _start_i4; } else goto label4;

            // LITERAL '\x27'
            _ParseLiteralChar(ref _index, '\x27');

        label4: // OR
            int _dummy_i4 = _index; // no-op for label

            // OR shortcut
            if (_results.Peek() == null) { _results.Pop(); _index = _start_i3; } else goto label3;

            // LITERAL '{'
            _ParseLiteralChar(ref _index, '{');

        label3: // OR
            int _dummy_i3 = _index; // no-op for label

            // LOOK 2
            var _r2 = _results.Pop();
            _results.Push( _r2 != null ? new _Parser_Item(_start_i2, _input_enumerable) : null );
            _index = _start_i2;

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label1; }

            // CALLORVAR CSharpCode
            _Parser_Item _r9;

            _r9 = _MemoCall("CSharpCode", _index, CSharpCode, null);

            if (_r9 != null) _index = _r9.NextIndex;

            // BIND code
            code = _results.Peek();

        label1: // AND
            var _r1_2 = _results.Pop();
            var _r1_1 = _results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i1, _index, _input_enumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i1;
            }

            // ACT
            var _r0 = _results.Peek();
            if (_r0 != null)
            {
                _results.Pop();
                _results.Push( new _Parser_Item(_r0.StartIndex, _r0.NextIndex, _input_enumerable, _Thunk(_IM_Result => { return code; }, _r0), true) );
            }

        }


        public void InputClass(int _index, _Parser_Args _args)
        {

            _Parser_Item c = null;

            // AND 1
            int _start_i1 = _index;

            // AND 2
            int _start_i2 = _index;

            // AND 3
            int _start_i3 = _index;

            // AND 4
            int _start_i4 = _index;

            // LITERAL '['
            _ParseLiteralChar(ref _index, '[');

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label4; }

            // CALLORVAR SP
            _Parser_Item _r6;

            _r6 = _MemoCall("SP", _index, SP, null);

            if (_r6 != null) _index = _r6.NextIndex;

        label4: // AND
            var _r4_2 = _results.Pop();
            var _r4_1 = _results.Pop();

            if (_r4_1 != null && _r4_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i4, _index, _input_enumerable, _r4_1.Results.Concat(_r4_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i4;
            }

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label3; }

            // PLUS 8
            int _start_i8 = _index;
            var _res8 = Enumerable.Empty<ASTNode<_Parser_Item>>();
        label8:

            // OR 9
            int _start_i9 = _index;

            // CALLORVAR ClassRange
            _Parser_Item _r10;

            _r10 = _MemoCall("ClassRange", _index, ClassRange, null);

            if (_r10 != null) _index = _r10.NextIndex;

            // OR shortcut
            if (_results.Peek() == null) { _results.Pop(); _index = _start_i9; } else goto label9;

            // AND 11
            int _start_i11 = _index;

            // LOOK 12
            int _start_i12 = _index;

            // LITERAL '\x27'
            _ParseLiteralChar(ref _index, '\x27');

            // LOOK 12
            var _r12 = _results.Pop();
            _results.Push( _r12 != null ? new _Parser_Item(_start_i12, _input_enumerable) : null );
            _index = _start_i12;

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label11; }

            // CALLORVAR Literal
            _Parser_Item _r14;

            _r14 = _MemoCall("Literal", _index, Literal, null);

            if (_r14 != null) _index = _r14.NextIndex;

        label11: // AND
            var _r11_2 = _results.Pop();
            var _r11_1 = _results.Pop();

            if (_r11_1 != null && _r11_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i11, _index, _input_enumerable, _r11_1.Results.Concat(_r11_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i11;
            }

        label9: // OR
            int _dummy_i9 = _index; // no-op for label

            // PLUS 8
            var _r8 = _results.Pop();
            if (_r8 != null)
            {
                _res8 = _res8.Concat(_r8.Results);
                goto label8;
            }
            else
            {
                if (_index > _start_i8)
                    _results.Push(new _Parser_Item(_start_i8, _index, _input_enumerable, _res8.Where(_NON_NULL), true));
                else
                    _results.Push(null);
            }

            // BIND c
            c = _results.Peek();

        label3: // AND
            var _r3_2 = _results.Pop();
            var _r3_1 = _results.Pop();

            if (_r3_1 != null && _r3_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i3, _index, _input_enumerable, _r3_1.Results.Concat(_r3_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i3;
            }

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label2; }

            // LITERAL ']'
            _ParseLiteralChar(ref _index, ']');

        label2: // AND
            var _r2_2 = _results.Pop();
            var _r2_1 = _results.Pop();

            if (_r2_1 != null && _r2_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i2, _index, _input_enumerable, _r2_1.Results.Concat(_r2_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i2;
            }

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label1; }

            // CALLORVAR SP
            _Parser_Item _r16;

            _r16 = _MemoCall("SP", _index, SP, null);

            if (_r16 != null) _index = _r16.NextIndex;

        label1: // AND
            var _r1_2 = _results.Pop();
            var _r1_1 = _results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i1, _index, _input_enumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i1;
            }

            // ACT
            var _r0 = _results.Peek();
            if (_r0 != null)
            {
                _results.Pop();
                _results.Push( new _Parser_Item(_r0.StartIndex, _r0.NextIndex, _input_enumerable, _Thunk(_IM_Result => { return new InputClass<_Parser_Item>(c.Results); }, _r0), true) );
            }

        }


        public void ClassRange(int _index, _Parser_Args _args)
        {

            _Parser_Item from = null;
            _Parser_Item to = null;

            // AND 1
            int _start_i1 = _index;

            // AND 2
            int _start_i2 = _index;

            // AND 3
            int _start_i3 = _index;

            // AND 4
            int _start_i4 = _index;

            // AND 5
            int _start_i5 = _index;

            // AND 6
            int _start_i6 = _index;

            // AND 7
            int _start_i7 = _index;

            // LOOK 8
            int _start_i8 = _index;

            // LITERAL '\x27'
            _ParseLiteralChar(ref _index, '\x27');

            // LOOK 8
            var _r8 = _results.Pop();
            _results.Push( _r8 != null ? new _Parser_Item(_start_i8, _input_enumerable) : null );
            _index = _start_i8;

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label7; }

            // CALLORVAR Literal
            _Parser_Item _r11;

            _r11 = _MemoCall("Literal", _index, Literal, null);

            if (_r11 != null) _index = _r11.NextIndex;

            // BIND from
            from = _results.Peek();

        label7: // AND
            var _r7_2 = _results.Pop();
            var _r7_1 = _results.Pop();

            if (_r7_1 != null && _r7_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i7, _index, _input_enumerable, _r7_1.Results.Concat(_r7_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i7;
            }

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label6; }

            // CALLORVAR SP
            _Parser_Item _r12;

            _r12 = _MemoCall("SP", _index, SP, null);

            if (_r12 != null) _index = _r12.NextIndex;

        label6: // AND
            var _r6_2 = _results.Pop();
            var _r6_1 = _results.Pop();

            if (_r6_1 != null && _r6_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i6, _index, _input_enumerable, _r6_1.Results.Concat(_r6_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i6;
            }

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label5; }

            // LITERAL '-'
            _ParseLiteralChar(ref _index, '-');

        label5: // AND
            var _r5_2 = _results.Pop();
            var _r5_1 = _results.Pop();

            if (_r5_1 != null && _r5_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i5, _index, _input_enumerable, _r5_1.Results.Concat(_r5_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i5;
            }

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label4; }

            // CALLORVAR SP
            _Parser_Item _r14;

            _r14 = _MemoCall("SP", _index, SP, null);

            if (_r14 != null) _index = _r14.NextIndex;

        label4: // AND
            var _r4_2 = _results.Pop();
            var _r4_1 = _results.Pop();

            if (_r4_1 != null && _r4_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i4, _index, _input_enumerable, _r4_1.Results.Concat(_r4_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i4;
            }

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label3; }

            // LOOK 15
            int _start_i15 = _index;

            // LITERAL '\x27'
            _ParseLiteralChar(ref _index, '\x27');

            // LOOK 15
            var _r15 = _results.Pop();
            _results.Push( _r15 != null ? new _Parser_Item(_start_i15, _input_enumerable) : null );
            _index = _start_i15;

        label3: // AND
            var _r3_2 = _results.Pop();
            var _r3_1 = _results.Pop();

            if (_r3_1 != null && _r3_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i3, _index, _input_enumerable, _r3_1.Results.Concat(_r3_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i3;
            }

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label2; }

            // CALLORVAR Literal
            _Parser_Item _r18;

            _r18 = _MemoCall("Literal", _index, Literal, null);

            if (_r18 != null) _index = _r18.NextIndex;

            // BIND to
            to = _results.Peek();

        label2: // AND
            var _r2_2 = _results.Pop();
            var _r2_1 = _results.Pop();

            if (_r2_1 != null && _r2_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i2, _index, _input_enumerable, _r2_1.Results.Concat(_r2_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i2;
            }

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label1; }

            // CALLORVAR SP
            _Parser_Item _r19;

            _r19 = _MemoCall("SP", _index, SP, null);

            if (_r19 != null) _index = _r19.NextIndex;

        label1: // AND
            var _r1_2 = _results.Pop();
            var _r1_1 = _results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i1, _index, _input_enumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i1;
            }

            // ACT
            var _r0 = _results.Peek();
            if (_r0 != null)
            {
                _results.Pop();
                _results.Push( new _Parser_Item(_r0.StartIndex, _r0.NextIndex, _input_enumerable, _Thunk(_IM_Result => { char ch_from = AST.ClassRange<_Parser_Item>.GetChar(from);
            char ch_to = AST.ClassRange<_Parser_Item>.GetChar(to);

            List<char> range = new List<char>();
            if (ch_from > ch_to)                
            {
                range.Add(ch_from);
                range.Add(ch_to);
            }
            else
            {
                for (char ch = ch_from ; ch <= ch_to; ++ch)
                    range.Add(ch);
            }

            return new ClassRange<_Parser_Item>(_IM_Result, range); }, _r0), true) );
            }

        }


        public void CSharpCode(int _index, _Parser_Args _args)
        {

            _Parser_Item code = null;

            // AND 1
            int _start_i1 = _index;

            // CALLORVAR CSharpCodeItem
            _Parser_Item _r3;

            _r3 = _MemoCall("CSharpCodeItem", _index, CSharpCodeItem, null);

            if (_r3 != null) _index = _r3.NextIndex;

            // BIND code
            code = _results.Peek();

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label1; }

            // CALLORVAR SP
            _Parser_Item _r4;

            _r4 = _MemoCall("SP", _index, SP, null);

            if (_r4 != null) _index = _r4.NextIndex;

        label1: // AND
            var _r1_2 = _results.Pop();
            var _r1_1 = _results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i1, _index, _input_enumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i1;
            }

            // ACT
            var _r0 = _results.Peek();
            if (_r0 != null)
            {
                _results.Pop();
                _results.Push( new _Parser_Item(_r0.StartIndex, _r0.NextIndex, _input_enumerable, _Thunk(_IM_Result => { return new Code<_Parser_Item>(code); }, _r0), true) );
            }

        }


        public void CSharpCodeItem(int _index, _Parser_Args _args)
        {

            // OR 0
            int _start_i0 = _index;

            // OR 1
            int _start_i1 = _index;

            // OR 2
            int _start_i2 = _index;

            // AND 3
            int _start_i3 = _index;

            // AND 4
            int _start_i4 = _index;

            // LITERAL '{'
            _ParseLiteralChar(ref _index, '{');

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label4; }

            // STAR 6
            int _start_i6 = _index;
            var _res6 = Enumerable.Empty<ASTNode<_Parser_Item>>();
        label6:

            // AND 7
            int _start_i7 = _index;

            // NOT 8
            int _start_i8 = _index;

            // LITERAL '}'
            _ParseLiteralChar(ref _index, '}');

            // NOT 8
            var _r8 = _results.Pop();
            _results.Push( _r8 == null ? new _Parser_Item(_start_i8, _input_enumerable) : null);
            _index = _start_i8;

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label7; }

            // OR 10
            int _start_i10 = _index;

            // OR 11
            int _start_i11 = _index;

            // CALLORVAR Comment
            _Parser_Item _r12;

            _r12 = _MemoCall("Comment", _index, Comment, null);

            if (_r12 != null) _index = _r12.NextIndex;

            // OR shortcut
            if (_results.Peek() == null) { _results.Pop(); _index = _start_i11; } else goto label11;

            // CALLORVAR CSharpCodeItem
            _Parser_Item _r13;

            _r13 = _MemoCall("CSharpCodeItem", _index, CSharpCodeItem, null);

            if (_r13 != null) _index = _r13.NextIndex;

        label11: // OR
            int _dummy_i11 = _index; // no-op for label

            // OR shortcut
            if (_results.Peek() == null) { _results.Pop(); _index = _start_i10; } else goto label10;

            // ANY
            _ParseAny(ref _index);

        label10: // OR
            int _dummy_i10 = _index; // no-op for label

        label7: // AND
            var _r7_2 = _results.Pop();
            var _r7_1 = _results.Pop();

            if (_r7_1 != null && _r7_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i7, _index, _input_enumerable, _r7_1.Results.Concat(_r7_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i7;
            }

            // STAR 6
            var _r6 = _results.Pop();
            if (_r6 != null)
            {
                _res6 = _res6.Concat(_r6.Results);
                goto label6;
            }
            else
            {
                _results.Push(new _Parser_Item(_start_i6, _index, _input_enumerable, _res6.Where(_NON_NULL), true));
            }

        label4: // AND
            var _r4_2 = _results.Pop();
            var _r4_1 = _results.Pop();

            if (_r4_1 != null && _r4_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i4, _index, _input_enumerable, _r4_1.Results.Concat(_r4_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i4;
            }

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label3; }

            // LITERAL '}'
            _ParseLiteralChar(ref _index, '}');

        label3: // AND
            var _r3_2 = _results.Pop();
            var _r3_1 = _results.Pop();

            if (_r3_1 != null && _r3_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i3, _index, _input_enumerable, _r3_1.Results.Concat(_r3_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i3;
            }

            // OR shortcut
            if (_results.Peek() == null) { _results.Pop(); _index = _start_i2; } else goto label2;

            // AND 16
            int _start_i16 = _index;

            // AND 17
            int _start_i17 = _index;

            // LITERAL '('
            _ParseLiteralChar(ref _index, '(');

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label17; }

            // STAR 19
            int _start_i19 = _index;
            var _res19 = Enumerable.Empty<ASTNode<_Parser_Item>>();
        label19:

            // AND 20
            int _start_i20 = _index;

            // NOT 21
            int _start_i21 = _index;

            // LITERAL ')'
            _ParseLiteralChar(ref _index, ')');

            // NOT 21
            var _r21 = _results.Pop();
            _results.Push( _r21 == null ? new _Parser_Item(_start_i21, _input_enumerable) : null);
            _index = _start_i21;

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label20; }

            // OR 23
            int _start_i23 = _index;

            // OR 24
            int _start_i24 = _index;

            // CALLORVAR Comment
            _Parser_Item _r25;

            _r25 = _MemoCall("Comment", _index, Comment, null);

            if (_r25 != null) _index = _r25.NextIndex;

            // OR shortcut
            if (_results.Peek() == null) { _results.Pop(); _index = _start_i24; } else goto label24;

            // CALLORVAR CSharpCodeItem
            _Parser_Item _r26;

            _r26 = _MemoCall("CSharpCodeItem", _index, CSharpCodeItem, null);

            if (_r26 != null) _index = _r26.NextIndex;

        label24: // OR
            int _dummy_i24 = _index; // no-op for label

            // OR shortcut
            if (_results.Peek() == null) { _results.Pop(); _index = _start_i23; } else goto label23;

            // ANY
            _ParseAny(ref _index);

        label23: // OR
            int _dummy_i23 = _index; // no-op for label

        label20: // AND
            var _r20_2 = _results.Pop();
            var _r20_1 = _results.Pop();

            if (_r20_1 != null && _r20_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i20, _index, _input_enumerable, _r20_1.Results.Concat(_r20_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i20;
            }

            // STAR 19
            var _r19 = _results.Pop();
            if (_r19 != null)
            {
                _res19 = _res19.Concat(_r19.Results);
                goto label19;
            }
            else
            {
                _results.Push(new _Parser_Item(_start_i19, _index, _input_enumerable, _res19.Where(_NON_NULL), true));
            }

        label17: // AND
            var _r17_2 = _results.Pop();
            var _r17_1 = _results.Pop();

            if (_r17_1 != null && _r17_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i17, _index, _input_enumerable, _r17_1.Results.Concat(_r17_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i17;
            }

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label16; }

            // LITERAL ')'
            _ParseLiteralChar(ref _index, ')');

        label16: // AND
            var _r16_2 = _results.Pop();
            var _r16_1 = _results.Pop();

            if (_r16_1 != null && _r16_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i16, _index, _input_enumerable, _r16_1.Results.Concat(_r16_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i16;
            }

        label2: // OR
            int _dummy_i2 = _index; // no-op for label

            // OR shortcut
            if (_results.Peek() == null) { _results.Pop(); _index = _start_i1; } else goto label1;

            // AND 29
            int _start_i29 = _index;

            // AND 30
            int _start_i30 = _index;

            // LITERAL '\x27'
            _ParseLiteralChar(ref _index, '\x27');

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label30; }

            // STAR 32
            int _start_i32 = _index;
            var _res32 = Enumerable.Empty<ASTNode<_Parser_Item>>();
        label32:

            // OR 33
            int _start_i33 = _index;

            // OR 34
            int _start_i34 = _index;

            // LITERAL "\x5c\x27"
            _ParseLiteralString(ref _index, "\x5c\x27");

            // OR shortcut
            if (_results.Peek() == null) { _results.Pop(); _index = _start_i34; } else goto label34;

            // LITERAL "\x5c\x5c"
            _ParseLiteralString(ref _index, "\x5c\x5c");

        label34: // OR
            int _dummy_i34 = _index; // no-op for label

            // OR shortcut
            if (_results.Peek() == null) { _results.Pop(); _index = _start_i33; } else goto label33;

            // AND 37
            int _start_i37 = _index;

            // NOT 38
            int _start_i38 = _index;

            // LITERAL '\x27'
            _ParseLiteralChar(ref _index, '\x27');

            // NOT 38
            var _r38 = _results.Pop();
            _results.Push( _r38 == null ? new _Parser_Item(_start_i38, _input_enumerable) : null);
            _index = _start_i38;

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label37; }

            // ANY
            _ParseAny(ref _index);

        label37: // AND
            var _r37_2 = _results.Pop();
            var _r37_1 = _results.Pop();

            if (_r37_1 != null && _r37_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i37, _index, _input_enumerable, _r37_1.Results.Concat(_r37_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i37;
            }

        label33: // OR
            int _dummy_i33 = _index; // no-op for label

            // STAR 32
            var _r32 = _results.Pop();
            if (_r32 != null)
            {
                _res32 = _res32.Concat(_r32.Results);
                goto label32;
            }
            else
            {
                _results.Push(new _Parser_Item(_start_i32, _index, _input_enumerable, _res32.Where(_NON_NULL), true));
            }

        label30: // AND
            var _r30_2 = _results.Pop();
            var _r30_1 = _results.Pop();

            if (_r30_1 != null && _r30_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i30, _index, _input_enumerable, _r30_1.Results.Concat(_r30_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i30;
            }

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label29; }

            // LITERAL '\x27'
            _ParseLiteralChar(ref _index, '\x27');

        label29: // AND
            var _r29_2 = _results.Pop();
            var _r29_1 = _results.Pop();

            if (_r29_1 != null && _r29_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i29, _index, _input_enumerable, _r29_1.Results.Concat(_r29_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i29;
            }

        label1: // OR
            int _dummy_i1 = _index; // no-op for label

            // OR shortcut
            if (_results.Peek() == null) { _results.Pop(); _index = _start_i0; } else goto label0;

            // AND 42
            int _start_i42 = _index;

            // AND 43
            int _start_i43 = _index;

            // LITERAL '\x22'
            _ParseLiteralChar(ref _index, '\x22');

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label43; }

            // STAR 45
            int _start_i45 = _index;
            var _res45 = Enumerable.Empty<ASTNode<_Parser_Item>>();
        label45:

            // OR 46
            int _start_i46 = _index;

            // OR 47
            int _start_i47 = _index;

            // LITERAL "\x5c\x22"
            _ParseLiteralString(ref _index, "\x5c\x22");

            // OR shortcut
            if (_results.Peek() == null) { _results.Pop(); _index = _start_i47; } else goto label47;

            // LITERAL "\x5c\x5c"
            _ParseLiteralString(ref _index, "\x5c\x5c");

        label47: // OR
            int _dummy_i47 = _index; // no-op for label

            // OR shortcut
            if (_results.Peek() == null) { _results.Pop(); _index = _start_i46; } else goto label46;

            // AND 50
            int _start_i50 = _index;

            // NOT 51
            int _start_i51 = _index;

            // LITERAL '\x22'
            _ParseLiteralChar(ref _index, '\x22');

            // NOT 51
            var _r51 = _results.Pop();
            _results.Push( _r51 == null ? new _Parser_Item(_start_i51, _input_enumerable) : null);
            _index = _start_i51;

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label50; }

            // ANY
            _ParseAny(ref _index);

        label50: // AND
            var _r50_2 = _results.Pop();
            var _r50_1 = _results.Pop();

            if (_r50_1 != null && _r50_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i50, _index, _input_enumerable, _r50_1.Results.Concat(_r50_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i50;
            }

        label46: // OR
            int _dummy_i46 = _index; // no-op for label

            // STAR 45
            var _r45 = _results.Pop();
            if (_r45 != null)
            {
                _res45 = _res45.Concat(_r45.Results);
                goto label45;
            }
            else
            {
                _results.Push(new _Parser_Item(_start_i45, _index, _input_enumerable, _res45.Where(_NON_NULL), true));
            }

        label43: // AND
            var _r43_2 = _results.Pop();
            var _r43_1 = _results.Pop();

            if (_r43_1 != null && _r43_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i43, _index, _input_enumerable, _r43_1.Results.Concat(_r43_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i43;
            }

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label42; }

            // LITERAL '\x22'
            _ParseLiteralChar(ref _index, '\x22');

        label42: // AND
            var _r42_2 = _results.Pop();
            var _r42_1 = _results.Pop();

            if (_r42_1 != null && _r42_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i42, _index, _input_enumerable, _r42_1.Results.Concat(_r42_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i42;
            }

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void Identifier(int _index, _Parser_Args _args)
        {

            _Parser_Item id = null;

            // AND 1
            int _start_i1 = _index;

            // CALLORVAR Ident
            _Parser_Item _r3;

            _r3 = _MemoCall("Ident", _index, Ident, null);

            if (_r3 != null) _index = _r3.NextIndex;

            // BIND id
            id = _results.Peek();

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label1; }

            // CALLORVAR SP
            _Parser_Item _r4;

            _r4 = _MemoCall("SP", _index, SP, null);

            if (_r4 != null) _index = _r4.NextIndex;

        label1: // AND
            var _r1_2 = _results.Pop();
            var _r1_1 = _results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i1, _index, _input_enumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i1;
            }

            // ACT
            var _r0 = _results.Peek();
            if (_r0 != null)
            {
                _results.Pop();
                _results.Push( new _Parser_Item(_r0.StartIndex, _r0.NextIndex, _input_enumerable, _Thunk(_IM_Result => { return id; }, _r0), true) );
            }

        }


        public void Ident(int _index, _Parser_Args _args)
        {

            _Parser_Item id = null;

            // AND 2
            int _start_i2 = _index;

            // CALLORVAR IdentBegin
            _Parser_Item _r3;

            _r3 = _MemoCall("IdentBegin", _index, IdentBegin, null);

            if (_r3 != null) _index = _r3.NextIndex;

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label2; }

            // STAR 4
            int _start_i4 = _index;
            var _res4 = Enumerable.Empty<ASTNode<_Parser_Item>>();
        label4:

            // CALLORVAR IdentBody
            _Parser_Item _r5;

            _r5 = _MemoCall("IdentBody", _index, IdentBody, null);

            if (_r5 != null) _index = _r5.NextIndex;

            // STAR 4
            var _r4 = _results.Pop();
            if (_r4 != null)
            {
                _res4 = _res4.Concat(_r4.Results);
                goto label4;
            }
            else
            {
                _results.Push(new _Parser_Item(_start_i4, _index, _input_enumerable, _res4.Where(_NON_NULL), true));
            }

        label2: // AND
            var _r2_2 = _results.Pop();
            var _r2_1 = _results.Pop();

            if (_r2_1 != null && _r2_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i2, _index, _input_enumerable, _r2_1.Results.Concat(_r2_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i2;
            }

            // BIND id
            id = _results.Peek();

            // ACT
            var _r0 = _results.Peek();
            if (_r0 != null)
            {
                _results.Pop();
                _results.Push( new _Parser_Item(_r0.StartIndex, _r0.NextIndex, _input_enumerable, _Thunk(_IM_Result => { return new Idfr<_Parser_Item>(id); }, _r0), true) );
            }

        }


        public void IdentBegin(int _index, _Parser_Args _args)
        {

            // OR 0
            int _start_i0 = _index;

            // LITERAL '_'
            _ParseLiteralChar(ref _index, '_');

            // OR shortcut
            if (_results.Peek() == null) { _results.Pop(); _index = _start_i0; } else goto label0;

            // COND 2
            int _start_i2 = _index;

            // ANY
            _ParseAny(ref _index);

            // COND
            Func<_Parser_Item, bool> lambda2 = (_IM_Result) => { return System.Char.IsLetter(_IM_Result); };
            if (!lambda2(_results.Peek())) { _results.Pop(); _results.Push(null); _index = _start_i2; }
        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void IdentBody(int _index, _Parser_Args _args)
        {

            // OR 0
            int _start_i0 = _index;

            // LITERAL '_'
            _ParseLiteralChar(ref _index, '_');

            // OR shortcut
            if (_results.Peek() == null) { _results.Pop(); _index = _start_i0; } else goto label0;

            // COND 2
            int _start_i2 = _index;

            // ANY
            _ParseAny(ref _index);

            // COND
            Func<_Parser_Item, bool> lambda2 = (_IM_Result) => { return System.Char.IsLetterOrDigit(_IM_Result); };
            if (!lambda2(_results.Peek())) { _results.Pop(); _results.Push(null); _index = _start_i2; }
        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void GenericId(int _index, _Parser_Args _args)
        {

            _Parser_Item ids = null;
            _Parser_Item gp = null;

            // AND 1
            int _start_i1 = _index;

            // AND 2
            int _start_i2 = _index;

            // AND 4
            int _start_i4 = _index;

            // CALLORVAR Ident
            _Parser_Item _r5;

            _r5 = _MemoCall("Ident", _index, Ident, null);

            if (_r5 != null) _index = _r5.NextIndex;

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label4; }

            // STAR 6
            int _start_i6 = _index;
            var _res6 = Enumerable.Empty<ASTNode<_Parser_Item>>();
        label6:

            // AND 7
            int _start_i7 = _index;

            // CALLORVAR DOT
            _Parser_Item _r8;

            _r8 = _MemoCall("DOT", _index, DOT, null);

            if (_r8 != null) _index = _r8.NextIndex;

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label7; }

            // CALLORVAR Ident
            _Parser_Item _r9;

            _r9 = _MemoCall("Ident", _index, Ident, null);

            if (_r9 != null) _index = _r9.NextIndex;

        label7: // AND
            var _r7_2 = _results.Pop();
            var _r7_1 = _results.Pop();

            if (_r7_1 != null && _r7_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i7, _index, _input_enumerable, _r7_1.Results.Concat(_r7_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i7;
            }

            // STAR 6
            var _r6 = _results.Pop();
            if (_r6 != null)
            {
                _res6 = _res6.Concat(_r6.Results);
                goto label6;
            }
            else
            {
                _results.Push(new _Parser_Item(_start_i6, _index, _input_enumerable, _res6.Where(_NON_NULL), true));
            }

        label4: // AND
            var _r4_2 = _results.Pop();
            var _r4_1 = _results.Pop();

            if (_r4_1 != null && _r4_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i4, _index, _input_enumerable, _r4_1.Results.Concat(_r4_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i4;
            }

            // BIND ids
            ids = _results.Peek();

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label2; }

            // CALLORVAR SP
            _Parser_Item _r10;

            _r10 = _MemoCall("SP", _index, SP, null);

            if (_r10 != null) _index = _r10.NextIndex;

        label2: // AND
            var _r2_2 = _results.Pop();
            var _r2_1 = _results.Pop();

            if (_r2_1 != null && _r2_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i2, _index, _input_enumerable, _r2_1.Results.Concat(_r2_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i2;
            }

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label1; }

            // AND 13
            int _start_i13 = _index;

            // AND 14
            int _start_i14 = _index;

            // CALLORVAR LESS
            _Parser_Item _r15;

            _r15 = _MemoCall("LESS", _index, LESS, null);

            if (_r15 != null) _index = _r15.NextIndex;

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label14; }

            // AND 16
            int _start_i16 = _index;

            // CALLORVAR GenericId
            _Parser_Item _r17;

            _r17 = _MemoCall("GenericId", _index, GenericId, null);

            if (_r17 != null) _index = _r17.NextIndex;

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label16; }

            // STAR 18
            int _start_i18 = _index;
            var _res18 = Enumerable.Empty<ASTNode<_Parser_Item>>();
        label18:

            // AND 19
            int _start_i19 = _index;

            // CALLORVAR COMMA
            _Parser_Item _r20;

            _r20 = _MemoCall("COMMA", _index, COMMA, null);

            if (_r20 != null) _index = _r20.NextIndex;

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label19; }

            // CALLORVAR GenericId
            _Parser_Item _r21;

            _r21 = _MemoCall("GenericId", _index, GenericId, null);

            if (_r21 != null) _index = _r21.NextIndex;

        label19: // AND
            var _r19_2 = _results.Pop();
            var _r19_1 = _results.Pop();

            if (_r19_1 != null && _r19_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i19, _index, _input_enumerable, _r19_1.Results.Concat(_r19_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i19;
            }

            // STAR 18
            var _r18 = _results.Pop();
            if (_r18 != null)
            {
                _res18 = _res18.Concat(_r18.Results);
                goto label18;
            }
            else
            {
                _results.Push(new _Parser_Item(_start_i18, _index, _input_enumerable, _res18.Where(_NON_NULL), true));
            }

        label16: // AND
            var _r16_2 = _results.Pop();
            var _r16_1 = _results.Pop();

            if (_r16_1 != null && _r16_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i16, _index, _input_enumerable, _r16_1.Results.Concat(_r16_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i16;
            }

        label14: // AND
            var _r14_2 = _results.Pop();
            var _r14_1 = _results.Pop();

            if (_r14_1 != null && _r14_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i14, _index, _input_enumerable, _r14_1.Results.Concat(_r14_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i14;
            }

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label13; }

            // CALLORVAR GREATER
            _Parser_Item _r22;

            _r22 = _MemoCall("GREATER", _index, GREATER, null);

            if (_r22 != null) _index = _r22.NextIndex;

        label13: // AND
            var _r13_2 = _results.Pop();
            var _r13_1 = _results.Pop();

            if (_r13_1 != null && _r13_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i13, _index, _input_enumerable, _r13_1.Results.Concat(_r13_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i13;
            }

            // QUES
            if (_results.Peek() == null) { _results.Pop(); _results.Push(new _Parser_Item(_index, _input_enumerable)); }

            // BIND gp
            gp = _results.Peek();

        label1: // AND
            var _r1_2 = _results.Pop();
            var _r1_1 = _results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i1, _index, _input_enumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i1;
            }

            // ACT
            var _r0 = _results.Peek();
            if (_r0 != null)
            {
                _results.Pop();
                _results.Push( new _Parser_Item(_r0.StartIndex, _r0.NextIndex, _input_enumerable, _Thunk(_IM_Result => { return new Idfr<_Parser_Item>(ids, gp); }, _r0), true) );
            }

        }


        public void USING(int _index, _Parser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // LITERAL "using"
            _ParseLiteralString(ref _index, "using");

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label0; }

            // CALLORVAR SP
            _Parser_Item _r2;

            _r2 = _MemoCall("SP", _index, SP, null);

            if (_r2 != null) _index = _r2.NextIndex;

        label0: // AND
            var _r0_2 = _results.Pop();
            var _r0_1 = _results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i0, _index, _input_enumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i0;
            }

        }


        public void IRONMETA(int _index, _Parser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // LITERAL "ironmeta"
            _ParseLiteralString(ref _index, "ironmeta");

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label0; }

            // CALLORVAR SP
            _Parser_Item _r2;

            _r2 = _MemoCall("SP", _index, SP, null);

            if (_r2 != null) _index = _r2.NextIndex;

        label0: // AND
            var _r0_2 = _results.Pop();
            var _r0_1 = _results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i0, _index, _input_enumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i0;
            }

        }


        public void EQUALS(int _index, _Parser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // OR 1
            int _start_i1 = _index;

            // LITERAL '='
            _ParseLiteralChar(ref _index, '=');

            // OR shortcut
            if (_results.Peek() == null) { _results.Pop(); _index = _start_i1; } else goto label1;

            // LITERAL "::="
            _ParseLiteralString(ref _index, "::=");

        label1: // OR
            int _dummy_i1 = _index; // no-op for label

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label0; }

            // CALLORVAR SP
            _Parser_Item _r4;

            _r4 = _MemoCall("SP", _index, SP, null);

            if (_r4 != null) _index = _r4.NextIndex;

        label0: // AND
            var _r0_2 = _results.Pop();
            var _r0_1 = _results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i0, _index, _input_enumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i0;
            }

        }


        public void OVERRIDE(int _index, _Parser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // LITERAL "override"
            _ParseLiteralString(ref _index, "override");

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label0; }

            // CALLORVAR SP
            _Parser_Item _r2;

            _r2 = _MemoCall("SP", _index, SP, null);

            if (_r2 != null) _index = _r2.NextIndex;

        label0: // AND
            var _r0_2 = _results.Pop();
            var _r0_1 = _results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i0, _index, _input_enumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i0;
            }

        }


        public void LR(int _index, _Parser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // LITERAL "lr"
            _ParseLiteralString(ref _index, "lr");

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label0; }

            // CALLORVAR SP
            _Parser_Item _r2;

            _r2 = _MemoCall("SP", _index, SP, null);

            if (_r2 != null) _index = _r2.NextIndex;

        label0: // AND
            var _r0_2 = _results.Pop();
            var _r0_1 = _results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i0, _index, _input_enumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i0;
            }

        }


        public void SEMI(int _index, _Parser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // OR 1
            int _start_i1 = _index;

            // LITERAL ';'
            _ParseLiteralChar(ref _index, ';');

            // OR shortcut
            if (_results.Peek() == null) { _results.Pop(); _index = _start_i1; } else goto label1;

            // LITERAL ','
            _ParseLiteralChar(ref _index, ',');

        label1: // OR
            int _dummy_i1 = _index; // no-op for label

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label0; }

            // CALLORVAR SP
            _Parser_Item _r4;

            _r4 = _MemoCall("SP", _index, SP, null);

            if (_r4 != null) _index = _r4.NextIndex;

        label0: // AND
            var _r0_2 = _results.Pop();
            var _r0_1 = _results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i0, _index, _input_enumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i0;
            }

        }


        public void BANG(int _index, _Parser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // LITERAL '!'
            _ParseLiteralChar(ref _index, '!');

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label0; }

            // CALLORVAR SP
            _Parser_Item _r2;

            _r2 = _MemoCall("SP", _index, SP, null);

            if (_r2 != null) _index = _r2.NextIndex;

        label0: // AND
            var _r0_2 = _results.Pop();
            var _r0_1 = _results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i0, _index, _input_enumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i0;
            }

        }


        public void OR(int _index, _Parser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // LITERAL '|'
            _ParseLiteralChar(ref _index, '|');

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label0; }

            // CALLORVAR SP
            _Parser_Item _r2;

            _r2 = _MemoCall("SP", _index, SP, null);

            if (_r2 != null) _index = _r2.NextIndex;

        label0: // AND
            var _r0_2 = _results.Pop();
            var _r0_1 = _results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i0, _index, _input_enumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i0;
            }

        }


        public void ACTION(int _index, _Parser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // LITERAL "->"
            _ParseLiteralString(ref _index, "->");

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label0; }

            // CALLORVAR SP
            _Parser_Item _r2;

            _r2 = _MemoCall("SP", _index, SP, null);

            if (_r2 != null) _index = _r2.NextIndex;

        label0: // AND
            var _r0_2 = _results.Pop();
            var _r0_1 = _results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i0, _index, _input_enumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i0;
            }

        }


        public void COLON(int _index, _Parser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // LITERAL ':'
            _ParseLiteralChar(ref _index, ':');

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label0; }

            // CALLORVAR SP
            _Parser_Item _r2;

            _r2 = _MemoCall("SP", _index, SP, null);

            if (_r2 != null) _index = _r2.NextIndex;

        label0: // AND
            var _r0_2 = _results.Pop();
            var _r0_1 = _results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i0, _index, _input_enumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i0;
            }

        }


        public void COMMA(int _index, _Parser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // LITERAL ','
            _ParseLiteralChar(ref _index, ',');

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label0; }

            // CALLORVAR SP
            _Parser_Item _r2;

            _r2 = _MemoCall("SP", _index, SP, null);

            if (_r2 != null) _index = _r2.NextIndex;

        label0: // AND
            var _r0_2 = _results.Pop();
            var _r0_1 = _results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i0, _index, _input_enumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i0;
            }

        }


        public void DOT(int _index, _Parser_Args _args)
        {

            // LITERAL '.'
            _ParseLiteralChar(ref _index, '.');

        }


        public void PERIOD(int _index, _Parser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // CALLORVAR DOT
            _Parser_Item _r1;

            _r1 = _MemoCall("DOT", _index, DOT, null);

            if (_r1 != null) _index = _r1.NextIndex;

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label0; }

            // CALLORVAR SP
            _Parser_Item _r2;

            _r2 = _MemoCall("SP", _index, SP, null);

            if (_r2 != null) _index = _r2.NextIndex;

        label0: // AND
            var _r0_2 = _results.Pop();
            var _r0_1 = _results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i0, _index, _input_enumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i0;
            }

        }


        public void BRA(int _index, _Parser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // LITERAL '{'
            _ParseLiteralChar(ref _index, '{');

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label0; }

            // CALLORVAR SP
            _Parser_Item _r2;

            _r2 = _MemoCall("SP", _index, SP, null);

            if (_r2 != null) _index = _r2.NextIndex;

        label0: // AND
            var _r0_2 = _results.Pop();
            var _r0_1 = _results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i0, _index, _input_enumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i0;
            }

        }


        public void KET(int _index, _Parser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // LITERAL '}'
            _ParseLiteralChar(ref _index, '}');

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label0; }

            // CALLORVAR SP
            _Parser_Item _r2;

            _r2 = _MemoCall("SP", _index, SP, null);

            if (_r2 != null) _index = _r2.NextIndex;

        label0: // AND
            var _r0_2 = _results.Pop();
            var _r0_1 = _results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i0, _index, _input_enumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i0;
            }

        }


        public void OPEN(int _index, _Parser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // LITERAL '('
            _ParseLiteralChar(ref _index, '(');

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label0; }

            // CALLORVAR SP
            _Parser_Item _r2;

            _r2 = _MemoCall("SP", _index, SP, null);

            if (_r2 != null) _index = _r2.NextIndex;

        label0: // AND
            var _r0_2 = _results.Pop();
            var _r0_1 = _results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i0, _index, _input_enumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i0;
            }

        }


        public void CLOSE(int _index, _Parser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // LITERAL ')'
            _ParseLiteralChar(ref _index, ')');

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label0; }

            // CALLORVAR SP
            _Parser_Item _r2;

            _r2 = _MemoCall("SP", _index, SP, null);

            if (_r2 != null) _index = _r2.NextIndex;

        label0: // AND
            var _r0_2 = _results.Pop();
            var _r0_1 = _results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i0, _index, _input_enumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i0;
            }

        }


        public void LESS(int _index, _Parser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // LITERAL '<'
            _ParseLiteralChar(ref _index, '<');

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label0; }

            // CALLORVAR SP
            _Parser_Item _r2;

            _r2 = _MemoCall("SP", _index, SP, null);

            if (_r2 != null) _index = _r2.NextIndex;

        label0: // AND
            var _r0_2 = _results.Pop();
            var _r0_1 = _results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i0, _index, _input_enumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i0;
            }

        }


        public void GREATER(int _index, _Parser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // LITERAL '>'
            _ParseLiteralChar(ref _index, '>');

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label0; }

            // CALLORVAR SP
            _Parser_Item _r2;

            _r2 = _MemoCall("SP", _index, SP, null);

            if (_r2 != null) _index = _r2.NextIndex;

        label0: // AND
            var _r0_2 = _results.Pop();
            var _r0_1 = _results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i0, _index, _input_enumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i0;
            }

        }


        public void QUES(int _index, _Parser_Args _args)
        {

            // LITERAL '?'
            _ParseLiteralChar(ref _index, '?');

        }


        public void AND_PRE(int _index, _Parser_Args _args)
        {

            // LITERAL '&'
            _ParseLiteralChar(ref _index, '&');

        }


        public void NOT_PRE(int _index, _Parser_Args _args)
        {

            // LITERAL '~'
            _ParseLiteralChar(ref _index, '~');

        }


        public void STAR(int _index, _Parser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // LITERAL '*'
            _ParseLiteralChar(ref _index, '*');

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label0; }

            // CALLORVAR SP
            _Parser_Item _r2;

            _r2 = _MemoCall("SP", _index, SP, null);

            if (_r2 != null) _index = _r2.NextIndex;

        label0: // AND
            var _r0_2 = _results.Pop();
            var _r0_1 = _results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i0, _index, _input_enumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i0;
            }

        }


        public void PLUS(int _index, _Parser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // LITERAL '+'
            _ParseLiteralChar(ref _index, '+');

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label0; }

            // CALLORVAR SP
            _Parser_Item _r2;

            _r2 = _MemoCall("SP", _index, SP, null);

            if (_r2 != null) _index = _r2.NextIndex;

        label0: // AND
            var _r0_2 = _results.Pop();
            var _r0_1 = _results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i0, _index, _input_enumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i0;
            }

        }


        public void EOL(int _index, _Parser_Args _args)
        {

            // OR 0
            int _start_i0 = _index;

            // AND 1
            int _start_i1 = _index;

            // LITERAL '\r'
            _ParseLiteralChar(ref _index, '\r');

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label1; }

            // LITERAL '\n'
            _ParseLiteralChar(ref _index, '\n');

            // QUES
            if (_results.Peek() == null) { _results.Pop(); _results.Push(new _Parser_Item(_index, _input_enumerable)); }

        label1: // AND
            var _r1_2 = _results.Pop();
            var _r1_1 = _results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i1, _index, _input_enumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i1;
            }

            // OR shortcut
            if (_results.Peek() == null) { _results.Pop(); _index = _start_i0; } else goto label0;

            // LITERAL '\n'
            _ParseLiteralChar(ref _index, '\n');

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void EOF(int _index, _Parser_Args _args)
        {

            // NOT 0
            int _start_i0 = _index;

            // ANY
            _ParseAny(ref _index);

            // NOT 0
            var _r0 = _results.Pop();
            _results.Push( _r0 == null ? new _Parser_Item(_start_i0, _input_enumerable) : null);
            _index = _start_i0;

        }


        public void SP(int _index, _Parser_Args _args)
        {

            // STAR 0
            int _start_i0 = _index;
            var _res0 = Enumerable.Empty<ASTNode<_Parser_Item>>();
        label0:

            // OR 1
            int _start_i1 = _index;

            // CALLORVAR WS
            _Parser_Item _r2;

            _r2 = _MemoCall("WS", _index, WS, null);

            if (_r2 != null) _index = _r2.NextIndex;

            // OR shortcut
            if (_results.Peek() == null) { _results.Pop(); _index = _start_i1; } else goto label1;

            // CALLORVAR Comment
            _Parser_Item _r3;

            _r3 = _MemoCall("Comment", _index, Comment, null);

            if (_r3 != null) _index = _r3.NextIndex;

        label1: // OR
            int _dummy_i1 = _index; // no-op for label

            // STAR 0
            var _r0 = _results.Pop();
            if (_r0 != null)
            {
                _res0 = _res0.Concat(_r0.Results);
                goto label0;
            }
            else
            {
                _results.Push(new _Parser_Item(_start_i0, _index, _input_enumerable, _res0.Where(_NON_NULL), true));
            }

        }


        public void WS(int _index, _Parser_Args _args)
        {

            // OR 0
            int _start_i0 = _index;

            // OR 1
            int _start_i1 = _index;

            // OR 2
            int _start_i2 = _index;

            // LITERAL ' '
            _ParseLiteralChar(ref _index, ' ');

            // OR shortcut
            if (_results.Peek() == null) { _results.Pop(); _index = _start_i2; } else goto label2;

            // LITERAL '\n'
            _ParseLiteralChar(ref _index, '\n');

        label2: // OR
            int _dummy_i2 = _index; // no-op for label

            // OR shortcut
            if (_results.Peek() == null) { _results.Pop(); _index = _start_i1; } else goto label1;

            // LITERAL '\r'
            _ParseLiteralChar(ref _index, '\r');

        label1: // OR
            int _dummy_i1 = _index; // no-op for label

            // OR shortcut
            if (_results.Peek() == null) { _results.Pop(); _index = _start_i0; } else goto label0;

            // LITERAL '\t'
            _ParseLiteralChar(ref _index, '\t');

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void Comment(int _index, _Parser_Args _args)
        {

            // OR 0
            int _start_i0 = _index;

            // AND 1
            int _start_i1 = _index;

            // AND 2
            int _start_i2 = _index;

            // LITERAL "//"
            _ParseLiteralString(ref _index, "//");

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label2; }

            // STAR 4
            int _start_i4 = _index;
            var _res4 = Enumerable.Empty<ASTNode<_Parser_Item>>();
        label4:

            // AND 5
            int _start_i5 = _index;

            // NOT 6
            int _start_i6 = _index;

            // OR 7
            int _start_i7 = _index;

            // AND 8
            int _start_i8 = _index;

            // LITERAL '\r'
            _ParseLiteralChar(ref _index, '\r');

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label8; }

            // LITERAL '\n'
            _ParseLiteralChar(ref _index, '\n');

            // QUES
            if (_results.Peek() == null) { _results.Pop(); _results.Push(new _Parser_Item(_index, _input_enumerable)); }

        label8: // AND
            var _r8_2 = _results.Pop();
            var _r8_1 = _results.Pop();

            if (_r8_1 != null && _r8_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i8, _index, _input_enumerable, _r8_1.Results.Concat(_r8_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i8;
            }

            // OR shortcut
            if (_results.Peek() == null) { _results.Pop(); _index = _start_i7; } else goto label7;

            // LITERAL '\n'
            _ParseLiteralChar(ref _index, '\n');

        label7: // OR
            int _dummy_i7 = _index; // no-op for label

            // NOT 6
            var _r6 = _results.Pop();
            _results.Push( _r6 == null ? new _Parser_Item(_start_i6, _input_enumerable) : null);
            _index = _start_i6;

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label5; }

            // ANY
            _ParseAny(ref _index);

        label5: // AND
            var _r5_2 = _results.Pop();
            var _r5_1 = _results.Pop();

            if (_r5_1 != null && _r5_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i5, _index, _input_enumerable, _r5_1.Results.Concat(_r5_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i5;
            }

            // STAR 4
            var _r4 = _results.Pop();
            if (_r4 != null)
            {
                _res4 = _res4.Concat(_r4.Results);
                goto label4;
            }
            else
            {
                _results.Push(new _Parser_Item(_start_i4, _index, _input_enumerable, _res4.Where(_NON_NULL), true));
            }

        label2: // AND
            var _r2_2 = _results.Pop();
            var _r2_1 = _results.Pop();

            if (_r2_1 != null && _r2_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i2, _index, _input_enumerable, _r2_1.Results.Concat(_r2_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i2;
            }

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label1; }

            // OR 14
            int _start_i14 = _index;

            // AND 15
            int _start_i15 = _index;

            // LITERAL '\r'
            _ParseLiteralChar(ref _index, '\r');

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label15; }

            // LITERAL '\n'
            _ParseLiteralChar(ref _index, '\n');

            // QUES
            if (_results.Peek() == null) { _results.Pop(); _results.Push(new _Parser_Item(_index, _input_enumerable)); }

        label15: // AND
            var _r15_2 = _results.Pop();
            var _r15_1 = _results.Pop();

            if (_r15_1 != null && _r15_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i15, _index, _input_enumerable, _r15_1.Results.Concat(_r15_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i15;
            }

            // OR shortcut
            if (_results.Peek() == null) { _results.Pop(); _index = _start_i14; } else goto label14;

            // LITERAL '\n'
            _ParseLiteralChar(ref _index, '\n');

        label14: // OR
            int _dummy_i14 = _index; // no-op for label

        label1: // AND
            var _r1_2 = _results.Pop();
            var _r1_1 = _results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i1, _index, _input_enumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i1;
            }

            // OR shortcut
            if (_results.Peek() == null) { _results.Pop(); _index = _start_i0; } else goto label0;

            // AND 20
            int _start_i20 = _index;

            // AND 21
            int _start_i21 = _index;

            // LITERAL "/*"
            _ParseLiteralString(ref _index, "/*");

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label21; }

            // STAR 23
            int _start_i23 = _index;
            var _res23 = Enumerable.Empty<ASTNode<_Parser_Item>>();
        label23:

            // AND 24
            int _start_i24 = _index;

            // NOT 25
            int _start_i25 = _index;

            // LITERAL "*/"
            _ParseLiteralString(ref _index, "*/");

            // NOT 25
            var _r25 = _results.Pop();
            _results.Push( _r25 == null ? new _Parser_Item(_start_i25, _input_enumerable) : null);
            _index = _start_i25;

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label24; }

            // ANY
            _ParseAny(ref _index);

        label24: // AND
            var _r24_2 = _results.Pop();
            var _r24_1 = _results.Pop();

            if (_r24_1 != null && _r24_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i24, _index, _input_enumerable, _r24_1.Results.Concat(_r24_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i24;
            }

            // STAR 23
            var _r23 = _results.Pop();
            if (_r23 != null)
            {
                _res23 = _res23.Concat(_r23.Results);
                goto label23;
            }
            else
            {
                _results.Push(new _Parser_Item(_start_i23, _index, _input_enumerable, _res23.Where(_NON_NULL), true));
            }

        label21: // AND
            var _r21_2 = _results.Pop();
            var _r21_1 = _results.Pop();

            if (_r21_1 != null && _r21_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i21, _index, _input_enumerable, _r21_1.Results.Concat(_r21_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i21;
            }

            // AND shortcut
            if (_results.Peek() == null) { _results.Push(null); goto label20; }

            // LITERAL "*/"
            _ParseLiteralString(ref _index, "*/");

        label20: // AND
            var _r20_2 = _results.Pop();
            var _r20_1 = _results.Pop();

            if (_r20_1 != null && _r20_2 != null)
            {
                _results.Push( new _Parser_Item(_start_i20, _index, _input_enumerable, _r20_1.Results.Concat(_r20_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _results.Push(null);
                _index = _start_i20;
            }

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }

    } // class Parser

} // namespace IronMeta.Generator


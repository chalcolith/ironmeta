//
// IronMeta Parser Parser; Generated 2013-08-21 05:00:52Z UTC
//

using System;
using System.Collections.Generic;
using System.Linq;
using IronMeta.Matcher;
using IronMeta;

#pragma warning disable 0219
#pragma warning disable 1591

namespace IronMeta.Generator
{

    using _Parser_Inputs = IEnumerable<char>;
    using _Parser_Results = IEnumerable<AST.Node>;
    using _Parser_Item = IronMeta.Matcher.MatchItem<char, AST.Node>;
    using _Parser_Args = IEnumerable<IronMeta.Matcher.MatchItem<char, AST.Node>>;
    using _Parser_Memo = Memo<char, AST.Node>;
    using _Parser_Rule = System.Action<Memo<char, AST.Node>, int, IEnumerable<IronMeta.Matcher.MatchItem<char, AST.Node>>>;
    using _Parser_Base = IronMeta.Matcher.Matcher<char, AST.Node>;

    public partial class Parser : IronMeta.Matcher.CharMatcher<AST.Node>
    {
        public Parser()
            : base()
        { }

        public Parser(bool handle_left_recursion)
            : base(handle_left_recursion)
        { }

        public void IronMetaFile(_Parser_Memo _memo, int _index, _Parser_Args _args)
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
            _ParseLiteralChar(_memo, ref _index, '\ufeff');

            // QUES
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _memo.Results.Push(new _Parser_Item(_index, _memo.InputEnumerable)); }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label4; }

            // CALLORVAR SP
            _Parser_Item _r7;

            _r7 = _MemoCall(_memo, "SP", _index, SP, null);

            if (_r7 != null) _index = _r7.NextIndex;

        label4: // AND
            var _r4_2 = _memo.Results.Pop();
            var _r4_1 = _memo.Results.Pop();

            if (_r4_1 != null && _r4_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i4, _index, _memo.InputEnumerable, _r4_1.Results.Concat(_r4_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i4;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label3; }

            // CALLORVAR Preamble
            _Parser_Item _r10;

            _r10 = _MemoCall(_memo, "Preamble", _index, Preamble, null);

            if (_r10 != null) _index = _r10.NextIndex;

            // QUES
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _memo.Results.Push(new _Parser_Item(_index, _memo.InputEnumerable)); }

            // BIND p
            p = _memo.Results.Peek();

        label3: // AND
            var _r3_2 = _memo.Results.Pop();
            var _r3_1 = _memo.Results.Pop();

            if (_r3_1 != null && _r3_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i3, _index, _memo.InputEnumerable, _r3_1.Results.Concat(_r3_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i3;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label2; }

            // CALLORVAR Grammar
            _Parser_Item _r12;

            _r12 = _MemoCall(_memo, "Grammar", _index, Grammar, null);

            if (_r12 != null) _index = _r12.NextIndex;

            // BIND g
            g = _memo.Results.Peek();

        label2: // AND
            var _r2_2 = _memo.Results.Pop();
            var _r2_1 = _memo.Results.Pop();

            if (_r2_1 != null && _r2_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i2, _index, _memo.InputEnumerable, _r2_1.Results.Concat(_r2_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i2;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label1; }

            // CALLORVAR EOF
            _Parser_Item _r13;

            _r13 = _MemoCall(_memo, "EOF", _index, EOF, null);

            if (_r13 != null) _index = _r13.NextIndex;

        label1: // AND
            var _r1_2 = _memo.Results.Pop();
            var _r1_1 = _memo.Results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i1, _index, _memo.InputEnumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i1;
            }

            // ACT
            var _r0 = _memo.Results.Peek();
            if (_r0 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _Parser_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return new AST.GrammarFile(p, g); }, _r0), true) );
            }

        }


        public void Preamble(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            _Parser_Item u = null;

            // PLUS 2
            int _start_i2 = _index;
            var _res2 = Enumerable.Empty<AST.Node>();
        label2:

            // CALLORVAR Using
            _Parser_Item _r3;

            _r3 = _MemoCall(_memo, "Using", _index, Using, null);

            if (_r3 != null) _index = _r3.NextIndex;

            // PLUS 2
            var _r2 = _memo.Results.Pop();
            if (_r2 != null)
            {
                _res2 = _res2.Concat(_r2.Results);
                goto label2;
            }
            else
            {
                if (_index > _start_i2)
                    _memo.Results.Push(new _Parser_Item(_start_i2, _index, _memo.InputEnumerable, _res2.Where(_NON_NULL), true));
                else
                    _memo.Results.Push(null);
            }

            // BIND u
            u = _memo.Results.Peek();

            // ACT
            var _r0 = _memo.Results.Peek();
            if (_r0 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _Parser_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return new AST.Preamble(u.Results); }, _r0), true) );
            }

        }


        public void Using(_Parser_Memo _memo, int _index, _Parser_Args _args)
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

            _r4 = _MemoCall(_memo, "USING", _index, USING, null);

            if (_r4 != null) _index = _r4.NextIndex;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label3; }

            // AND 6
            int _start_i6 = _index;

            // CALLORVAR Ident
            _Parser_Item _r7;

            _r7 = _MemoCall(_memo, "Ident", _index, Ident, null);

            if (_r7 != null) _index = _r7.NextIndex;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label6; }

            // STAR 8
            int _start_i8 = _index;
            var _res8 = Enumerable.Empty<AST.Node>();
        label8:

            // AND 9
            int _start_i9 = _index;

            // CALLORVAR DOT
            _Parser_Item _r10;

            _r10 = _MemoCall(_memo, "DOT", _index, DOT, null);

            if (_r10 != null) _index = _r10.NextIndex;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label9; }

            // CALLORVAR Ident
            _Parser_Item _r11;

            _r11 = _MemoCall(_memo, "Ident", _index, Ident, null);

            if (_r11 != null) _index = _r11.NextIndex;

        label9: // AND
            var _r9_2 = _memo.Results.Pop();
            var _r9_1 = _memo.Results.Pop();

            if (_r9_1 != null && _r9_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i9, _index, _memo.InputEnumerable, _r9_1.Results.Concat(_r9_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i9;
            }

            // STAR 8
            var _r8 = _memo.Results.Pop();
            if (_r8 != null)
            {
                _res8 = _res8.Concat(_r8.Results);
                goto label8;
            }
            else
            {
                _memo.Results.Push(new _Parser_Item(_start_i8, _index, _memo.InputEnumerable, _res8.Where(_NON_NULL), true));
            }

        label6: // AND
            var _r6_2 = _memo.Results.Pop();
            var _r6_1 = _memo.Results.Pop();

            if (_r6_1 != null && _r6_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i6, _index, _memo.InputEnumerable, _r6_1.Results.Concat(_r6_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i6;
            }

            // BIND name
            name = _memo.Results.Peek();

        label3: // AND
            var _r3_2 = _memo.Results.Pop();
            var _r3_1 = _memo.Results.Pop();

            if (_r3_1 != null && _r3_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i3, _index, _memo.InputEnumerable, _r3_1.Results.Concat(_r3_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i3;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label2; }

            // CALLORVAR SP
            _Parser_Item _r12;

            _r12 = _MemoCall(_memo, "SP", _index, SP, null);

            if (_r12 != null) _index = _r12.NextIndex;

        label2: // AND
            var _r2_2 = _memo.Results.Pop();
            var _r2_1 = _memo.Results.Pop();

            if (_r2_1 != null && _r2_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i2, _index, _memo.InputEnumerable, _r2_1.Results.Concat(_r2_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i2;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label1; }

            // CALLORVAR SEMI
            _Parser_Item _r13;

            _r13 = _MemoCall(_memo, "SEMI", _index, SEMI, null);

            if (_r13 != null) _index = _r13.NextIndex;

        label1: // AND
            var _r1_2 = _memo.Results.Pop();
            var _r1_1 = _memo.Results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i1, _index, _memo.InputEnumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i1;
            }

            // ACT
            var _r0 = _memo.Results.Peek();
            if (_r0 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _Parser_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return new AST.Using(name); }, _r0), true) );
            }

        }


        public void Grammar(_Parser_Memo _memo, int _index, _Parser_Args _args)
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

            _r12 = _MemoCall(_memo, "IRONMETA", _index, IRONMETA, null);

            if (_r12 != null) _index = _r12.NextIndex;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label11; }

            // CALLORVAR Identifier
            _Parser_Item _r14;

            _r14 = _MemoCall(_memo, "Identifier", _index, Identifier, null);

            if (_r14 != null) _index = _r14.NextIndex;

            // BIND name
            name = _memo.Results.Peek();

        label11: // AND
            var _r11_2 = _memo.Results.Pop();
            var _r11_1 = _memo.Results.Pop();

            if (_r11_1 != null && _r11_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i11, _index, _memo.InputEnumerable, _r11_1.Results.Concat(_r11_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i11;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label10; }

            // CALLORVAR LESS
            _Parser_Item _r15;

            _r15 = _MemoCall(_memo, "LESS", _index, LESS, null);

            if (_r15 != null) _index = _r15.NextIndex;

        label10: // AND
            var _r10_2 = _memo.Results.Pop();
            var _r10_1 = _memo.Results.Pop();

            if (_r10_1 != null && _r10_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i10, _index, _memo.InputEnumerable, _r10_1.Results.Concat(_r10_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i10;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label9; }

            // CALLORVAR GenericId
            _Parser_Item _r17;

            _r17 = _MemoCall(_memo, "GenericId", _index, GenericId, null);

            if (_r17 != null) _index = _r17.NextIndex;

            // BIND tinput
            tinput = _memo.Results.Peek();

        label9: // AND
            var _r9_2 = _memo.Results.Pop();
            var _r9_1 = _memo.Results.Pop();

            if (_r9_1 != null && _r9_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i9, _index, _memo.InputEnumerable, _r9_1.Results.Concat(_r9_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i9;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label8; }

            // CALLORVAR COMMA
            _Parser_Item _r18;

            _r18 = _MemoCall(_memo, "COMMA", _index, COMMA, null);

            if (_r18 != null) _index = _r18.NextIndex;

        label8: // AND
            var _r8_2 = _memo.Results.Pop();
            var _r8_1 = _memo.Results.Pop();

            if (_r8_1 != null && _r8_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i8, _index, _memo.InputEnumerable, _r8_1.Results.Concat(_r8_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i8;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label7; }

            // CALLORVAR GenericId
            _Parser_Item _r20;

            _r20 = _MemoCall(_memo, "GenericId", _index, GenericId, null);

            if (_r20 != null) _index = _r20.NextIndex;

            // BIND tresult
            tresult = _memo.Results.Peek();

        label7: // AND
            var _r7_2 = _memo.Results.Pop();
            var _r7_1 = _memo.Results.Pop();

            if (_r7_1 != null && _r7_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i7, _index, _memo.InputEnumerable, _r7_1.Results.Concat(_r7_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i7;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label6; }

            // CALLORVAR GREATER
            _Parser_Item _r21;

            _r21 = _MemoCall(_memo, "GREATER", _index, GREATER, null);

            if (_r21 != null) _index = _r21.NextIndex;

        label6: // AND
            var _r6_2 = _memo.Results.Pop();
            var _r6_1 = _memo.Results.Pop();

            if (_r6_1 != null && _r6_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i6, _index, _memo.InputEnumerable, _r6_1.Results.Concat(_r6_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i6;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label5; }

            // CALLORVAR COLON
            _Parser_Item _r22;

            _r22 = _MemoCall(_memo, "COLON", _index, COLON, null);

            if (_r22 != null) _index = _r22.NextIndex;

        label5: // AND
            var _r5_2 = _memo.Results.Pop();
            var _r5_1 = _memo.Results.Pop();

            if (_r5_1 != null && _r5_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i5, _index, _memo.InputEnumerable, _r5_1.Results.Concat(_r5_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i5;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label4; }

            // CALLORVAR GenericId
            _Parser_Item _r24;

            _r24 = _MemoCall(_memo, "GenericId", _index, GenericId, null);

            if (_r24 != null) _index = _r24.NextIndex;

            // BIND baseclass
            baseclass = _memo.Results.Peek();

        label4: // AND
            var _r4_2 = _memo.Results.Pop();
            var _r4_1 = _memo.Results.Pop();

            if (_r4_1 != null && _r4_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i4, _index, _memo.InputEnumerable, _r4_1.Results.Concat(_r4_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i4;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label3; }

            // CALLORVAR BRA
            _Parser_Item _r25;

            _r25 = _MemoCall(_memo, "BRA", _index, BRA, null);

            if (_r25 != null) _index = _r25.NextIndex;

        label3: // AND
            var _r3_2 = _memo.Results.Pop();
            var _r3_1 = _memo.Results.Pop();

            if (_r3_1 != null && _r3_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i3, _index, _memo.InputEnumerable, _r3_1.Results.Concat(_r3_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i3;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label2; }

            // STAR 27
            int _start_i27 = _index;
            var _res27 = Enumerable.Empty<AST.Node>();
        label27:

            // CALLORVAR Rule
            _Parser_Item _r28;

            _r28 = _MemoCall(_memo, "Rule", _index, Rule, null);

            if (_r28 != null) _index = _r28.NextIndex;

            // STAR 27
            var _r27 = _memo.Results.Pop();
            if (_r27 != null)
            {
                _res27 = _res27.Concat(_r27.Results);
                goto label27;
            }
            else
            {
                _memo.Results.Push(new _Parser_Item(_start_i27, _index, _memo.InputEnumerable, _res27.Where(_NON_NULL), true));
            }

            // BIND rules
            rules = _memo.Results.Peek();

        label2: // AND
            var _r2_2 = _memo.Results.Pop();
            var _r2_1 = _memo.Results.Pop();

            if (_r2_1 != null && _r2_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i2, _index, _memo.InputEnumerable, _r2_1.Results.Concat(_r2_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i2;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label1; }

            // CALLORVAR KET
            _Parser_Item _r29;

            _r29 = _MemoCall(_memo, "KET", _index, KET, null);

            if (_r29 != null) _index = _r29.NextIndex;

        label1: // AND
            var _r1_2 = _memo.Results.Pop();
            var _r1_1 = _memo.Results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i1, _index, _memo.InputEnumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i1;
            }

            // ACT
            var _r0 = _memo.Results.Peek();
            if (_r0 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _Parser_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return new AST.Grammar(name, tinput, tresult, baseclass, rules.Results); }, _r0), true) );
            }

        }


        public void Rule(_Parser_Memo _memo, int _index, _Parser_Args _args)
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

            _r8 = _MemoCall(_memo, "OVERRIDE", _index, OVERRIDE, null);

            if (_r8 != null) _index = _r8.NextIndex;

            // QUES
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _memo.Results.Push(new _Parser_Item(_index, _memo.InputEnumerable)); }

            // BIND ovr
            ovr = _memo.Results.Peek();

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label5; }

            // CALLORVAR Identifier
            _Parser_Item _r10;

            _r10 = _MemoCall(_memo, "Identifier", _index, Identifier, null);

            if (_r10 != null) _index = _r10.NextIndex;

            // BIND name
            name = _memo.Results.Peek();

        label5: // AND
            var _r5_2 = _memo.Results.Pop();
            var _r5_1 = _memo.Results.Pop();

            if (_r5_1 != null && _r5_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i5, _index, _memo.InputEnumerable, _r5_1.Results.Concat(_r5_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i5;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label4; }

            // CALLORVAR Disjunction
            _Parser_Item _r13;

            _r13 = _MemoCall(_memo, "Disjunction", _index, Disjunction, null);

            if (_r13 != null) _index = _r13.NextIndex;

            // QUES
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _memo.Results.Push(new _Parser_Item(_index, _memo.InputEnumerable)); }

            // BIND parms
            parms = _memo.Results.Peek();

        label4: // AND
            var _r4_2 = _memo.Results.Pop();
            var _r4_1 = _memo.Results.Pop();

            if (_r4_1 != null && _r4_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i4, _index, _memo.InputEnumerable, _r4_1.Results.Concat(_r4_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i4;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label3; }

            // CALLORVAR EQUALS
            _Parser_Item _r14;

            _r14 = _MemoCall(_memo, "EQUALS", _index, EQUALS, null);

            if (_r14 != null) _index = _r14.NextIndex;

        label3: // AND
            var _r3_2 = _memo.Results.Pop();
            var _r3_1 = _memo.Results.Pop();

            if (_r3_1 != null && _r3_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i3, _index, _memo.InputEnumerable, _r3_1.Results.Concat(_r3_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i3;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label2; }

            // CALLORVAR Disjunction
            _Parser_Item _r16;

            _r16 = _MemoCall(_memo, "Disjunction", _index, Disjunction, null);

            if (_r16 != null) _index = _r16.NextIndex;

            // BIND body
            body = _memo.Results.Peek();

        label2: // AND
            var _r2_2 = _memo.Results.Pop();
            var _r2_1 = _memo.Results.Pop();

            if (_r2_1 != null && _r2_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i2, _index, _memo.InputEnumerable, _r2_1.Results.Concat(_r2_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i2;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label1; }

            // CALLORVAR SEMI
            _Parser_Item _r17;

            _r17 = _MemoCall(_memo, "SEMI", _index, SEMI, null);

            if (_r17 != null) _index = _r17.NextIndex;

        label1: // AND
            var _r1_2 = _memo.Results.Pop();
            var _r1_1 = _memo.Results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i1, _index, _memo.InputEnumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i1;
            }

            // ACT
            var _r0 = _memo.Results.Peek();
            if (_r0 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _Parser_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return new AST.Rule(name, parms.Inputs.Any() ? new AST.Args(parms, body) : (AST.Node)body, Trimmed(ovr)); }, _r0), true) );
            }

        }


        public void Disjunction(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            // AND 1
            int _start_i1 = _index;

            // CALLORVAR ActionExp
            _Parser_Item _r2;

            _r2 = _MemoCall(_memo, "ActionExp", _index, ActionExp, null);

            if (_r2 != null) _index = _r2.NextIndex;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label1; }

            // STAR 3
            int _start_i3 = _index;
            var _res3 = Enumerable.Empty<AST.Node>();
        label3:

            // AND 4
            int _start_i4 = _index;

            // CALLORVAR OR
            _Parser_Item _r5;

            _r5 = _MemoCall(_memo, "OR", _index, OR, null);

            if (_r5 != null) _index = _r5.NextIndex;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label4; }

            // CALLORVAR ActionExp
            _Parser_Item _r6;

            _r6 = _MemoCall(_memo, "ActionExp", _index, ActionExp, null);

            if (_r6 != null) _index = _r6.NextIndex;

        label4: // AND
            var _r4_2 = _memo.Results.Pop();
            var _r4_1 = _memo.Results.Pop();

            if (_r4_1 != null && _r4_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i4, _index, _memo.InputEnumerable, _r4_1.Results.Concat(_r4_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i4;
            }

            // STAR 3
            var _r3 = _memo.Results.Pop();
            if (_r3 != null)
            {
                _res3 = _res3.Concat(_r3.Results);
                goto label3;
            }
            else
            {
                _memo.Results.Push(new _Parser_Item(_start_i3, _index, _memo.InputEnumerable, _res3.Where(_NON_NULL), true));
            }

        label1: // AND
            var _r1_2 = _memo.Results.Pop();
            var _r1_1 = _memo.Results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i1, _index, _memo.InputEnumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i1;
            }

            // ACT
            var _r0 = _memo.Results.Peek();
            if (_r0 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _Parser_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return _IM_Result.Results
                    .Where(node => node != null)
                    .Aggregate<AST.Node, AST.Node>(null, (prev, cur) => prev != null ? new AST.Or(prev, cur) : cur); }, _r0), true) );
            }

        }


        public void ActionExp(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            _Parser_Item body = null;
            _Parser_Item action = null;

            // OR 0
            int _start_i0 = _index;

            // AND 2
            int _start_i2 = _index;

            // AND 3
            int _start_i3 = _index;

            // AND 4
            int _start_i4 = _index;

            // CALLORVAR SequenceExp
            _Parser_Item _r6;

            _r6 = _MemoCall(_memo, "SequenceExp", _index, SequenceExp, null);

            if (_r6 != null) _index = _r6.NextIndex;

            // BIND body
            body = _memo.Results.Peek();

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label4; }

            // CALLORVAR ACTION
            _Parser_Item _r7;

            _r7 = _MemoCall(_memo, "ACTION", _index, ACTION, null);

            if (_r7 != null) _index = _r7.NextIndex;

        label4: // AND
            var _r4_2 = _memo.Results.Pop();
            var _r4_1 = _memo.Results.Pop();

            if (_r4_1 != null && _r4_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i4, _index, _memo.InputEnumerable, _r4_1.Results.Concat(_r4_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i4;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label3; }

            // LOOK 8
            int _start_i8 = _index;

            // CALLORVAR BRA
            _Parser_Item _r9;

            _r9 = _MemoCall(_memo, "BRA", _index, BRA, null);

            if (_r9 != null) _index = _r9.NextIndex;

            // LOOK 8
            var _r8 = _memo.Results.Pop();
            _memo.Results.Push( _r8 != null ? new _Parser_Item(_start_i8, _memo.InputEnumerable) : null );
            _index = _start_i8;

        label3: // AND
            var _r3_2 = _memo.Results.Pop();
            var _r3_1 = _memo.Results.Pop();

            if (_r3_1 != null && _r3_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i3, _index, _memo.InputEnumerable, _r3_1.Results.Concat(_r3_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i3;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label2; }

            // CALLORVAR CSharpCode
            _Parser_Item _r11;

            _r11 = _MemoCall(_memo, "CSharpCode", _index, CSharpCode, null);

            if (_r11 != null) _index = _r11.NextIndex;

            // BIND action
            action = _memo.Results.Peek();

        label2: // AND
            var _r2_2 = _memo.Results.Pop();
            var _r2_1 = _memo.Results.Pop();

            if (_r2_1 != null && _r2_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i2, _index, _memo.InputEnumerable, _r2_1.Results.Concat(_r2_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i2;
            }

            // ACT
            var _r1 = _memo.Results.Peek();
            if (_r1 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _Parser_Item(_r1.StartIndex, _r1.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return new AST.Act(body, action); }, _r1), true) );
            }

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i0; } else goto label0;

            // CALLORVAR SequenceExp
            _Parser_Item _r12;

            _r12 = _MemoCall(_memo, "SequenceExp", _index, SequenceExp, null);

            if (_r12 != null) _index = _r12.NextIndex;

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void FailExp(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            _Parser_Item msg = null;

            // AND 1
            int _start_i1 = _index;

            // CALLORVAR BANG
            _Parser_Item _r2;

            _r2 = _MemoCall(_memo, "BANG", _index, BANG, null);

            if (_r2 != null) _index = _r2.NextIndex;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label1; }

            // CALLORVAR CSharpCode
            _Parser_Item _r5;

            _r5 = _MemoCall(_memo, "CSharpCode", _index, CSharpCode, null);

            if (_r5 != null) _index = _r5.NextIndex;

            // BIND msg
            msg = _memo.Results.Peek();

            // QUES
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _memo.Results.Push(new _Parser_Item(_index, _memo.InputEnumerable)); }

        label1: // AND
            var _r1_2 = _memo.Results.Pop();
            var _r1_1 = _memo.Results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i1, _index, _memo.InputEnumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i1;
            }

            // ACT
            var _r0 = _memo.Results.Peek();
            if (_r0 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _Parser_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return new AST.Fail(msg); }, _r0), true) );
            }

        }


        public void SequenceExp(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            // PLUS 1
            int _start_i1 = _index;
            var _res1 = Enumerable.Empty<AST.Node>();
        label1:

            // CALLORVAR ConditionExp
            _Parser_Item _r2;

            _r2 = _MemoCall(_memo, "ConditionExp", _index, ConditionExp, null);

            if (_r2 != null) _index = _r2.NextIndex;

            // PLUS 1
            var _r1 = _memo.Results.Pop();
            if (_r1 != null)
            {
                _res1 = _res1.Concat(_r1.Results);
                goto label1;
            }
            else
            {
                if (_index > _start_i1)
                    _memo.Results.Push(new _Parser_Item(_start_i1, _index, _memo.InputEnumerable, _res1.Where(_NON_NULL), true));
                else
                    _memo.Results.Push(null);
            }

            // ACT
            var _r0 = _memo.Results.Peek();
            if (_r0 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _Parser_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return _IM_Result.Results
                    .Where(node => node != null)
                    .Aggregate<AST.Node, AST.Node>(null, (prev, cur) => prev != null ? new AST.And(prev, cur) : cur); }, _r0), true) );
            }

        }


        public void ConditionExp(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            _Parser_Item body = null;
            _Parser_Item cond = null;

            // OR 0
            int _start_i0 = _index;

            // OR 1
            int _start_i1 = _index;

            // CALLORVAR FailExp
            _Parser_Item _r2;

            _r2 = _MemoCall(_memo, "FailExp", _index, FailExp, null);

            if (_r2 != null) _index = _r2.NextIndex;

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i1; } else goto label1;

            // AND 4
            int _start_i4 = _index;

            // AND 5
            int _start_i5 = _index;

            // AND 6
            int _start_i6 = _index;

            // CALLORVAR BoundTerm
            _Parser_Item _r8;

            _r8 = _MemoCall(_memo, "BoundTerm", _index, BoundTerm, null);

            if (_r8 != null) _index = _r8.NextIndex;

            // BIND body
            body = _memo.Results.Peek();

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label6; }

            // CALLORVAR QUES
            _Parser_Item _r9;

            _r9 = _MemoCall(_memo, "QUES", _index, QUES, null);

            if (_r9 != null) _index = _r9.NextIndex;

        label6: // AND
            var _r6_2 = _memo.Results.Pop();
            var _r6_1 = _memo.Results.Pop();

            if (_r6_1 != null && _r6_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i6, _index, _memo.InputEnumerable, _r6_1.Results.Concat(_r6_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i6;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label5; }

            // LOOK 10
            int _start_i10 = _index;

            // CALLORVAR OPEN
            _Parser_Item _r11;

            _r11 = _MemoCall(_memo, "OPEN", _index, OPEN, null);

            if (_r11 != null) _index = _r11.NextIndex;

            // LOOK 10
            var _r10 = _memo.Results.Pop();
            _memo.Results.Push( _r10 != null ? new _Parser_Item(_start_i10, _memo.InputEnumerable) : null );
            _index = _start_i10;

        label5: // AND
            var _r5_2 = _memo.Results.Pop();
            var _r5_1 = _memo.Results.Pop();

            if (_r5_1 != null && _r5_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i5, _index, _memo.InputEnumerable, _r5_1.Results.Concat(_r5_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i5;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label4; }

            // CALLORVAR CSharpCode
            _Parser_Item _r13;

            _r13 = _MemoCall(_memo, "CSharpCode", _index, CSharpCode, null);

            if (_r13 != null) _index = _r13.NextIndex;

            // BIND cond
            cond = _memo.Results.Peek();

        label4: // AND
            var _r4_2 = _memo.Results.Pop();
            var _r4_1 = _memo.Results.Pop();

            if (_r4_1 != null && _r4_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i4, _index, _memo.InputEnumerable, _r4_1.Results.Concat(_r4_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i4;
            }

            // ACT
            var _r3 = _memo.Results.Peek();
            if (_r3 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _Parser_Item(_r3.StartIndex, _r3.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return new AST.Cond(body, cond); }, _r3), true) );
            }

        label1: // OR
            int _dummy_i1 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i0; } else goto label0;

            // CALLORVAR BoundTerm
            _Parser_Item _r14;

            _r14 = _MemoCall(_memo, "BoundTerm", _index, BoundTerm, null);

            if (_r14 != null) _index = _r14.NextIndex;

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void BoundTerm(_Parser_Memo _memo, int _index, _Parser_Args _args)
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

            _r6 = _MemoCall(_memo, "PrefixedTerm", _index, PrefixedTerm, null);

            if (_r6 != null) _index = _r6.NextIndex;

            // BIND exp
            exp = _memo.Results.Peek();

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label4; }

            // CALLORVAR COLON
            _Parser_Item _r7;

            _r7 = _MemoCall(_memo, "COLON", _index, COLON, null);

            if (_r7 != null) _index = _r7.NextIndex;

        label4: // AND
            var _r4_2 = _memo.Results.Pop();
            var _r4_1 = _memo.Results.Pop();

            if (_r4_1 != null && _r4_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i4, _index, _memo.InputEnumerable, _r4_1.Results.Concat(_r4_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i4;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label3; }

            // CALLORVAR Identifier
            _Parser_Item _r9;

            _r9 = _MemoCall(_memo, "Identifier", _index, Identifier, null);

            if (_r9 != null) _index = _r9.NextIndex;

            // BIND id
            id = _memo.Results.Peek();

        label3: // AND
            var _r3_2 = _memo.Results.Pop();
            var _r3_1 = _memo.Results.Pop();

            if (_r3_1 != null && _r3_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i3, _index, _memo.InputEnumerable, _r3_1.Results.Concat(_r3_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i3;
            }

            // ACT
            var _r2 = _memo.Results.Peek();
            if (_r2 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _Parser_Item(_r2.StartIndex, _r2.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return new AST.Bind(exp, id); }, _r2), true) );
            }

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i1; } else goto label1;

            // AND 11
            int _start_i11 = _index;

            // CALLORVAR COLON
            _Parser_Item _r12;

            _r12 = _MemoCall(_memo, "COLON", _index, COLON, null);

            if (_r12 != null) _index = _r12.NextIndex;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label11; }

            // CALLORVAR Identifier
            _Parser_Item _r14;

            _r14 = _MemoCall(_memo, "Identifier", _index, Identifier, null);

            if (_r14 != null) _index = _r14.NextIndex;

            // BIND id
            id = _memo.Results.Peek();

        label11: // AND
            var _r11_2 = _memo.Results.Pop();
            var _r11_1 = _memo.Results.Pop();

            if (_r11_1 != null && _r11_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i11, _index, _memo.InputEnumerable, _r11_1.Results.Concat(_r11_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i11;
            }

            // ACT
            var _r10 = _memo.Results.Peek();
            if (_r10 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _Parser_Item(_r10.StartIndex, _r10.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return new AST.Bind(new AST.Any(), id); }, _r10), true) );
            }

        label1: // OR
            int _dummy_i1 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i0; } else goto label0;

            // CALLORVAR PrefixedTerm
            _Parser_Item _r15;

            _r15 = _MemoCall(_memo, "PrefixedTerm", _index, PrefixedTerm, null);

            if (_r15 != null) _index = _r15.NextIndex;

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void PrefixedTerm(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            // OR 0
            int _start_i0 = _index;

            // OR 1
            int _start_i1 = _index;

            // CALLORVAR LookTerm
            _Parser_Item _r2;

            _r2 = _MemoCall(_memo, "LookTerm", _index, LookTerm, null);

            if (_r2 != null) _index = _r2.NextIndex;

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i1; } else goto label1;

            // CALLORVAR NotTerm
            _Parser_Item _r3;

            _r3 = _MemoCall(_memo, "NotTerm", _index, NotTerm, null);

            if (_r3 != null) _index = _r3.NextIndex;

        label1: // OR
            int _dummy_i1 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i0; } else goto label0;

            // CALLORVAR PostfixedTerm
            _Parser_Item _r4;

            _r4 = _MemoCall(_memo, "PostfixedTerm", _index, PostfixedTerm, null);

            if (_r4 != null) _index = _r4.NextIndex;

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void LookTerm(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            _Parser_Item exp = null;

            // AND 1
            int _start_i1 = _index;

            // CALLORVAR AND_PRE
            _Parser_Item _r2;

            _r2 = _MemoCall(_memo, "AND_PRE", _index, AND_PRE, null);

            if (_r2 != null) _index = _r2.NextIndex;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label1; }

            // CALLORVAR PostfixedTerm
            _Parser_Item _r4;

            _r4 = _MemoCall(_memo, "PostfixedTerm", _index, PostfixedTerm, null);

            if (_r4 != null) _index = _r4.NextIndex;

            // BIND exp
            exp = _memo.Results.Peek();

        label1: // AND
            var _r1_2 = _memo.Results.Pop();
            var _r1_1 = _memo.Results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i1, _index, _memo.InputEnumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i1;
            }

            // ACT
            var _r0 = _memo.Results.Peek();
            if (_r0 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _Parser_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return new AST.Look(exp); }, _r0), true) );
            }

        }


        public void NotTerm(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            _Parser_Item exp = null;

            // AND 1
            int _start_i1 = _index;

            // CALLORVAR NOT_PRE
            _Parser_Item _r2;

            _r2 = _MemoCall(_memo, "NOT_PRE", _index, NOT_PRE, null);

            if (_r2 != null) _index = _r2.NextIndex;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label1; }

            // CALLORVAR PostfixedTerm
            _Parser_Item _r4;

            _r4 = _MemoCall(_memo, "PostfixedTerm", _index, PostfixedTerm, null);

            if (_r4 != null) _index = _r4.NextIndex;

            // BIND exp
            exp = _memo.Results.Peek();

        label1: // AND
            var _r1_2 = _memo.Results.Pop();
            var _r1_1 = _memo.Results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i1, _index, _memo.InputEnumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i1;
            }

            // ACT
            var _r0 = _memo.Results.Peek();
            if (_r0 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _Parser_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return new AST.Not(exp); }, _r0), true) );
            }

        }


        public void PostfixedTerm(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            // OR 0
            int _start_i0 = _index;

            // OR 1
            int _start_i1 = _index;

            // OR 2
            int _start_i2 = _index;

            // OR 3
            int _start_i3 = _index;

            // CALLORVAR MinMaxTerm
            _Parser_Item _r4;

            _r4 = _MemoCall(_memo, "MinMaxTerm", _index, MinMaxTerm, null);

            if (_r4 != null) _index = _r4.NextIndex;

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i3; } else goto label3;

            // CALLORVAR StarTerm
            _Parser_Item _r5;

            _r5 = _MemoCall(_memo, "StarTerm", _index, StarTerm, null);

            if (_r5 != null) _index = _r5.NextIndex;

        label3: // OR
            int _dummy_i3 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i2; } else goto label2;

            // CALLORVAR PlusTerm
            _Parser_Item _r6;

            _r6 = _MemoCall(_memo, "PlusTerm", _index, PlusTerm, null);

            if (_r6 != null) _index = _r6.NextIndex;

        label2: // OR
            int _dummy_i2 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i1; } else goto label1;

            // CALLORVAR QuesTerm
            _Parser_Item _r7;

            _r7 = _MemoCall(_memo, "QuesTerm", _index, QuesTerm, null);

            if (_r7 != null) _index = _r7.NextIndex;

        label1: // OR
            int _dummy_i1 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i0; } else goto label0;

            // CALLORVAR Term
            _Parser_Item _r8;

            _r8 = _MemoCall(_memo, "Term", _index, Term, null);

            if (_r8 != null) _index = _r8.NextIndex;

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void MinMaxTerm(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            _Parser_Item exp = null;
            _Parser_Item n = null;
            _Parser_Item x = null;

            // AND 1
            int _start_i1 = _index;

            // AND 2
            int _start_i2 = _index;

            // AND 3
            int _start_i3 = _index;

            // AND 4
            int _start_i4 = _index;

            // CALLORVAR Term
            _Parser_Item _r6;

            _r6 = _MemoCall(_memo, "Term", _index, Term, null);

            if (_r6 != null) _index = _r6.NextIndex;

            // BIND exp
            exp = _memo.Results.Peek();

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label4; }

            // CALLORVAR BRA
            _Parser_Item _r7;

            _r7 = _MemoCall(_memo, "BRA", _index, BRA, null);

            if (_r7 != null) _index = _r7.NextIndex;

        label4: // AND
            var _r4_2 = _memo.Results.Pop();
            var _r4_1 = _memo.Results.Pop();

            if (_r4_1 != null && _r4_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i4, _index, _memo.InputEnumerable, _r4_1.Results.Concat(_r4_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i4;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label3; }

            // CALLORVAR Number
            _Parser_Item _r9;

            _r9 = _MemoCall(_memo, "Number", _index, Number, null);

            if (_r9 != null) _index = _r9.NextIndex;

            // BIND n
            n = _memo.Results.Peek();

        label3: // AND
            var _r3_2 = _memo.Results.Pop();
            var _r3_1 = _memo.Results.Pop();

            if (_r3_1 != null && _r3_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i3, _index, _memo.InputEnumerable, _r3_1.Results.Concat(_r3_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i3;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label2; }

            // AND 11
            int _start_i11 = _index;

            // CALLORVAR COMMA
            _Parser_Item _r12;

            _r12 = _MemoCall(_memo, "COMMA", _index, COMMA, null);

            if (_r12 != null) _index = _r12.NextIndex;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label11; }

            // CALLORVAR Number
            _Parser_Item _r14;

            _r14 = _MemoCall(_memo, "Number", _index, Number, null);

            if (_r14 != null) _index = _r14.NextIndex;

            // BIND x
            x = _memo.Results.Peek();

        label11: // AND
            var _r11_2 = _memo.Results.Pop();
            var _r11_1 = _memo.Results.Pop();

            if (_r11_1 != null && _r11_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i11, _index, _memo.InputEnumerable, _r11_1.Results.Concat(_r11_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i11;
            }

            // QUES
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _memo.Results.Push(new _Parser_Item(_index, _memo.InputEnumerable)); }

        label2: // AND
            var _r2_2 = _memo.Results.Pop();
            var _r2_1 = _memo.Results.Pop();

            if (_r2_1 != null && _r2_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i2, _index, _memo.InputEnumerable, _r2_1.Results.Concat(_r2_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i2;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label1; }

            // CALLORVAR KET
            _Parser_Item _r15;

            _r15 = _MemoCall(_memo, "KET", _index, KET, null);

            if (_r15 != null) _index = _r15.NextIndex;

        label1: // AND
            var _r1_2 = _memo.Results.Pop();
            var _r1_1 = _memo.Results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i1, _index, _memo.InputEnumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i1;
            }

            // ACT
            var _r0 = _memo.Results.Peek();
            if (_r0 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _Parser_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { int min = int.Parse(Input(n));
            int max = x != null && x.Inputs.Any() ? int.Parse(Input(x)) : min;

            if (min < 0)
                throw new MatcherException(n.StartIndex, "min must be >= 0");
            if (max < 1)
                throw new MatcherException(x.StartIndex, "max must be > 1");
            if (max < min)
                throw new MatcherException(n.NextIndex, "max cannot be less than min for a MinMaxTerm");

            AST.Node res = null;
            for (int i = 0; i < min; ++i)
                res = res != null ? new AST.And(res, exp) : (AST.Node)exp;
            for (int i = 0; i < (max-min); ++i)
                res = res != null ? new AST.And(res, new AST.Ques(exp)) : (AST.Node)new AST.Ques(exp);
            return res; }, _r0), true) );
            }

        }


        public void StarTerm(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            _Parser_Item exp = null;

            // AND 1
            int _start_i1 = _index;

            // CALLORVAR Term
            _Parser_Item _r3;

            _r3 = _MemoCall(_memo, "Term", _index, Term, null);

            if (_r3 != null) _index = _r3.NextIndex;

            // BIND exp
            exp = _memo.Results.Peek();

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label1; }

            // CALLORVAR STAR
            _Parser_Item _r4;

            _r4 = _MemoCall(_memo, "STAR", _index, STAR, null);

            if (_r4 != null) _index = _r4.NextIndex;

        label1: // AND
            var _r1_2 = _memo.Results.Pop();
            var _r1_1 = _memo.Results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i1, _index, _memo.InputEnumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i1;
            }

            // ACT
            var _r0 = _memo.Results.Peek();
            if (_r0 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _Parser_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return new AST.Star(exp); }, _r0), true) );
            }

        }


        public void PlusTerm(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            _Parser_Item exp = null;

            // AND 1
            int _start_i1 = _index;

            // CALLORVAR Term
            _Parser_Item _r3;

            _r3 = _MemoCall(_memo, "Term", _index, Term, null);

            if (_r3 != null) _index = _r3.NextIndex;

            // BIND exp
            exp = _memo.Results.Peek();

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label1; }

            // CALLORVAR PLUS
            _Parser_Item _r4;

            _r4 = _MemoCall(_memo, "PLUS", _index, PLUS, null);

            if (_r4 != null) _index = _r4.NextIndex;

        label1: // AND
            var _r1_2 = _memo.Results.Pop();
            var _r1_1 = _memo.Results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i1, _index, _memo.InputEnumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i1;
            }

            // ACT
            var _r0 = _memo.Results.Peek();
            if (_r0 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _Parser_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return new AST.Plus(exp); }, _r0), true) );
            }

        }


        public void QuesTerm(_Parser_Memo _memo, int _index, _Parser_Args _args)
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

            _r5 = _MemoCall(_memo, "Term", _index, Term, null);

            if (_r5 != null) _index = _r5.NextIndex;

            // BIND exp
            exp = _memo.Results.Peek();

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label3; }

            // CALLORVAR QUES
            _Parser_Item _r6;

            _r6 = _MemoCall(_memo, "QUES", _index, QUES, null);

            if (_r6 != null) _index = _r6.NextIndex;

        label3: // AND
            var _r3_2 = _memo.Results.Pop();
            var _r3_1 = _memo.Results.Pop();

            if (_r3_1 != null && _r3_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i3, _index, _memo.InputEnumerable, _r3_1.Results.Concat(_r3_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i3;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label2; }

            // NOT 7
            int _start_i7 = _index;

            // CALLORVAR OPEN
            _Parser_Item _r8;

            _r8 = _MemoCall(_memo, "OPEN", _index, OPEN, null);

            if (_r8 != null) _index = _r8.NextIndex;

            // NOT 7
            var _r7 = _memo.Results.Pop();
            _memo.Results.Push( _r7 == null ? new _Parser_Item(_start_i7, _memo.InputEnumerable) : null);
            _index = _start_i7;

        label2: // AND
            var _r2_2 = _memo.Results.Pop();
            var _r2_1 = _memo.Results.Pop();

            if (_r2_1 != null && _r2_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i2, _index, _memo.InputEnumerable, _r2_1.Results.Concat(_r2_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i2;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label1; }

            // CALLORVAR SP
            _Parser_Item _r9;

            _r9 = _MemoCall(_memo, "SP", _index, SP, null);

            if (_r9 != null) _index = _r9.NextIndex;

        label1: // AND
            var _r1_2 = _memo.Results.Pop();
            var _r1_1 = _memo.Results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i1, _index, _memo.InputEnumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i1;
            }

            // ACT
            var _r0 = _memo.Results.Peek();
            if (_r0 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _Parser_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return new AST.Ques(exp); }, _r0), true) );
            }

        }


        public void Term(_Parser_Memo _memo, int _index, _Parser_Args _args)
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

            _r5 = _MemoCall(_memo, "InputClass", _index, InputClass, null);

            if (_r5 != null) _index = _r5.NextIndex;

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i4; } else goto label4;

            // CALLORVAR ParenTerm
            _Parser_Item _r6;

            _r6 = _MemoCall(_memo, "ParenTerm", _index, ParenTerm, null);

            if (_r6 != null) _index = _r6.NextIndex;

        label4: // OR
            int _dummy_i4 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i3; } else goto label3;

            // CALLORVAR RuleCall
            _Parser_Item _r7;

            _r7 = _MemoCall(_memo, "RuleCall", _index, RuleCall, null);

            if (_r7 != null) _index = _r7.NextIndex;

        label3: // OR
            int _dummy_i3 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i2; } else goto label2;

            // CALLORVAR CallOrVar
            _Parser_Item _r8;

            _r8 = _MemoCall(_memo, "CallOrVar", _index, CallOrVar, null);

            if (_r8 != null) _index = _r8.NextIndex;

        label2: // OR
            int _dummy_i2 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i1; } else goto label1;

            // CALLORVAR Literal
            _Parser_Item _r9;

            _r9 = _MemoCall(_memo, "Literal", _index, Literal, null);

            if (_r9 != null) _index = _r9.NextIndex;

        label1: // OR
            int _dummy_i1 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i0; } else goto label0;

            // CALLORVAR AnyTerm
            _Parser_Item _r10;

            _r10 = _MemoCall(_memo, "AnyTerm", _index, AnyTerm, null);

            if (_r10 != null) _index = _r10.NextIndex;

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void ParenTerm(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            _Parser_Item exp = null;

            // AND 1
            int _start_i1 = _index;

            // AND 2
            int _start_i2 = _index;

            // CALLORVAR OPEN
            _Parser_Item _r3;

            _r3 = _MemoCall(_memo, "OPEN", _index, OPEN, null);

            if (_r3 != null) _index = _r3.NextIndex;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label2; }

            // CALLORVAR Disjunction
            _Parser_Item _r5;

            _r5 = _MemoCall(_memo, "Disjunction", _index, Disjunction, null);

            if (_r5 != null) _index = _r5.NextIndex;

            // BIND exp
            exp = _memo.Results.Peek();

        label2: // AND
            var _r2_2 = _memo.Results.Pop();
            var _r2_1 = _memo.Results.Pop();

            if (_r2_1 != null && _r2_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i2, _index, _memo.InputEnumerable, _r2_1.Results.Concat(_r2_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i2;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label1; }

            // CALLORVAR CLOSE
            _Parser_Item _r6;

            _r6 = _MemoCall(_memo, "CLOSE", _index, CLOSE, null);

            if (_r6 != null) _index = _r6.NextIndex;

        label1: // AND
            var _r1_2 = _memo.Results.Pop();
            var _r1_1 = _memo.Results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i1, _index, _memo.InputEnumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i1;
            }

            // ACT
            var _r0 = _memo.Results.Peek();
            if (_r0 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _Parser_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return exp; }, _r0), true) );
            }

        }


        public void AnyTerm(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            // CALLORVAR PERIOD
            _Parser_Item _r1;

            _r1 = _MemoCall(_memo, "PERIOD", _index, PERIOD, null);

            if (_r1 != null) _index = _r1.NextIndex;

            // ACT
            var _r0 = _memo.Results.Peek();
            if (_r0 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _Parser_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return new AST.Any(); }, _r0), true) );
            }

        }


        public void RuleCall(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            _Parser_Item name = null;
            _Parser_Item p = null;

            // AND 1
            int _start_i1 = _index;

            // AND 2
            int _start_i2 = _index;

            // AND 3
            int _start_i3 = _index;

            // CALLORVAR QualifiedId
            _Parser_Item _r5;

            _r5 = _MemoCall(_memo, "QualifiedId", _index, QualifiedId, null);

            if (_r5 != null) _index = _r5.NextIndex;

            // BIND name
            name = _memo.Results.Peek();

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label3; }

            // CALLORVAR OPEN
            _Parser_Item _r6;

            _r6 = _MemoCall(_memo, "OPEN", _index, OPEN, null);

            if (_r6 != null) _index = _r6.NextIndex;

        label3: // AND
            var _r3_2 = _memo.Results.Pop();
            var _r3_1 = _memo.Results.Pop();

            if (_r3_1 != null && _r3_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i3, _index, _memo.InputEnumerable, _r3_1.Results.Concat(_r3_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i3;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label2; }

            // CALLORVAR ParameterList
            _Parser_Item _r9;

            _r9 = _MemoCall(_memo, "ParameterList", _index, ParameterList, null);

            if (_r9 != null) _index = _r9.NextIndex;

            // QUES
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _memo.Results.Push(new _Parser_Item(_index, _memo.InputEnumerable)); }

            // BIND p
            p = _memo.Results.Peek();

        label2: // AND
            var _r2_2 = _memo.Results.Pop();
            var _r2_1 = _memo.Results.Pop();

            if (_r2_1 != null && _r2_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i2, _index, _memo.InputEnumerable, _r2_1.Results.Concat(_r2_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i2;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label1; }

            // CALLORVAR CLOSE
            _Parser_Item _r10;

            _r10 = _MemoCall(_memo, "CLOSE", _index, CLOSE, null);

            if (_r10 != null) _index = _r10.NextIndex;

        label1: // AND
            var _r1_2 = _memo.Results.Pop();
            var _r1_1 = _memo.Results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i1, _index, _memo.InputEnumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i1;
            }

            // ACT
            var _r0 = _memo.Results.Peek();
            if (_r0 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _Parser_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return new AST.Call(name, p.Results); }, _r0), true) );
            }

        }


        public void ParameterList(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            // AND 1
            int _start_i1 = _index;

            // CALLORVAR Parameter
            _Parser_Item _r2;

            _r2 = _MemoCall(_memo, "Parameter", _index, Parameter, null);

            if (_r2 != null) _index = _r2.NextIndex;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label1; }

            // STAR 3
            int _start_i3 = _index;
            var _res3 = Enumerable.Empty<AST.Node>();
        label3:

            // AND 4
            int _start_i4 = _index;

            // CALLORVAR COMMA
            _Parser_Item _r5;

            _r5 = _MemoCall(_memo, "COMMA", _index, COMMA, null);

            if (_r5 != null) _index = _r5.NextIndex;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label4; }

            // CALLORVAR Parameter
            _Parser_Item _r6;

            _r6 = _MemoCall(_memo, "Parameter", _index, Parameter, null);

            if (_r6 != null) _index = _r6.NextIndex;

        label4: // AND
            var _r4_2 = _memo.Results.Pop();
            var _r4_1 = _memo.Results.Pop();

            if (_r4_1 != null && _r4_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i4, _index, _memo.InputEnumerable, _r4_1.Results.Concat(_r4_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i4;
            }

            // STAR 3
            var _r3 = _memo.Results.Pop();
            if (_r3 != null)
            {
                _res3 = _res3.Concat(_r3.Results);
                goto label3;
            }
            else
            {
                _memo.Results.Push(new _Parser_Item(_start_i3, _index, _memo.InputEnumerable, _res3.Where(_NON_NULL), true));
            }

        label1: // AND
            var _r1_2 = _memo.Results.Pop();
            var _r1_1 = _memo.Results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i1, _index, _memo.InputEnumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i1;
            }

            // ACT
            var _r0 = _memo.Results.Peek();
            if (_r0 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _Parser_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return _IM_Result.Results.Where(node => node != null); }, _r0), true) );
            }

        }


        public void Parameter(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            // OR 0
            int _start_i0 = _index;

            // CALLORVAR CallOrVar
            _Parser_Item _r1;

            _r1 = _MemoCall(_memo, "CallOrVar", _index, CallOrVar, null);

            if (_r1 != null) _index = _r1.NextIndex;

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i0; } else goto label0;

            // CALLORVAR Literal
            _Parser_Item _r2;

            _r2 = _MemoCall(_memo, "Literal", _index, Literal, null);

            if (_r2 != null) _index = _r2.NextIndex;

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void CallOrVar(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            _Parser_Item name = null;

            // AND 1
            int _start_i1 = _index;

            // NOT 2
            int _start_i2 = _index;

            // CALLORVAR KEYWORD
            _Parser_Item _r3;

            _r3 = _MemoCall(_memo, "KEYWORD", _index, KEYWORD, null);

            if (_r3 != null) _index = _r3.NextIndex;

            // NOT 2
            var _r2 = _memo.Results.Pop();
            _memo.Results.Push( _r2 == null ? new _Parser_Item(_start_i2, _memo.InputEnumerable) : null);
            _index = _start_i2;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label1; }

            // CALLORVAR QualifiedId
            _Parser_Item _r5;

            _r5 = _MemoCall(_memo, "QualifiedId", _index, QualifiedId, null);

            if (_r5 != null) _index = _r5.NextIndex;

            // BIND name
            name = _memo.Results.Peek();

        label1: // AND
            var _r1_2 = _memo.Results.Pop();
            var _r1_1 = _memo.Results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i1, _index, _memo.InputEnumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i1;
            }

            // ACT
            var _r0 = _memo.Results.Peek();
            if (_r0 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _Parser_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return new AST.CallOrVar(name); }, _r0), true) );
            }

        }


        public void Literal(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            // AND 1
            int _start_i1 = _index;

            // OR 2
            int _start_i2 = _index;

            // AND 3
            int _start_i3 = _index;

            // AND 4
            int _start_i4 = _index;

            // CALLORVAR NEW
            _Parser_Item _r5;

            _r5 = _MemoCall(_memo, "NEW", _index, NEW, null);

            if (_r5 != null) _index = _r5.NextIndex;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label4; }

            // CALLORVAR GenericId
            _Parser_Item _r7;

            _r7 = _MemoCall(_memo, "GenericId", _index, GenericId, null);

            if (_r7 != null) _index = _r7.NextIndex;

            // QUES
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _memo.Results.Push(new _Parser_Item(_index, _memo.InputEnumerable)); }

        label4: // AND
            var _r4_2 = _memo.Results.Pop();
            var _r4_1 = _memo.Results.Pop();

            if (_r4_1 != null && _r4_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i4, _index, _memo.InputEnumerable, _r4_1.Results.Concat(_r4_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i4;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label3; }

            // LOOK 8
            int _start_i8 = _index;

            // LITERAL '{'
            _ParseLiteralChar(_memo, ref _index, '{');

            // LOOK 8
            var _r8 = _memo.Results.Pop();
            _memo.Results.Push( _r8 != null ? new _Parser_Item(_start_i8, _memo.InputEnumerable) : null );
            _index = _start_i8;

        label3: // AND
            var _r3_2 = _memo.Results.Pop();
            var _r3_1 = _memo.Results.Pop();

            if (_r3_1 != null && _r3_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i3, _index, _memo.InputEnumerable, _r3_1.Results.Concat(_r3_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i3;
            }

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i2; } else goto label2;

            // LOOK 10
            int _start_i10 = _index;

            // OR 11
            int _start_i11 = _index;

            // OR 12
            int _start_i12 = _index;

            // LITERAL '\x22'
            _ParseLiteralChar(_memo, ref _index, '\x22');

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i12; } else goto label12;

            // LITERAL '\x27'
            _ParseLiteralChar(_memo, ref _index, '\x27');

        label12: // OR
            int _dummy_i12 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i11; } else goto label11;

            // LITERAL '{'
            _ParseLiteralChar(_memo, ref _index, '{');

        label11: // OR
            int _dummy_i11 = _index; // no-op for label

            // LOOK 10
            var _r10 = _memo.Results.Pop();
            _memo.Results.Push( _r10 != null ? new _Parser_Item(_start_i10, _memo.InputEnumerable) : null );
            _index = _start_i10;

        label2: // OR
            int _dummy_i2 = _index; // no-op for label

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label1; }

            // CALLORVAR CSharpCode
            _Parser_Item _r16;

            _r16 = _MemoCall(_memo, "CSharpCode", _index, CSharpCode, null);

            if (_r16 != null) _index = _r16.NextIndex;

        label1: // AND
            var _r1_2 = _memo.Results.Pop();
            var _r1_1 = _memo.Results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i1, _index, _memo.InputEnumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i1;
            }

            // ACT
            var _r0 = _memo.Results.Peek();
            if (_r0 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _Parser_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return new AST.Code(_IM_Result); }, _r0), true) );
            }

        }


        public void InputClass(_Parser_Memo _memo, int _index, _Parser_Args _args)
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
            _ParseLiteralChar(_memo, ref _index, '[');

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label4; }

            // CALLORVAR SP
            _Parser_Item _r6;

            _r6 = _MemoCall(_memo, "SP", _index, SP, null);

            if (_r6 != null) _index = _r6.NextIndex;

        label4: // AND
            var _r4_2 = _memo.Results.Pop();
            var _r4_1 = _memo.Results.Pop();

            if (_r4_1 != null && _r4_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i4, _index, _memo.InputEnumerable, _r4_1.Results.Concat(_r4_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i4;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label3; }

            // PLUS 8
            int _start_i8 = _index;
            var _res8 = Enumerable.Empty<AST.Node>();
        label8:

            // OR 9
            int _start_i9 = _index;

            // CALLORVAR ClassRange
            _Parser_Item _r10;

            _r10 = _MemoCall(_memo, "ClassRange", _index, ClassRange, null);

            if (_r10 != null) _index = _r10.NextIndex;

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i9; } else goto label9;

            // AND 11
            int _start_i11 = _index;

            // LOOK 12
            int _start_i12 = _index;

            // LITERAL '\x27'
            _ParseLiteralChar(_memo, ref _index, '\x27');

            // LOOK 12
            var _r12 = _memo.Results.Pop();
            _memo.Results.Push( _r12 != null ? new _Parser_Item(_start_i12, _memo.InputEnumerable) : null );
            _index = _start_i12;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label11; }

            // CALLORVAR Literal
            _Parser_Item _r14;

            _r14 = _MemoCall(_memo, "Literal", _index, Literal, null);

            if (_r14 != null) _index = _r14.NextIndex;

        label11: // AND
            var _r11_2 = _memo.Results.Pop();
            var _r11_1 = _memo.Results.Pop();

            if (_r11_1 != null && _r11_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i11, _index, _memo.InputEnumerable, _r11_1.Results.Concat(_r11_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i11;
            }

        label9: // OR
            int _dummy_i9 = _index; // no-op for label

            // PLUS 8
            var _r8 = _memo.Results.Pop();
            if (_r8 != null)
            {
                _res8 = _res8.Concat(_r8.Results);
                goto label8;
            }
            else
            {
                if (_index > _start_i8)
                    _memo.Results.Push(new _Parser_Item(_start_i8, _index, _memo.InputEnumerable, _res8.Where(_NON_NULL), true));
                else
                    _memo.Results.Push(null);
            }

            // BIND c
            c = _memo.Results.Peek();

        label3: // AND
            var _r3_2 = _memo.Results.Pop();
            var _r3_1 = _memo.Results.Pop();

            if (_r3_1 != null && _r3_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i3, _index, _memo.InputEnumerable, _r3_1.Results.Concat(_r3_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i3;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label2; }

            // LITERAL ']'
            _ParseLiteralChar(_memo, ref _index, ']');

        label2: // AND
            var _r2_2 = _memo.Results.Pop();
            var _r2_1 = _memo.Results.Pop();

            if (_r2_1 != null && _r2_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i2, _index, _memo.InputEnumerable, _r2_1.Results.Concat(_r2_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i2;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label1; }

            // CALLORVAR SP
            _Parser_Item _r16;

            _r16 = _MemoCall(_memo, "SP", _index, SP, null);

            if (_r16 != null) _index = _r16.NextIndex;

        label1: // AND
            var _r1_2 = _memo.Results.Pop();
            var _r1_1 = _memo.Results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i1, _index, _memo.InputEnumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i1;
            }

            // ACT
            var _r0 = _memo.Results.Peek();
            if (_r0 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _Parser_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return new AST.InputClass(c.Results); }, _r0), true) );
            }

        }


        public void ClassRange(_Parser_Memo _memo, int _index, _Parser_Args _args)
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
            _ParseLiteralChar(_memo, ref _index, '\x27');

            // LOOK 8
            var _r8 = _memo.Results.Pop();
            _memo.Results.Push( _r8 != null ? new _Parser_Item(_start_i8, _memo.InputEnumerable) : null );
            _index = _start_i8;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label7; }

            // CALLORVAR Literal
            _Parser_Item _r11;

            _r11 = _MemoCall(_memo, "Literal", _index, Literal, null);

            if (_r11 != null) _index = _r11.NextIndex;

            // BIND from
            from = _memo.Results.Peek();

        label7: // AND
            var _r7_2 = _memo.Results.Pop();
            var _r7_1 = _memo.Results.Pop();

            if (_r7_1 != null && _r7_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i7, _index, _memo.InputEnumerable, _r7_1.Results.Concat(_r7_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i7;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label6; }

            // CALLORVAR SP
            _Parser_Item _r12;

            _r12 = _MemoCall(_memo, "SP", _index, SP, null);

            if (_r12 != null) _index = _r12.NextIndex;

        label6: // AND
            var _r6_2 = _memo.Results.Pop();
            var _r6_1 = _memo.Results.Pop();

            if (_r6_1 != null && _r6_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i6, _index, _memo.InputEnumerable, _r6_1.Results.Concat(_r6_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i6;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label5; }

            // LITERAL '-'
            _ParseLiteralChar(_memo, ref _index, '-');

        label5: // AND
            var _r5_2 = _memo.Results.Pop();
            var _r5_1 = _memo.Results.Pop();

            if (_r5_1 != null && _r5_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i5, _index, _memo.InputEnumerable, _r5_1.Results.Concat(_r5_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i5;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label4; }

            // CALLORVAR SP
            _Parser_Item _r14;

            _r14 = _MemoCall(_memo, "SP", _index, SP, null);

            if (_r14 != null) _index = _r14.NextIndex;

        label4: // AND
            var _r4_2 = _memo.Results.Pop();
            var _r4_1 = _memo.Results.Pop();

            if (_r4_1 != null && _r4_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i4, _index, _memo.InputEnumerable, _r4_1.Results.Concat(_r4_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i4;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label3; }

            // LOOK 15
            int _start_i15 = _index;

            // LITERAL '\x27'
            _ParseLiteralChar(_memo, ref _index, '\x27');

            // LOOK 15
            var _r15 = _memo.Results.Pop();
            _memo.Results.Push( _r15 != null ? new _Parser_Item(_start_i15, _memo.InputEnumerable) : null );
            _index = _start_i15;

        label3: // AND
            var _r3_2 = _memo.Results.Pop();
            var _r3_1 = _memo.Results.Pop();

            if (_r3_1 != null && _r3_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i3, _index, _memo.InputEnumerable, _r3_1.Results.Concat(_r3_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i3;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label2; }

            // CALLORVAR Literal
            _Parser_Item _r18;

            _r18 = _MemoCall(_memo, "Literal", _index, Literal, null);

            if (_r18 != null) _index = _r18.NextIndex;

            // BIND to
            to = _memo.Results.Peek();

        label2: // AND
            var _r2_2 = _memo.Results.Pop();
            var _r2_1 = _memo.Results.Pop();

            if (_r2_1 != null && _r2_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i2, _index, _memo.InputEnumerable, _r2_1.Results.Concat(_r2_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i2;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label1; }

            // CALLORVAR SP
            _Parser_Item _r19;

            _r19 = _MemoCall(_memo, "SP", _index, SP, null);

            if (_r19 != null) _index = _r19.NextIndex;

        label1: // AND
            var _r1_2 = _memo.Results.Pop();
            var _r1_1 = _memo.Results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i1, _index, _memo.InputEnumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i1;
            }

            // ACT
            var _r0 = _memo.Results.Peek();
            if (_r0 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _Parser_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { char ch_from = AST.ClassRange.GetChar(from);
            char ch_to = AST.ClassRange.GetChar(to);

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

            return new AST.ClassRange(_IM_Result, range); }, _r0), true) );
            }

        }


        public void CSharpCode(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            _Parser_Item code = null;

            // AND 1
            int _start_i1 = _index;

            // CALLORVAR CSharpCodeItem
            _Parser_Item _r3;

            _r3 = _MemoCall(_memo, "CSharpCodeItem", _index, CSharpCodeItem, null);

            if (_r3 != null) _index = _r3.NextIndex;

            // BIND code
            code = _memo.Results.Peek();

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label1; }

            // CALLORVAR SP
            _Parser_Item _r4;

            _r4 = _MemoCall(_memo, "SP", _index, SP, null);

            if (_r4 != null) _index = _r4.NextIndex;

        label1: // AND
            var _r1_2 = _memo.Results.Pop();
            var _r1_1 = _memo.Results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i1, _index, _memo.InputEnumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i1;
            }

            // ACT
            var _r0 = _memo.Results.Peek();
            if (_r0 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _Parser_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return new AST.Code(code); }, _r0), true) );
            }

        }


        public void CSharpCodeItem(_Parser_Memo _memo, int _index, _Parser_Args _args)
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
            _ParseLiteralChar(_memo, ref _index, '{');

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label4; }

            // STAR 6
            int _start_i6 = _index;
            var _res6 = Enumerable.Empty<AST.Node>();
        label6:

            // AND 7
            int _start_i7 = _index;

            // NOT 8
            int _start_i8 = _index;

            // LITERAL '}'
            _ParseLiteralChar(_memo, ref _index, '}');

            // NOT 8
            var _r8 = _memo.Results.Pop();
            _memo.Results.Push( _r8 == null ? new _Parser_Item(_start_i8, _memo.InputEnumerable) : null);
            _index = _start_i8;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label7; }

            // OR 10
            int _start_i10 = _index;

            // OR 11
            int _start_i11 = _index;

            // OR 12
            int _start_i12 = _index;

            // CALLORVAR EOL
            _Parser_Item _r13;

            _r13 = _MemoCall(_memo, "EOL", _index, EOL, null);

            if (_r13 != null) _index = _r13.NextIndex;

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i12; } else goto label12;

            // CALLORVAR Comment
            _Parser_Item _r14;

            _r14 = _MemoCall(_memo, "Comment", _index, Comment, null);

            if (_r14 != null) _index = _r14.NextIndex;

        label12: // OR
            int _dummy_i12 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i11; } else goto label11;

            // CALLORVAR CSharpCodeItem
            _Parser_Item _r15;

            _r15 = _MemoCall(_memo, "CSharpCodeItem", _index, CSharpCodeItem, null);

            if (_r15 != null) _index = _r15.NextIndex;

        label11: // OR
            int _dummy_i11 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i10; } else goto label10;

            // ANY
            _ParseAny(_memo, ref _index);

        label10: // OR
            int _dummy_i10 = _index; // no-op for label

        label7: // AND
            var _r7_2 = _memo.Results.Pop();
            var _r7_1 = _memo.Results.Pop();

            if (_r7_1 != null && _r7_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i7, _index, _memo.InputEnumerable, _r7_1.Results.Concat(_r7_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i7;
            }

            // STAR 6
            var _r6 = _memo.Results.Pop();
            if (_r6 != null)
            {
                _res6 = _res6.Concat(_r6.Results);
                goto label6;
            }
            else
            {
                _memo.Results.Push(new _Parser_Item(_start_i6, _index, _memo.InputEnumerable, _res6.Where(_NON_NULL), true));
            }

        label4: // AND
            var _r4_2 = _memo.Results.Pop();
            var _r4_1 = _memo.Results.Pop();

            if (_r4_1 != null && _r4_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i4, _index, _memo.InputEnumerable, _r4_1.Results.Concat(_r4_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i4;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label3; }

            // LITERAL '}'
            _ParseLiteralChar(_memo, ref _index, '}');

        label3: // AND
            var _r3_2 = _memo.Results.Pop();
            var _r3_1 = _memo.Results.Pop();

            if (_r3_1 != null && _r3_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i3, _index, _memo.InputEnumerable, _r3_1.Results.Concat(_r3_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i3;
            }

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i2; } else goto label2;

            // AND 18
            int _start_i18 = _index;

            // AND 19
            int _start_i19 = _index;

            // LITERAL '('
            _ParseLiteralChar(_memo, ref _index, '(');

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label19; }

            // STAR 21
            int _start_i21 = _index;
            var _res21 = Enumerable.Empty<AST.Node>();
        label21:

            // AND 22
            int _start_i22 = _index;

            // NOT 23
            int _start_i23 = _index;

            // LITERAL ')'
            _ParseLiteralChar(_memo, ref _index, ')');

            // NOT 23
            var _r23 = _memo.Results.Pop();
            _memo.Results.Push( _r23 == null ? new _Parser_Item(_start_i23, _memo.InputEnumerable) : null);
            _index = _start_i23;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label22; }

            // OR 25
            int _start_i25 = _index;

            // OR 26
            int _start_i26 = _index;

            // OR 27
            int _start_i27 = _index;

            // CALLORVAR EOL
            _Parser_Item _r28;

            _r28 = _MemoCall(_memo, "EOL", _index, EOL, null);

            if (_r28 != null) _index = _r28.NextIndex;

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i27; } else goto label27;

            // CALLORVAR Comment
            _Parser_Item _r29;

            _r29 = _MemoCall(_memo, "Comment", _index, Comment, null);

            if (_r29 != null) _index = _r29.NextIndex;

        label27: // OR
            int _dummy_i27 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i26; } else goto label26;

            // CALLORVAR CSharpCodeItem
            _Parser_Item _r30;

            _r30 = _MemoCall(_memo, "CSharpCodeItem", _index, CSharpCodeItem, null);

            if (_r30 != null) _index = _r30.NextIndex;

        label26: // OR
            int _dummy_i26 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i25; } else goto label25;

            // ANY
            _ParseAny(_memo, ref _index);

        label25: // OR
            int _dummy_i25 = _index; // no-op for label

        label22: // AND
            var _r22_2 = _memo.Results.Pop();
            var _r22_1 = _memo.Results.Pop();

            if (_r22_1 != null && _r22_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i22, _index, _memo.InputEnumerable, _r22_1.Results.Concat(_r22_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i22;
            }

            // STAR 21
            var _r21 = _memo.Results.Pop();
            if (_r21 != null)
            {
                _res21 = _res21.Concat(_r21.Results);
                goto label21;
            }
            else
            {
                _memo.Results.Push(new _Parser_Item(_start_i21, _index, _memo.InputEnumerable, _res21.Where(_NON_NULL), true));
            }

        label19: // AND
            var _r19_2 = _memo.Results.Pop();
            var _r19_1 = _memo.Results.Pop();

            if (_r19_1 != null && _r19_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i19, _index, _memo.InputEnumerable, _r19_1.Results.Concat(_r19_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i19;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label18; }

            // LITERAL ')'
            _ParseLiteralChar(_memo, ref _index, ')');

        label18: // AND
            var _r18_2 = _memo.Results.Pop();
            var _r18_1 = _memo.Results.Pop();

            if (_r18_1 != null && _r18_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i18, _index, _memo.InputEnumerable, _r18_1.Results.Concat(_r18_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i18;
            }

        label2: // OR
            int _dummy_i2 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i1; } else goto label1;

            // AND 33
            int _start_i33 = _index;

            // AND 34
            int _start_i34 = _index;

            // LITERAL '\x27'
            _ParseLiteralChar(_memo, ref _index, '\x27');

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label34; }

            // STAR 36
            int _start_i36 = _index;
            var _res36 = Enumerable.Empty<AST.Node>();
        label36:

            // OR 37
            int _start_i37 = _index;

            // OR 38
            int _start_i38 = _index;

            // OR 39
            int _start_i39 = _index;

            // CALLORVAR EOL
            _Parser_Item _r40;

            _r40 = _MemoCall(_memo, "EOL", _index, EOL, null);

            if (_r40 != null) _index = _r40.NextIndex;

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i39; } else goto label39;

            // LITERAL "\x5c\x27"
            _ParseLiteralString(_memo, ref _index, "\x5c\x27");

        label39: // OR
            int _dummy_i39 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i38; } else goto label38;

            // LITERAL "\x5c\x5c"
            _ParseLiteralString(_memo, ref _index, "\x5c\x5c");

        label38: // OR
            int _dummy_i38 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i37; } else goto label37;

            // AND 43
            int _start_i43 = _index;

            // NOT 44
            int _start_i44 = _index;

            // LITERAL '\x27'
            _ParseLiteralChar(_memo, ref _index, '\x27');

            // NOT 44
            var _r44 = _memo.Results.Pop();
            _memo.Results.Push( _r44 == null ? new _Parser_Item(_start_i44, _memo.InputEnumerable) : null);
            _index = _start_i44;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label43; }

            // ANY
            _ParseAny(_memo, ref _index);

        label43: // AND
            var _r43_2 = _memo.Results.Pop();
            var _r43_1 = _memo.Results.Pop();

            if (_r43_1 != null && _r43_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i43, _index, _memo.InputEnumerable, _r43_1.Results.Concat(_r43_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i43;
            }

        label37: // OR
            int _dummy_i37 = _index; // no-op for label

            // STAR 36
            var _r36 = _memo.Results.Pop();
            if (_r36 != null)
            {
                _res36 = _res36.Concat(_r36.Results);
                goto label36;
            }
            else
            {
                _memo.Results.Push(new _Parser_Item(_start_i36, _index, _memo.InputEnumerable, _res36.Where(_NON_NULL), true));
            }

        label34: // AND
            var _r34_2 = _memo.Results.Pop();
            var _r34_1 = _memo.Results.Pop();

            if (_r34_1 != null && _r34_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i34, _index, _memo.InputEnumerable, _r34_1.Results.Concat(_r34_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i34;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label33; }

            // LITERAL '\x27'
            _ParseLiteralChar(_memo, ref _index, '\x27');

        label33: // AND
            var _r33_2 = _memo.Results.Pop();
            var _r33_1 = _memo.Results.Pop();

            if (_r33_1 != null && _r33_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i33, _index, _memo.InputEnumerable, _r33_1.Results.Concat(_r33_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i33;
            }

        label1: // OR
            int _dummy_i1 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i0; } else goto label0;

            // AND 48
            int _start_i48 = _index;

            // AND 49
            int _start_i49 = _index;

            // LITERAL '\x22'
            _ParseLiteralChar(_memo, ref _index, '\x22');

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label49; }

            // STAR 51
            int _start_i51 = _index;
            var _res51 = Enumerable.Empty<AST.Node>();
        label51:

            // OR 52
            int _start_i52 = _index;

            // OR 53
            int _start_i53 = _index;

            // OR 54
            int _start_i54 = _index;

            // CALLORVAR EOL
            _Parser_Item _r55;

            _r55 = _MemoCall(_memo, "EOL", _index, EOL, null);

            if (_r55 != null) _index = _r55.NextIndex;

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i54; } else goto label54;

            // LITERAL "\x5c\x22"
            _ParseLiteralString(_memo, ref _index, "\x5c\x22");

        label54: // OR
            int _dummy_i54 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i53; } else goto label53;

            // LITERAL "\x5c\x5c"
            _ParseLiteralString(_memo, ref _index, "\x5c\x5c");

        label53: // OR
            int _dummy_i53 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i52; } else goto label52;

            // AND 58
            int _start_i58 = _index;

            // NOT 59
            int _start_i59 = _index;

            // LITERAL '\x22'
            _ParseLiteralChar(_memo, ref _index, '\x22');

            // NOT 59
            var _r59 = _memo.Results.Pop();
            _memo.Results.Push( _r59 == null ? new _Parser_Item(_start_i59, _memo.InputEnumerable) : null);
            _index = _start_i59;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label58; }

            // ANY
            _ParseAny(_memo, ref _index);

        label58: // AND
            var _r58_2 = _memo.Results.Pop();
            var _r58_1 = _memo.Results.Pop();

            if (_r58_1 != null && _r58_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i58, _index, _memo.InputEnumerable, _r58_1.Results.Concat(_r58_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i58;
            }

        label52: // OR
            int _dummy_i52 = _index; // no-op for label

            // STAR 51
            var _r51 = _memo.Results.Pop();
            if (_r51 != null)
            {
                _res51 = _res51.Concat(_r51.Results);
                goto label51;
            }
            else
            {
                _memo.Results.Push(new _Parser_Item(_start_i51, _index, _memo.InputEnumerable, _res51.Where(_NON_NULL), true));
            }

        label49: // AND
            var _r49_2 = _memo.Results.Pop();
            var _r49_1 = _memo.Results.Pop();

            if (_r49_1 != null && _r49_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i49, _index, _memo.InputEnumerable, _r49_1.Results.Concat(_r49_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i49;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label48; }

            // LITERAL '\x22'
            _ParseLiteralChar(_memo, ref _index, '\x22');

        label48: // AND
            var _r48_2 = _memo.Results.Pop();
            var _r48_1 = _memo.Results.Pop();

            if (_r48_1 != null && _r48_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i48, _index, _memo.InputEnumerable, _r48_1.Results.Concat(_r48_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i48;
            }

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void Identifier(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            _Parser_Item id = null;

            // AND 1
            int _start_i1 = _index;

            // CALLORVAR Ident
            _Parser_Item _r3;

            _r3 = _MemoCall(_memo, "Ident", _index, Ident, null);

            if (_r3 != null) _index = _r3.NextIndex;

            // BIND id
            id = _memo.Results.Peek();

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label1; }

            // CALLORVAR SP
            _Parser_Item _r4;

            _r4 = _MemoCall(_memo, "SP", _index, SP, null);

            if (_r4 != null) _index = _r4.NextIndex;

        label1: // AND
            var _r1_2 = _memo.Results.Pop();
            var _r1_1 = _memo.Results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i1, _index, _memo.InputEnumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i1;
            }

            // ACT
            var _r0 = _memo.Results.Peek();
            if (_r0 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _Parser_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return id; }, _r0), true) );
            }

        }


        public void Ident(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            _Parser_Item id = null;

            // AND 2
            int _start_i2 = _index;

            // CALLORVAR IdentBegin
            _Parser_Item _r3;

            _r3 = _MemoCall(_memo, "IdentBegin", _index, IdentBegin, null);

            if (_r3 != null) _index = _r3.NextIndex;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label2; }

            // STAR 4
            int _start_i4 = _index;
            var _res4 = Enumerable.Empty<AST.Node>();
        label4:

            // CALLORVAR IdentBody
            _Parser_Item _r5;

            _r5 = _MemoCall(_memo, "IdentBody", _index, IdentBody, null);

            if (_r5 != null) _index = _r5.NextIndex;

            // STAR 4
            var _r4 = _memo.Results.Pop();
            if (_r4 != null)
            {
                _res4 = _res4.Concat(_r4.Results);
                goto label4;
            }
            else
            {
                _memo.Results.Push(new _Parser_Item(_start_i4, _index, _memo.InputEnumerable, _res4.Where(_NON_NULL), true));
            }

        label2: // AND
            var _r2_2 = _memo.Results.Pop();
            var _r2_1 = _memo.Results.Pop();

            if (_r2_1 != null && _r2_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i2, _index, _memo.InputEnumerable, _r2_1.Results.Concat(_r2_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i2;
            }

            // BIND id
            id = _memo.Results.Peek();

            // ACT
            var _r0 = _memo.Results.Peek();
            if (_r0 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _Parser_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return new AST.Idfr(id); }, _r0), true) );
            }

        }


        public void IdentBegin(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            // OR 0
            int _start_i0 = _index;

            // LITERAL '_'
            _ParseLiteralChar(_memo, ref _index, '_');

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i0; } else goto label0;

            // COND 2
            int _start_i2 = _index;

            // ANY
            _ParseAny(_memo, ref _index);

            // COND
            Func<_Parser_Item, bool> lambda2 = (_IM_Result) => { return System.Char.IsLetter(_IM_Result); };
            if (_memo.Results.Peek() == null || !lambda2(_memo.Results.Peek())) { _memo.Results.Pop(); _memo.Results.Push(null); _index = _start_i2; }

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void IdentBody(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            // OR 0
            int _start_i0 = _index;

            // LITERAL '_'
            _ParseLiteralChar(_memo, ref _index, '_');

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i0; } else goto label0;

            // COND 2
            int _start_i2 = _index;

            // ANY
            _ParseAny(_memo, ref _index);

            // COND
            Func<_Parser_Item, bool> lambda2 = (_IM_Result) => { return System.Char.IsLetterOrDigit(_IM_Result); };
            if (_memo.Results.Peek() == null || !lambda2(_memo.Results.Peek())) { _memo.Results.Pop(); _memo.Results.Push(null); _index = _start_i2; }

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void QualifiedId(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            // AND 1
            int _start_i1 = _index;

            // AND 2
            int _start_i2 = _index;

            // CALLORVAR Ident
            _Parser_Item _r3;

            _r3 = _MemoCall(_memo, "Ident", _index, Ident, null);

            if (_r3 != null) _index = _r3.NextIndex;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label2; }

            // STAR 4
            int _start_i4 = _index;
            var _res4 = Enumerable.Empty<AST.Node>();
        label4:

            // AND 5
            int _start_i5 = _index;

            // CALLORVAR DOT
            _Parser_Item _r6;

            _r6 = _MemoCall(_memo, "DOT", _index, DOT, null);

            if (_r6 != null) _index = _r6.NextIndex;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label5; }

            // CALLORVAR Ident
            _Parser_Item _r7;

            _r7 = _MemoCall(_memo, "Ident", _index, Ident, null);

            if (_r7 != null) _index = _r7.NextIndex;

        label5: // AND
            var _r5_2 = _memo.Results.Pop();
            var _r5_1 = _memo.Results.Pop();

            if (_r5_1 != null && _r5_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i5, _index, _memo.InputEnumerable, _r5_1.Results.Concat(_r5_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i5;
            }

            // STAR 4
            var _r4 = _memo.Results.Pop();
            if (_r4 != null)
            {
                _res4 = _res4.Concat(_r4.Results);
                goto label4;
            }
            else
            {
                _memo.Results.Push(new _Parser_Item(_start_i4, _index, _memo.InputEnumerable, _res4.Where(_NON_NULL), true));
            }

        label2: // AND
            var _r2_2 = _memo.Results.Pop();
            var _r2_1 = _memo.Results.Pop();

            if (_r2_1 != null && _r2_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i2, _index, _memo.InputEnumerable, _r2_1.Results.Concat(_r2_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i2;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label1; }

            // CALLORVAR SP
            _Parser_Item _r8;

            _r8 = _MemoCall(_memo, "SP", _index, SP, null);

            if (_r8 != null) _index = _r8.NextIndex;

        label1: // AND
            var _r1_2 = _memo.Results.Pop();
            var _r1_1 = _memo.Results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i1, _index, _memo.InputEnumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i1;
            }

            // ACT
            var _r0 = _memo.Results.Peek();
            if (_r0 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _Parser_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return new AST.Idfr(_IM_Result); }, _r0), true) );
            }

        }


        public void GenericId(_Parser_Memo _memo, int _index, _Parser_Args _args)
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

            _r5 = _MemoCall(_memo, "Ident", _index, Ident, null);

            if (_r5 != null) _index = _r5.NextIndex;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label4; }

            // STAR 6
            int _start_i6 = _index;
            var _res6 = Enumerable.Empty<AST.Node>();
        label6:

            // AND 7
            int _start_i7 = _index;

            // CALLORVAR DOT
            _Parser_Item _r8;

            _r8 = _MemoCall(_memo, "DOT", _index, DOT, null);

            if (_r8 != null) _index = _r8.NextIndex;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label7; }

            // CALLORVAR Ident
            _Parser_Item _r9;

            _r9 = _MemoCall(_memo, "Ident", _index, Ident, null);

            if (_r9 != null) _index = _r9.NextIndex;

        label7: // AND
            var _r7_2 = _memo.Results.Pop();
            var _r7_1 = _memo.Results.Pop();

            if (_r7_1 != null && _r7_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i7, _index, _memo.InputEnumerable, _r7_1.Results.Concat(_r7_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i7;
            }

            // STAR 6
            var _r6 = _memo.Results.Pop();
            if (_r6 != null)
            {
                _res6 = _res6.Concat(_r6.Results);
                goto label6;
            }
            else
            {
                _memo.Results.Push(new _Parser_Item(_start_i6, _index, _memo.InputEnumerable, _res6.Where(_NON_NULL), true));
            }

        label4: // AND
            var _r4_2 = _memo.Results.Pop();
            var _r4_1 = _memo.Results.Pop();

            if (_r4_1 != null && _r4_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i4, _index, _memo.InputEnumerable, _r4_1.Results.Concat(_r4_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i4;
            }

            // BIND ids
            ids = _memo.Results.Peek();

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label2; }

            // CALLORVAR SP
            _Parser_Item _r10;

            _r10 = _MemoCall(_memo, "SP", _index, SP, null);

            if (_r10 != null) _index = _r10.NextIndex;

        label2: // AND
            var _r2_2 = _memo.Results.Pop();
            var _r2_1 = _memo.Results.Pop();

            if (_r2_1 != null && _r2_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i2, _index, _memo.InputEnumerable, _r2_1.Results.Concat(_r2_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i2;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label1; }

            // AND 13
            int _start_i13 = _index;

            // AND 14
            int _start_i14 = _index;

            // CALLORVAR LESS
            _Parser_Item _r15;

            _r15 = _MemoCall(_memo, "LESS", _index, LESS, null);

            if (_r15 != null) _index = _r15.NextIndex;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label14; }

            // AND 16
            int _start_i16 = _index;

            // CALLORVAR GenericId
            _Parser_Item _r17;

            _r17 = _MemoCall(_memo, "GenericId", _index, GenericId, null);

            if (_r17 != null) _index = _r17.NextIndex;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label16; }

            // STAR 18
            int _start_i18 = _index;
            var _res18 = Enumerable.Empty<AST.Node>();
        label18:

            // AND 19
            int _start_i19 = _index;

            // CALLORVAR COMMA
            _Parser_Item _r20;

            _r20 = _MemoCall(_memo, "COMMA", _index, COMMA, null);

            if (_r20 != null) _index = _r20.NextIndex;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label19; }

            // CALLORVAR GenericId
            _Parser_Item _r21;

            _r21 = _MemoCall(_memo, "GenericId", _index, GenericId, null);

            if (_r21 != null) _index = _r21.NextIndex;

        label19: // AND
            var _r19_2 = _memo.Results.Pop();
            var _r19_1 = _memo.Results.Pop();

            if (_r19_1 != null && _r19_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i19, _index, _memo.InputEnumerable, _r19_1.Results.Concat(_r19_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i19;
            }

            // STAR 18
            var _r18 = _memo.Results.Pop();
            if (_r18 != null)
            {
                _res18 = _res18.Concat(_r18.Results);
                goto label18;
            }
            else
            {
                _memo.Results.Push(new _Parser_Item(_start_i18, _index, _memo.InputEnumerable, _res18.Where(_NON_NULL), true));
            }

        label16: // AND
            var _r16_2 = _memo.Results.Pop();
            var _r16_1 = _memo.Results.Pop();

            if (_r16_1 != null && _r16_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i16, _index, _memo.InputEnumerable, _r16_1.Results.Concat(_r16_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i16;
            }

        label14: // AND
            var _r14_2 = _memo.Results.Pop();
            var _r14_1 = _memo.Results.Pop();

            if (_r14_1 != null && _r14_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i14, _index, _memo.InputEnumerable, _r14_1.Results.Concat(_r14_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i14;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label13; }

            // CALLORVAR GREATER
            _Parser_Item _r22;

            _r22 = _MemoCall(_memo, "GREATER", _index, GREATER, null);

            if (_r22 != null) _index = _r22.NextIndex;

        label13: // AND
            var _r13_2 = _memo.Results.Pop();
            var _r13_1 = _memo.Results.Pop();

            if (_r13_1 != null && _r13_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i13, _index, _memo.InputEnumerable, _r13_1.Results.Concat(_r13_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i13;
            }

            // QUES
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _memo.Results.Push(new _Parser_Item(_index, _memo.InputEnumerable)); }

            // BIND gp
            gp = _memo.Results.Peek();

        label1: // AND
            var _r1_2 = _memo.Results.Pop();
            var _r1_1 = _memo.Results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i1, _index, _memo.InputEnumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i1;
            }

            // ACT
            var _r0 = _memo.Results.Peek();
            if (_r0 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _Parser_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { return new AST.Idfr(ids, gp); }, _r0), true) );
            }

        }


        public void Number(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // PLUS 1
            int _start_i1 = _index;
            var _res1 = Enumerable.Empty<AST.Node>();
        label1:

            // INPUT CLASS
            _ParseInputClass(_memo, ref _index, '\u0030', '\u0031', '\u0032', '\u0033', '\u0034', '\u0035', '\u0036', '\u0037', '\u0038', '\u0039');

            // PLUS 1
            var _r1 = _memo.Results.Pop();
            if (_r1 != null)
            {
                _res1 = _res1.Concat(_r1.Results);
                goto label1;
            }
            else
            {
                if (_index > _start_i1)
                    _memo.Results.Push(new _Parser_Item(_start_i1, _index, _memo.InputEnumerable, _res1.Where(_NON_NULL), true));
                else
                    _memo.Results.Push(null);
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label0; }

            // CALLORVAR SP
            _Parser_Item _r3;

            _r3 = _MemoCall(_memo, "SP", _index, SP, null);

            if (_r3 != null) _index = _r3.NextIndex;

        label0: // AND
            var _r0_2 = _memo.Results.Pop();
            var _r0_1 = _memo.Results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i0, _index, _memo.InputEnumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i0;
            }

        }


        public void KEYWORD(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            // OR 0
            int _start_i0 = _index;

            // OR 1
            int _start_i1 = _index;

            // OR 2
            int _start_i2 = _index;

            // OR 3
            int _start_i3 = _index;

            // CALLORVAR USING
            _Parser_Item _r4;

            _r4 = _MemoCall(_memo, "USING", _index, USING, null);

            if (_r4 != null) _index = _r4.NextIndex;

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i3; } else goto label3;

            // CALLORVAR IRONMETA
            _Parser_Item _r5;

            _r5 = _MemoCall(_memo, "IRONMETA", _index, IRONMETA, null);

            if (_r5 != null) _index = _r5.NextIndex;

        label3: // OR
            int _dummy_i3 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i2; } else goto label2;

            // CALLORVAR OVERRIDE
            _Parser_Item _r6;

            _r6 = _MemoCall(_memo, "OVERRIDE", _index, OVERRIDE, null);

            if (_r6 != null) _index = _r6.NextIndex;

        label2: // OR
            int _dummy_i2 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i1; } else goto label1;

            // CALLORVAR NEW
            _Parser_Item _r7;

            _r7 = _MemoCall(_memo, "NEW", _index, NEW, null);

            if (_r7 != null) _index = _r7.NextIndex;

        label1: // OR
            int _dummy_i1 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i0; } else goto label0;

            // CALLORVAR LR
            _Parser_Item _r8;

            _r8 = _MemoCall(_memo, "LR", _index, LR, null);

            if (_r8 != null) _index = _r8.NextIndex;

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void USING(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // LITERAL "using"
            _ParseLiteralString(_memo, ref _index, "using");

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label0; }

            // CALLORVAR SP
            _Parser_Item _r2;

            _r2 = _MemoCall(_memo, "SP", _index, SP, null);

            if (_r2 != null) _index = _r2.NextIndex;

        label0: // AND
            var _r0_2 = _memo.Results.Pop();
            var _r0_1 = _memo.Results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i0, _index, _memo.InputEnumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i0;
            }

        }


        public void IRONMETA(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // LITERAL "ironmeta"
            _ParseLiteralString(_memo, ref _index, "ironmeta");

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label0; }

            // CALLORVAR SP
            _Parser_Item _r2;

            _r2 = _MemoCall(_memo, "SP", _index, SP, null);

            if (_r2 != null) _index = _r2.NextIndex;

        label0: // AND
            var _r0_2 = _memo.Results.Pop();
            var _r0_1 = _memo.Results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i0, _index, _memo.InputEnumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i0;
            }

        }


        public void EQUALS(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // OR 1
            int _start_i1 = _index;

            // LITERAL '='
            _ParseLiteralChar(_memo, ref _index, '=');

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i1; } else goto label1;

            // LITERAL "::="
            _ParseLiteralString(_memo, ref _index, "::=");

        label1: // OR
            int _dummy_i1 = _index; // no-op for label

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label0; }

            // CALLORVAR SP
            _Parser_Item _r4;

            _r4 = _MemoCall(_memo, "SP", _index, SP, null);

            if (_r4 != null) _index = _r4.NextIndex;

        label0: // AND
            var _r0_2 = _memo.Results.Pop();
            var _r0_1 = _memo.Results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i0, _index, _memo.InputEnumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i0;
            }

        }


        public void OVERRIDE(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // OR 1
            int _start_i1 = _index;

            // OR 2
            int _start_i2 = _index;

            // LITERAL "override"
            _ParseLiteralString(_memo, ref _index, "override");

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i2; } else goto label2;

            // LITERAL "virtual"
            _ParseLiteralString(_memo, ref _index, "virtual");

        label2: // OR
            int _dummy_i2 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i1; } else goto label1;

            // LITERAL "new"
            _ParseLiteralString(_memo, ref _index, "new");

        label1: // OR
            int _dummy_i1 = _index; // no-op for label

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label0; }

            // CALLORVAR SP
            _Parser_Item _r6;

            _r6 = _MemoCall(_memo, "SP", _index, SP, null);

            if (_r6 != null) _index = _r6.NextIndex;

        label0: // AND
            var _r0_2 = _memo.Results.Pop();
            var _r0_1 = _memo.Results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i0, _index, _memo.InputEnumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i0;
            }

        }


        public void NEW(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // LITERAL "new"
            _ParseLiteralString(_memo, ref _index, "new");

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label0; }

            // CALLORVAR SP
            _Parser_Item _r2;

            _r2 = _MemoCall(_memo, "SP", _index, SP, null);

            if (_r2 != null) _index = _r2.NextIndex;

        label0: // AND
            var _r0_2 = _memo.Results.Pop();
            var _r0_1 = _memo.Results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i0, _index, _memo.InputEnumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i0;
            }

        }


        public void LR(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // LITERAL "lr"
            _ParseLiteralString(_memo, ref _index, "lr");

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label0; }

            // CALLORVAR SP
            _Parser_Item _r2;

            _r2 = _MemoCall(_memo, "SP", _index, SP, null);

            if (_r2 != null) _index = _r2.NextIndex;

        label0: // AND
            var _r0_2 = _memo.Results.Pop();
            var _r0_1 = _memo.Results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i0, _index, _memo.InputEnumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i0;
            }

        }


        public void SEMI(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // OR 1
            int _start_i1 = _index;

            // LITERAL ';'
            _ParseLiteralChar(_memo, ref _index, ';');

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i1; } else goto label1;

            // LITERAL ','
            _ParseLiteralChar(_memo, ref _index, ',');

        label1: // OR
            int _dummy_i1 = _index; // no-op for label

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label0; }

            // CALLORVAR SP
            _Parser_Item _r4;

            _r4 = _MemoCall(_memo, "SP", _index, SP, null);

            if (_r4 != null) _index = _r4.NextIndex;

        label0: // AND
            var _r0_2 = _memo.Results.Pop();
            var _r0_1 = _memo.Results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i0, _index, _memo.InputEnumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i0;
            }

        }


        public void BANG(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // LITERAL '!'
            _ParseLiteralChar(_memo, ref _index, '!');

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label0; }

            // CALLORVAR SP
            _Parser_Item _r2;

            _r2 = _MemoCall(_memo, "SP", _index, SP, null);

            if (_r2 != null) _index = _r2.NextIndex;

        label0: // AND
            var _r0_2 = _memo.Results.Pop();
            var _r0_1 = _memo.Results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i0, _index, _memo.InputEnumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i0;
            }

        }


        public void OR(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // LITERAL '|'
            _ParseLiteralChar(_memo, ref _index, '|');

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label0; }

            // CALLORVAR SP
            _Parser_Item _r2;

            _r2 = _MemoCall(_memo, "SP", _index, SP, null);

            if (_r2 != null) _index = _r2.NextIndex;

        label0: // AND
            var _r0_2 = _memo.Results.Pop();
            var _r0_1 = _memo.Results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i0, _index, _memo.InputEnumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i0;
            }

        }


        public void ACTION(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // LITERAL "->"
            _ParseLiteralString(_memo, ref _index, "->");

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label0; }

            // CALLORVAR SP
            _Parser_Item _r2;

            _r2 = _MemoCall(_memo, "SP", _index, SP, null);

            if (_r2 != null) _index = _r2.NextIndex;

        label0: // AND
            var _r0_2 = _memo.Results.Pop();
            var _r0_1 = _memo.Results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i0, _index, _memo.InputEnumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i0;
            }

        }


        public void COLON(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // LITERAL ':'
            _ParseLiteralChar(_memo, ref _index, ':');

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label0; }

            // CALLORVAR SP
            _Parser_Item _r2;

            _r2 = _MemoCall(_memo, "SP", _index, SP, null);

            if (_r2 != null) _index = _r2.NextIndex;

        label0: // AND
            var _r0_2 = _memo.Results.Pop();
            var _r0_1 = _memo.Results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i0, _index, _memo.InputEnumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i0;
            }

        }


        public void COMMA(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // LITERAL ','
            _ParseLiteralChar(_memo, ref _index, ',');

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label0; }

            // CALLORVAR SP
            _Parser_Item _r2;

            _r2 = _MemoCall(_memo, "SP", _index, SP, null);

            if (_r2 != null) _index = _r2.NextIndex;

        label0: // AND
            var _r0_2 = _memo.Results.Pop();
            var _r0_1 = _memo.Results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i0, _index, _memo.InputEnumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i0;
            }

        }


        public void DOT(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            // LITERAL '.'
            _ParseLiteralChar(_memo, ref _index, '.');

        }


        public void PERIOD(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // CALLORVAR DOT
            _Parser_Item _r1;

            _r1 = _MemoCall(_memo, "DOT", _index, DOT, null);

            if (_r1 != null) _index = _r1.NextIndex;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label0; }

            // CALLORVAR SP
            _Parser_Item _r2;

            _r2 = _MemoCall(_memo, "SP", _index, SP, null);

            if (_r2 != null) _index = _r2.NextIndex;

        label0: // AND
            var _r0_2 = _memo.Results.Pop();
            var _r0_1 = _memo.Results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i0, _index, _memo.InputEnumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i0;
            }

        }


        public void BRA(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // LITERAL '{'
            _ParseLiteralChar(_memo, ref _index, '{');

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label0; }

            // CALLORVAR SP
            _Parser_Item _r2;

            _r2 = _MemoCall(_memo, "SP", _index, SP, null);

            if (_r2 != null) _index = _r2.NextIndex;

        label0: // AND
            var _r0_2 = _memo.Results.Pop();
            var _r0_1 = _memo.Results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i0, _index, _memo.InputEnumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i0;
            }

        }


        public void KET(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // LITERAL '}'
            _ParseLiteralChar(_memo, ref _index, '}');

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label0; }

            // CALLORVAR SP
            _Parser_Item _r2;

            _r2 = _MemoCall(_memo, "SP", _index, SP, null);

            if (_r2 != null) _index = _r2.NextIndex;

        label0: // AND
            var _r0_2 = _memo.Results.Pop();
            var _r0_1 = _memo.Results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i0, _index, _memo.InputEnumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i0;
            }

        }


        public void OPEN(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // LITERAL '('
            _ParseLiteralChar(_memo, ref _index, '(');

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label0; }

            // CALLORVAR SP
            _Parser_Item _r2;

            _r2 = _MemoCall(_memo, "SP", _index, SP, null);

            if (_r2 != null) _index = _r2.NextIndex;

        label0: // AND
            var _r0_2 = _memo.Results.Pop();
            var _r0_1 = _memo.Results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i0, _index, _memo.InputEnumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i0;
            }

        }


        public void CLOSE(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // LITERAL ')'
            _ParseLiteralChar(_memo, ref _index, ')');

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label0; }

            // CALLORVAR SP
            _Parser_Item _r2;

            _r2 = _MemoCall(_memo, "SP", _index, SP, null);

            if (_r2 != null) _index = _r2.NextIndex;

        label0: // AND
            var _r0_2 = _memo.Results.Pop();
            var _r0_1 = _memo.Results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i0, _index, _memo.InputEnumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i0;
            }

        }


        public void LESS(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // LITERAL '<'
            _ParseLiteralChar(_memo, ref _index, '<');

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label0; }

            // CALLORVAR SP
            _Parser_Item _r2;

            _r2 = _MemoCall(_memo, "SP", _index, SP, null);

            if (_r2 != null) _index = _r2.NextIndex;

        label0: // AND
            var _r0_2 = _memo.Results.Pop();
            var _r0_1 = _memo.Results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i0, _index, _memo.InputEnumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i0;
            }

        }


        public void GREATER(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // LITERAL '>'
            _ParseLiteralChar(_memo, ref _index, '>');

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label0; }

            // CALLORVAR SP
            _Parser_Item _r2;

            _r2 = _MemoCall(_memo, "SP", _index, SP, null);

            if (_r2 != null) _index = _r2.NextIndex;

        label0: // AND
            var _r0_2 = _memo.Results.Pop();
            var _r0_1 = _memo.Results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i0, _index, _memo.InputEnumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i0;
            }

        }


        public void QUES(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            // LITERAL '?'
            _ParseLiteralChar(_memo, ref _index, '?');

        }


        public void AND_PRE(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            // LITERAL '&'
            _ParseLiteralChar(_memo, ref _index, '&');

        }


        public void NOT_PRE(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            // LITERAL '~'
            _ParseLiteralChar(_memo, ref _index, '~');

        }


        public void STAR(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // LITERAL '*'
            _ParseLiteralChar(_memo, ref _index, '*');

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label0; }

            // CALLORVAR SP
            _Parser_Item _r2;

            _r2 = _MemoCall(_memo, "SP", _index, SP, null);

            if (_r2 != null) _index = _r2.NextIndex;

        label0: // AND
            var _r0_2 = _memo.Results.Pop();
            var _r0_1 = _memo.Results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i0, _index, _memo.InputEnumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i0;
            }

        }


        public void PLUS(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            // AND 0
            int _start_i0 = _index;

            // LITERAL '+'
            _ParseLiteralChar(_memo, ref _index, '+');

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label0; }

            // CALLORVAR SP
            _Parser_Item _r2;

            _r2 = _MemoCall(_memo, "SP", _index, SP, null);

            if (_r2 != null) _index = _r2.NextIndex;

        label0: // AND
            var _r0_2 = _memo.Results.Pop();
            var _r0_1 = _memo.Results.Pop();

            if (_r0_1 != null && _r0_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i0, _index, _memo.InputEnumerable, _r0_1.Results.Concat(_r0_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i0;
            }

        }


        public void SP(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            // STAR 0
            int _start_i0 = _index;
            var _res0 = Enumerable.Empty<AST.Node>();
        label0:

            // OR 1
            int _start_i1 = _index;

            // OR 2
            int _start_i2 = _index;

            // CALLORVAR EOL
            _Parser_Item _r3;

            _r3 = _MemoCall(_memo, "EOL", _index, EOL, null);

            if (_r3 != null) _index = _r3.NextIndex;

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i2; } else goto label2;

            // CALLORVAR WS
            _Parser_Item _r4;

            _r4 = _MemoCall(_memo, "WS", _index, WS, null);

            if (_r4 != null) _index = _r4.NextIndex;

        label2: // OR
            int _dummy_i2 = _index; // no-op for label

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i1; } else goto label1;

            // CALLORVAR Comment
            _Parser_Item _r5;

            _r5 = _MemoCall(_memo, "Comment", _index, Comment, null);

            if (_r5 != null) _index = _r5.NextIndex;

        label1: // OR
            int _dummy_i1 = _index; // no-op for label

            // STAR 0
            var _r0 = _memo.Results.Pop();
            if (_r0 != null)
            {
                _res0 = _res0.Concat(_r0.Results);
                goto label0;
            }
            else
            {
                _memo.Results.Push(new _Parser_Item(_start_i0, _index, _memo.InputEnumerable, _res0.Where(_NON_NULL), true));
            }

        }


        public void WS(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            // INPUT CLASS
            _ParseInputClass(_memo, ref _index, ' ', '\t');

        }


        public void Comment(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            // OR 0
            int _start_i0 = _index;

            // AND 1
            int _start_i1 = _index;

            // AND 2
            int _start_i2 = _index;

            // LITERAL "//"
            _ParseLiteralString(_memo, ref _index, "//");

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label2; }

            // STAR 4
            int _start_i4 = _index;
            var _res4 = Enumerable.Empty<AST.Node>();
        label4:

            // AND 5
            int _start_i5 = _index;

            // NOT 6
            int _start_i6 = _index;

            // CALLORVAR EOL
            _Parser_Item _r7;

            _r7 = _MemoCall(_memo, "EOL", _index, EOL, null);

            if (_r7 != null) _index = _r7.NextIndex;

            // NOT 6
            var _r6 = _memo.Results.Pop();
            _memo.Results.Push( _r6 == null ? new _Parser_Item(_start_i6, _memo.InputEnumerable) : null);
            _index = _start_i6;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label5; }

            // ANY
            _ParseAny(_memo, ref _index);

        label5: // AND
            var _r5_2 = _memo.Results.Pop();
            var _r5_1 = _memo.Results.Pop();

            if (_r5_1 != null && _r5_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i5, _index, _memo.InputEnumerable, _r5_1.Results.Concat(_r5_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i5;
            }

            // STAR 4
            var _r4 = _memo.Results.Pop();
            if (_r4 != null)
            {
                _res4 = _res4.Concat(_r4.Results);
                goto label4;
            }
            else
            {
                _memo.Results.Push(new _Parser_Item(_start_i4, _index, _memo.InputEnumerable, _res4.Where(_NON_NULL), true));
            }

        label2: // AND
            var _r2_2 = _memo.Results.Pop();
            var _r2_1 = _memo.Results.Pop();

            if (_r2_1 != null && _r2_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i2, _index, _memo.InputEnumerable, _r2_1.Results.Concat(_r2_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i2;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label1; }

            // CALLORVAR EOL
            _Parser_Item _r9;

            _r9 = _MemoCall(_memo, "EOL", _index, EOL, null);

            if (_r9 != null) _index = _r9.NextIndex;

        label1: // AND
            var _r1_2 = _memo.Results.Pop();
            var _r1_1 = _memo.Results.Pop();

            if (_r1_1 != null && _r1_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i1, _index, _memo.InputEnumerable, _r1_1.Results.Concat(_r1_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i1;
            }

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i0; } else goto label0;

            // AND 10
            int _start_i10 = _index;

            // AND 11
            int _start_i11 = _index;

            // LITERAL "/*"
            _ParseLiteralString(_memo, ref _index, "/*");

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label11; }

            // STAR 13
            int _start_i13 = _index;
            var _res13 = Enumerable.Empty<AST.Node>();
        label13:

            // AND 14
            int _start_i14 = _index;

            // NOT 15
            int _start_i15 = _index;

            // LITERAL "*/"
            _ParseLiteralString(_memo, ref _index, "*/");

            // NOT 15
            var _r15 = _memo.Results.Pop();
            _memo.Results.Push( _r15 == null ? new _Parser_Item(_start_i15, _memo.InputEnumerable) : null);
            _index = _start_i15;

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label14; }

            // OR 17
            int _start_i17 = _index;

            // CALLORVAR EOL
            _Parser_Item _r18;

            _r18 = _MemoCall(_memo, "EOL", _index, EOL, null);

            if (_r18 != null) _index = _r18.NextIndex;

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i17; } else goto label17;

            // ANY
            _ParseAny(_memo, ref _index);

        label17: // OR
            int _dummy_i17 = _index; // no-op for label

        label14: // AND
            var _r14_2 = _memo.Results.Pop();
            var _r14_1 = _memo.Results.Pop();

            if (_r14_1 != null && _r14_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i14, _index, _memo.InputEnumerable, _r14_1.Results.Concat(_r14_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i14;
            }

            // STAR 13
            var _r13 = _memo.Results.Pop();
            if (_r13 != null)
            {
                _res13 = _res13.Concat(_r13.Results);
                goto label13;
            }
            else
            {
                _memo.Results.Push(new _Parser_Item(_start_i13, _index, _memo.InputEnumerable, _res13.Where(_NON_NULL), true));
            }

        label11: // AND
            var _r11_2 = _memo.Results.Pop();
            var _r11_1 = _memo.Results.Pop();

            if (_r11_1 != null && _r11_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i11, _index, _memo.InputEnumerable, _r11_1.Results.Concat(_r11_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i11;
            }

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label10; }

            // LITERAL "*/"
            _ParseLiteralString(_memo, ref _index, "*/");

        label10: // AND
            var _r10_2 = _memo.Results.Pop();
            var _r10_1 = _memo.Results.Pop();

            if (_r10_1 != null && _r10_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i10, _index, _memo.InputEnumerable, _r10_1.Results.Concat(_r10_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i10;
            }

        label0: // OR
            int _dummy_i0 = _index; // no-op for label

        }


        public void EOL(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            _Parser_Item nl = null;

            // OR 2
            int _start_i2 = _index;

            // AND 3
            int _start_i3 = _index;

            // LITERAL '\r'
            _ParseLiteralChar(_memo, ref _index, '\r');

            // AND shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Push(null); goto label3; }

            // LITERAL '\n'
            _ParseLiteralChar(_memo, ref _index, '\n');

            // QUES
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _memo.Results.Push(new _Parser_Item(_index, _memo.InputEnumerable)); }

        label3: // AND
            var _r3_2 = _memo.Results.Pop();
            var _r3_1 = _memo.Results.Pop();

            if (_r3_1 != null && _r3_2 != null)
            {
                _memo.Results.Push( new _Parser_Item(_start_i3, _index, _memo.InputEnumerable, _r3_1.Results.Concat(_r3_2.Results).Where(_NON_NULL), true) );
            }
            else
            {
                _memo.Results.Push(null);
                _index = _start_i3;
            }

            // OR shortcut
            if (_memo.Results.Peek() == null) { _memo.Results.Pop(); _index = _start_i2; } else goto label2;

            // LITERAL '\n'
            _ParseLiteralChar(_memo, ref _index, '\n');

        label2: // OR
            int _dummy_i2 = _index; // no-op for label

            // BIND nl
            nl = _memo.Results.Peek();

            // ACT
            var _r0 = _memo.Results.Peek();
            if (_r0 != null)
            {
                _memo.Results.Pop();
                _memo.Results.Push( new _Parser_Item(_r0.StartIndex, _r0.NextIndex, _memo.InputEnumerable, _Thunk(_IM_Result => { _memo.Positions.Add(nl.NextIndex); return nl; }, _r0), true) );
            }

        }


        public void EOF(_Parser_Memo _memo, int _index, _Parser_Args _args)
        {

            // NOT 0
            int _start_i0 = _index;

            // ANY
            _ParseAny(_memo, ref _index);

            // NOT 0
            var _r0 = _memo.Results.Pop();
            _memo.Results.Push( _r0 == null ? new _Parser_Item(_start_i0, _memo.InputEnumerable) : null);
            _index = _start_i0;

        }

    } // class Parser

} // namespace IronMeta.Generator


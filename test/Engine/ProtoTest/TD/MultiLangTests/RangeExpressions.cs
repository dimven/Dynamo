using System;
using System.Collections.Generic;
using NUnit.Framework;
using ProtoCore.DSASM.Mirror;
using ProtoCore.Runtime;
using ProtoTestFx.TD;
namespace ProtoTest.TD.MultiLangTests
{
    class RangeExpressions : ProtoTestBase
    {
        [Test]
        [Category("SmokeTest")]
        public void T01_SimpleRangeExpression()
        {
            string src = @"
a=i[0];a1=i[1];a2=i[2];a3=i[3];a4=i[4];a5=i[5];a6=i[6];a7=i[7];a8=i[8];a9=i[9];a10=i[10];a11=i[11];a12=i[12];a13=i[13];a14=i[14];a15=i[15];
i = [Imperative]
{
	a = 1..-6..-2;
	a1 = 2..6..~2.5; 
	a2 = 0.8..1..0.2; 
	a3 = 0.7..1..0.3; 
	a4 = 0.6..1..0.4; 
	a5 = 0.8..1..0.1; 
	a6 = 1..1.1..0.1; 
	a7 = 9..10..1; 
	a8 = 9..10..0.1;
	a9 = 0..1..0.1; 
	a10 = 0.1..1..0.1;
	a11 = 0.5..1..0.1;
	a12 = 0.4..1..0.1;
	a13 = 0.3..1..0.1;
	a14 = 0.2..1..0.1;
	a15 = (0.5)..(0.25)..(-0.25);
	return [a, a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13, a14, a15];
}
";
            ExecutionMirror mirror = thisTest.RunScriptSource(src);
            List<Object> result = new List<Object> { 1, -1, -3, -5 };
            thisTest.Verify("a", result);
            List<Object> result0 = new List<Object> { 2, 4, 6 };
            thisTest.Verify("a1", result0);
            List<Object> result1 = new List<Object> { 0.8, 1 };
            thisTest.Verify("a2", result1);
            List<Object> result2 = new List<Object> { 0.7, 1 };
            thisTest.Verify("a3", result2);
            List<Object> result3 = new List<Object> { 0.6, 1 };
            thisTest.Verify("a4", result3);
            List<Object> result4 = new List<Object> { 0.8, 0.9, 1 };
            thisTest.Verify("a5", result4);
            List<Object> result5 = new List<Object> { 1, 1.1 };
            thisTest.Verify("a6", result5);
            List<Object> result6 = new List<Object> { 9, 10 };
            thisTest.Verify("a7", result6);
            List<Object> result7 = new List<Object> { 9, 9.1, 9.2, 9.3, 9.4, 9.5, 9.6, 9.7, 9.8, 9.9, 10 };
            thisTest.Verify("a8", result7);
            List<Object> result8 = new List<Object> { 0, 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1 };
            thisTest.Verify("a9", result8);
            List<Object> result9 = new List<Object> { 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1 };
            thisTest.Verify("a10", result9);
            List<Object> result10 = new List<Object> { 0.5, 0.6, 0.7, 0.8, 0.9, 1 };
            thisTest.Verify("a11", result10);
            List<Object> result11 = new List<Object> { 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1 };
            thisTest.Verify("a12", result11);
            List<Object> result12 = new List<Object> { 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1 };
            thisTest.Verify("a13", result12);
            List<Object> result13 = new List<Object> { 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1 };
            thisTest.Verify("a14", result13);
            List<Object> result14 = new List<Object> { 0.5, 0.25 };
            thisTest.Verify("a15", result14);
        }

        [Test]
        [Category("SmokeTest")]
        public void T02_SimpleRangeExpression()
        {
            string src = @"a15=i[0];a16=i[1];a18=i[2];a19=i[3];a20=i[4];
i = [Imperative]
{
	a15 = 1/2..1/4..-1/4;
	a16 = (1/2)..(1/4)..(-1/4);
	a18 = 1.0/2.0..1.0/4.0..-1.0/4.0;
	a19 = (1.0/2.0)..(1.0/4.0)..(-1.0/4.0);
	a20 = 1..3*2; 
	//a21 = 1..-6;
    return [a15, a16, a18, a19, a20];
}";
            ExecutionMirror mirror = thisTest.RunScriptSource(src);
            List<Object> result = new List<Object> { 0.5, 0.25 };
            thisTest.Verify("a15", result);
            List<Object> result1 = new List<Object> { 0.5, 0.25 };
            thisTest.Verify("a16", result1);
            List<Object> result2 = new List<Object> { 0.5, 0.25 };
            thisTest.Verify("a18", result2);
            List<Object> result3 = new List<Object> { 0.5, 0.25 };
            thisTest.Verify("a19", result3);
            List<Object> result4 = new List<Object> { 1, 2, 3, 4, 5, 6 };
            thisTest.Verify("a20", result4);
        }

        [Test]
        public void T03_SimpleRangeExpressionUsingCollection()
        {
            string src = @"w1=i[0];w2=i[1];w3=i[2];w4=i[3];w5=i[4];
i = [Imperative]
{
	a = 3 ;
	b = 2 ;
	c = -1;
	w1 = a..b..-1 ; //correct  
	w2 = a..b..c; //correct 
	e1 = 1..2 ; //correct
	f = 3..4 ; //correct
	w3 = e1..f; //correct
	w4 = (3-2)..(w3[1][1])..(c+2) ; //correct
	w5 = (w3[1][1]-2)..(w3[1][1])..(w3[0][1]-1) ; //correct
    return [w1, w2, w3, w4, w5];
}
/* expected results : 
    Updated variable a = 3
    Updated variable b = 2
    Updated variable c = -1
    Updated variable w1 = { 3, 2 }
    Updated variable w2 = { 3, 2 }
    Updated variable e1 = { 1, 2 }
    Updated variable f = { 3, 4 }
    Updated variable w3 = { { 1, 2, 3 }, { 2, 3, 4 } }
    Updated variable w4 = { 1, 2, 3 }
    Updated variable w5 = { 1, 2, 3 }
*/
";
            ExecutionMirror mirror = thisTest.RunScriptSource(src);
            thisTest.Verify("w1", new object[] { 3, 2 });
            thisTest.Verify("w2", new object[] { 3, 2 });
            thisTest.Verify("w3", new object[] { new object[] { 1, 2, 3 }, new object[] { 2, 3, 4 } });
            thisTest.Verify("w4", new object[] { 1, 2, 3 });
            thisTest.Verify("w5", new object[] { 1, 2, 3 });
        }

        [Test]
        [Category("SmokeTest")]
        public void T04_SimpleRangeExpressionUsingFunctions()
        {
            string src = @"z1=i[0];z2=i[1];z3=i[2];z4=i[3];z5=i[4];z7=i[5];
	def twice : double( a : double ) 
	{
		return = 2 * a;
	}
i = [Imperative]
{
	z1 = 1..twice(4)..twice(1);
	z2 = 1..twice(4)..twice(1)-1;
	z3 = 1..twice(4)..(twice(1)-1);
	z4 = 2*twice(1)..1..-1;
	z5 = (2*twice(1))..1..-1;
	//z6 = z5 - z2 + 0.3;
	z7 = (z3[0]+0.3)..4..#1 ; 
   return [z1, z2, z3, z4, z5, z7];
}
/*
Succesfully created function 'twice' 
    Updated variable z1 = { 1, 3, 5, 7 }
    Updated variable z2 = { 1, 2, 3, ... , 6, 7, 8 }
    Updated variable z3 = { 1, 2, 3, ... , 6, 7, 8 }
    Updated variable z4 = { 4, 3, 2, 1 }
    Updated variable z5 = { 4, 3, 2, 1 }
    //Updated variable z6 = { 3.3, 1.3, -1.7, -2.7 }
    Updated variable z7 = { 1.3 }
	*/";
            ExecutionMirror mirror = thisTest.RunScriptSource(src);
            List<Object> result = new List<Object> { 1, 3, 5, 7 };
            thisTest.Verify("z1", result);
            List<Object> result1 = new List<Object> { 1, 2, 3, 4, 5, 6, 7, 8 };
            thisTest.Verify("z2", result1);
            List<Object> result2 = new List<Object> { 1, 2, 3, 4, 5, 6, 7, 8 };
            thisTest.Verify("z3", result2);
            List<Object> result3 = new List<Object> { 4, 3, 2, 1 };
            thisTest.Verify("z4", result3);
            List<Object> result4 = new List<Object> { 4, 3, 2, 1 };
            thisTest.Verify("z5", result4);
            List<Object> result8 = new List<Object> { 1.3 };
            thisTest.Verify("z7", result8);
        }

        [Test]
        [Category("SmokeTest")]
        public void T05_RangeExpressionWithIncrement()
        {
            string src = @"d=x[0];e1=x[1];f=x[2];g=x[3];h=x[4];i=x[5];j=x[6];k=x[7];l=x[8];m=x[9];
x=[Imperative]
{
	d = 0.9..1..0.1;
	e1 = -0.4..-0.5..-0.1;
	f = -0.4..-0.3..0.1;
	g = 0.4..1..0.2;
	h = 0.4..1..0.1;
	i = 0.4..1;
	j = 0.6..1..0.4;
	k = 0.09..0.1..0.01;
	l = 0.2..0.3..0.05;
	m = 0.05..0.1..0.04;
	n = 0.1..0.9..~0.3;
	k = 0.02..0.03..#3;
	l = 0.9..1..#5;
    return [d,e1,f,g,h,i,j,k,l,m];
}
";
            ExecutionMirror mirror = thisTest.RunScriptSource(src);
            List<Object> result3 = new List<Object> { 0.9, 1 };
            thisTest.Verify("d", result3);
            List<Object> result4 = new List<Object> { -0.4, -0.5 };
            thisTest.Verify("e1", result4);
            List<Object> result5 = new List<Object> { -0.4, -0.3 };
            thisTest.Verify("f", result5);
            List<Object> result6 = new List<Object> { 0.4, 0.6, 0.8, 1 };
            thisTest.Verify("g", result6);
            List<Object> result7 = new List<Object> { 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1 };
            thisTest.Verify("h", result7);
            List<Object> result8 = new List<Object> { 0.4 };
            thisTest.Verify("i", result8);
            List<Object> result9 = new List<Object> { 0.6, 1 };
            thisTest.Verify("j", result9);
            List<Object> result10 = new List<Object> { 0.02, 0.025, 0.03 };
            thisTest.Verify("k", result10);
            List<Object> result11 = new List<Object> { 0.9, 0.925, 0.95, 0.975, 1 };
            thisTest.Verify("l", result11);
            List<Object> result12 = new List<Object> { 0.05, 0.09 };
            thisTest.Verify("m", result12);
        }

        [Test]
        [Category("SmokeTest")]
        public void T06_RangeExpressionWithIncrement()
        {
            string src = @"a=i[0];
b=i[1];
c=i[2];
i = [Imperative]
{
	a = 0.3..0.1..-0.1;
	b = 0.1..0.3..0.2;
	c = 0.1..0.3..0.1;
    return [a, b, c];
}";
            ExecutionMirror mirror = thisTest.RunScriptSource(src);
            List<Object> result = new List<Object> { 0.3, 0.2, 0.1 };
            thisTest.Verify("a", result);
            List<Object> result1 = new List<Object> { 0.1, 0.3 };
            thisTest.Verify("b", result1);
            List<Object> result2 = new List<Object> { 0.1, 0.2, 0.3 };
            thisTest.Verify("c", result2);
        }

        [Test]
        [Category("SmokeTest")]
        public void T07_RangeExpressionWithIncrementUsingFunctionCall()
        {
            string code = @"
d=i[0];f=i[1];
	def increment : double[] (x : double[]) 
	{
        return = [Imperative]
        {
		    j = 0;
		    for( i in x )
		    {
			    x[j] = x[j] + 1 ;
			    j = j + 1;
		    }
		    return = x;
        }
	}
i = [Imperative]
{
	a = [1,2,3];
	b = [3,4,5] ;
	c = [1.5,2.5,4,3.65];
	f = [7,8*2,9+1,5-3,-1,-0.34];
	//nested collection
	d = [3.5,increment(c)];
    return [d, f];
}";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            List<Object> l1 = new List<object> { 3.5, new List<Object> { 2.5, 3.5, 5, 4.65 } };
            List<Object> l2 = new List<object> { 7, 16, 10, 2, -1, -0.34 };
            thisTest.Verify("d", l1);
            thisTest.Verify("f", l2);
        }

        [Test]
        public void T08_RangeExpressionWithIncrementUsingVariables()
        {
            string src = @"h=x[0];i=x[1];j=x[2];k=x[3];l=x[4];
	def square : double ( x :double ) 
	{
		return = x * x;
	}
x = [Imperative]
{
	z = square(4);
	x = 1 ;
	y = -2 ;
	a = 1..2 ;
	b = 1..6..3;
	c = 2..3..1;
	d = 2..10..2;
	e1 = 1..3..0.5;
	f = 2..4..0.2 ;
	//using variables
	h = z..3..-4;
	i = 1..z..x;
	j = z..x..y; 
	k = a..b..x ;
	l = a..c..x ;
	//using function call 
	g = 6..9.5..square(-1);
	m = 0.8..square(1)..0.1; 
	n = square(1)..0.8..-0.1;
	o = 0.8..square(0.9)..0.01; 
    return [h, i, j, k, l];
}
/*
result
z = 16
x = 1
y = -2
a = {1,2}
b = {1,4}
c = {2,3}
d = {2,4,6,8,10}
e1 = {1.000000,1.500000,2.000000,2.500000,3.000000}
f = {2.000000,2.200000,2.400000,2.600000,2.800000,3.000000,3.200000,3.400000,3.600000,3.800000,4.000000}
h = {16,12,8,4}
i = {1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16}
j = {16,14,12,10,8,6,4,2}
k = {{1},{2,3,4}}
l = {{1,2},{2,3}}
g = {6.000000,7.000000,8.000000,9.000000}
m = {0.800000,0.900000,1.000000}
n = {1.000000,0.900000,0.800000}
o = {0.800000,0.810000}
*/";
            ExecutionMirror mirror = thisTest.RunScriptSource(src);
            thisTest.Verify("h", new object[] { 16.0, 12.0, 8.0, 4.0 });
            thisTest.Verify("i", new object[] { 1.0, 2.0, 3.0, 4.0, 5.0, 6.0, 7.0, 8.0, 9.0, 10.0, 11.0, 12.0, 13.0, 14.0, 15.0, 16.0 });
            thisTest.Verify("j", new object[] { 16.0, 14.0, 12.0, 10.0, 8.0, 6.0, 4.0, 2.0 });
            thisTest.Verify("k", new object[] { new object[] { 1 }, new object[] { 2, 3, 4 } });
            thisTest.Verify("l", new object[] { new object[] { 1, 2 }, new object[] { 2, 3 } });
        }

        [Test]
        [Category("SmokeTest")]
        public void T09_RangeExpressionWithApproximateIncrement()
        {
            string src = @"a=i[0];b=i[1];f=i[2];g=i[3];h=i[4];j=i[5];k=i[6];l=i[7];
	def square : double ( x: double ) 
	{
		return = x * x;
	}
i=[Imperative]
{
	x = 0.1; 
	a = 0..2..~0.5;
	b = 0..0.1..~square(0.1);
	f = 0..0.1..~x;      
	g = 0.2..0.3..~x;    
	h = 0.3..0.2..~-0.1; 
	
	j = 0.8..0.5..~-0.3;
	k = 0.5..0.8..~0.3; 
	l = 0.2..0.3..~0.0;
    return [a, b, f, g, h, j, k, l];
}
";
            ExecutionMirror mirror = thisTest.RunScriptSource(src);
            List<Object> result3 = new List<Object> { 0.0, 0.5, 1.0, 1.5, 2.0 };
            thisTest.Verify("a", result3);
            List<Object> result4 = new List<Object> { 0, 0.01, 0.02, 0.03, 0.04, 0.05, 0.06, 0.07, 0.08, 0.09, 0.1, };
            thisTest.Verify("b", result4);
            List<Object> result5 = new List<Object> { 0, 0.1 };
            thisTest.Verify("f", result5);
            List<Object> result6 = new List<Object> { 0.2, 0.3 };
            thisTest.Verify("g", result6);
            List<Object> result7 = new List<Object> { 0.3, 0.2 };
            thisTest.Verify("h", result7);
            List<Object> result9 = new List<Object> { 0.8, 0.5 };
            thisTest.Verify("j", result9);
            List<Object> result10 = new List<Object> { 0.5, 0.8 };
            thisTest.Verify("k", result10);
        }

        [Test]
        [Category("Replication")]
        public void T10_RangeExpressionWithReplication()
        {
            //Assert.Fail("1454507 - Sprint15 : Rev 666 : Nested range expressions are throwing NullReferenceException ");
            //Assert.Fail("Replication guides are not implmented");
            string src = @"a = i[0];b = i[1];
i = [Imperative]
{
	//step value greater than the end value
	a = 1..2..3;
	b = 3..4..3;
	c = a..b..a[0]; // {{1,2,3}}
	d = 0.5..0.9..0.1;
	e1 = 0.1..0.2..0.05;
	f = e1[1]..d[2]..0.5;
	g = e1..d..0.2;
	h = e1[2]..d[1]..0.5;
    return [a, b];
}
";
            ExecutionMirror mirror = thisTest.RunScriptSource(src);
            List<Object> result = new List<Object>() { 1 };
            thisTest.Verify("a", result);
            List<Object> result1 = new List<Object>() { 3 };
            thisTest.Verify("b", result1);
            //List<Object> result2 = new List<Object>() { { 1, 2, 3 } };
            //thisTest.Verify("c", result2);
            // List<Object> result3 = new List<Object>() { { { 0.100 }, { 0.100, 0.600 }, { 0.100, 0.600 }, { 0.100, 0.600 }, { 0.100, 0.600 } }, { { 0.150 }, { 0.150 }, { 0.150, 0.650 }, { 0.150, 0.650 }, { 0.150, 0.650 } }, { { 0.200 }, { 0.200 }, { 0.200, 0.700 }, { 0.200, 0.700 }, { 0.200, 0.700 } } };
            // thisTest.Verify("f", result3);
            // List<Object> result4 = new List<Object>() { { { 0.100 }, { 0.150 }, { 0.200 } }, { { 0.100, 0.600 }, { 0.150 }, { 0.200 } }, { { 0.100, 0.600 }, { 0.150, 0.650 }, { 0.200, 0.700 } }, { { 0.100, 0.600 }, { 0.150, 0.650 }, { 0.200, 0.700 } }, { { 0.100, 0.600 }, { 0.150, 0.650 }, { 0.200, 0.700 } } };
            // thisTest.Verify("h", result4);
        }

        [Test]
        public void T12_RangeExpressionUsingNestedRangeExpressions()
        {
            string src = @"b=x[0];c=x[1];d=x[2];e1=x[3];f=x[4];g=x[5];h=x[6];i=x[7];j=x[8];
x=[Imperative]
{
	x = 1..5..2; // {1,3,5}
	y = 0..6..2; // {0,2,4,6}
	a = (3..12..3)..(4..16..4); // {3,6,9,12} .. {4..8..12..16}
	b = 3..00.6..#5;      // {3.0,2.4,1.8,1.2,0.6}
	//c = b[0]..7..#1;    //This indexed case works
	c = 5..7..#1;         //Compile error here , 5
	d = 5.5..6..#3;       // {5.5,5.75,6.0}
	e1 = -6..-8..#3;      //{-6,-7,-8}
	f = 1..0.8..#2;       //{1,0.8}
	g = 1..-0.8..#3;      // {1.0,0.1,-0.8}
	h = 2.5..2.75..#4;    //{2.5,2.58,2.67,2.75}
	i = x[0]..y[3]..#10;//1..6..#10
	j = 1..0.9..#4;// {1.0, 0.96,.93,0.9}
	k= 1..3..#0;//null
    return [b,c,d,e1,f,g,h,i,j];
}";
            ExecutionMirror mirror = thisTest.RunScriptSource(src);
            thisTest.Verify("b", new object[] { 3.0, 2.4, 1.8, 1.2, 0.6 });
            thisTest.Verify("c", new object[] { 5 });
            thisTest.Verify("d", new object[] { 5.5, 5.75, 6.0 });
            thisTest.Verify("e1", new object[] { -6, -7, -8 });
            thisTest.Verify("f", new object[] { 1.0, 0.8 });
            thisTest.Verify("g", new object[] { 1.0, 0.1, -0.8 });
            thisTest.Verify("h", new object[] { 2.5, 2.5 + 0.25 / 3.0, 2.5 + 0.5 / 3.0, 2.75 });
            thisTest.Verify("i", new Object[] { 1.000000, 1.555556, 2.111111, 2.666667, 3.222222, 3.777778, 4.333333, 4.888889, 5.444444, 6.000000 });
            thisTest.Verify("j", new object[] { 1.0, 1.0 - 0.1 / 3.0, 1.0 - 0.2 / 3.0, 0.9 });
        }

        [Test]
        [Category("SmokeTest")]
        public void T13_RangeExpressionWithStartEndValuesUsingFunctionCall()
        {
            string src = @"x=i[0];b=i[1];c=i[2];e1=i[3];f=i[4];g=i[5];
	def even : double (a : int) 
	{
        return = [Imperative]
        {
		    if((a % 2)>0)
		        return = (a+(a * 0.5));
		    else
		        return = (a-(a * 0.5));
        }
    }	
i = [Imperative]
{
	d = 3;
	x = 1..2..#d;
	a = even(2) ;
	b = 1..a;
	c = even(3)..even(5)..#6;
	d = even(5)..even(6)..#4;
	e1 = e..4..#3;  //e takes default value 2.17
	f = even(3)..(even(8)+4*0.5)..#3;
	g = even(2)+1..1..#5;
    return [x, b, c, e1, f, g];
}
";
            ExecutionMirror mirror = thisTest.RunScriptSource(src);
            List<Object> result3 = new List<Object> { 1, 1.5, 2.0 };
            thisTest.Verify("x", result3);
            List<Object> result4 = new List<Object> { 1 };
            thisTest.Verify("b", result4);
            List<Object> result5 = new List<Object> { 4.5, 5.1, 5.6999999999999993, 6.2999999999999989, 6.8999999999999986, 7.4999999999999982 };
            thisTest.Verify("c", result5);
            thisTest.Verify("e1", null);
            List<Object> result9 = new List<Object> { 4.5, 5.25, 6.0 };
            thisTest.Verify("f", result9);
            List<Object> result10 = new List<Object> { 2.0, 1.75, 1.5, 1.25, 1.0 };
            thisTest.Verify("g", result10);
        }

        [Test]
        [Category("SmokeTest")]
        public void T15_SimpleRangeExpression_1()
        {
            string src = @"a=x[0];b=x[1];d=x[2];f=x[3];g=x[4];h=x[5];i=x[6];l=x[7];
x = [Imperative]
{
	a = 1..2.2..#3;
	b = 0.1..0.2..#4;
	c = 1..3..~0.2;
	d = (a[0]+1)..(c[2]+0.9)..0.1; 
	e1 = 6..0.5..~-0.3;
	f = 0.5..1..~0.3;
	g = 0.5..0.6..0.01;
	h = 0.51..0.52..0.01;
	i = 0.95..1..0.05;
	j = 0.8..0.99..#10;
	//k = 0.9..1..#1;
	l = 0.9..1..0.1;
    return [a, b, d, f, g, h, i, l];
}
";
            ExecutionMirror mirror = thisTest.RunScriptSource(src);
            List<Object> result = new List<Object> { 1, 1.6, 2.2 };
            thisTest.Verify("a", result);
            List<Object> result1 = new List<Object> { 0.1, 0.13333333333333333, 0.16666666666666666, 0.2 };
            thisTest.Verify("b", result1);
            List<Object> result2 = new List<Object> { 2, 2.1, 2.2, 2.3000000000000003 };
            thisTest.Verify("d", result2);
            List<Object> result3 = new List<Object> { 0.5, 0.75, 1 };
            thisTest.Verify("f", result3);
            List<Object> result4 = new List<Object> { 0.5, 0.51, 0.52, 0.53, 0.54, 0.55, 0.56, 0.57, 0.58, 0.59, 0.6 };
            thisTest.Verify("g", result4);
            List<Object> result5 = new List<Object> { 0.51, 0.52 };
            thisTest.Verify("h", result5);
            List<Object> result6 = new List<Object> { 0.95, 1 };
            thisTest.Verify("i", result6);
            //List<Object> result7 = new List<Object>() { 0.9 };
            ////thisTest.Verify("k", result7);
            List<Object> result8 = new List<Object> { 0.9, 1 };
            thisTest.Verify("l", result8);
        }

        [Test]
        [Category("SmokeTest")]
        public void T16_SimpleRangeExpression_2()
        {
            string src = @"a=i[0];b=i[1];c=i[2];d=i[3];e1=i[4];f=i[5];g=i[6];h=i[7];
i = [Imperative]
{
	a = 1.2..1.3..0.1;
	b = 2..3..0.1;
	c = 1.2..1.5..0.1;
	//d = 1.3..1.4..~0.5; //incorrect 
	d = 1.3..1.4..0.5; 
	e1 = 1.5..1.7..~0.2;
	f = 3..3.2..~0.2;
	g = 3.6..3.8..~0.2; 
	h = 3.8..4..~0.2; 
    return [a, b, c, d, e1, f, g, h];
}";
            ExecutionMirror mirror = thisTest.RunScriptSource(src);
            List<Object> result = new List<Object> { 1.2, 1.3 };
            thisTest.Verify("a", result);
            List<Object> result1 = new List<Object> { 2, 2.1, 2.2, 2.3, 2.4, 2.5, 2.6, 2.7, 2.8, 2.9, 3 };
            thisTest.Verify("b", result1);
            List<Object> result2 = new List<Object> { 1.2, 1.3, 1.4, 1.5 };
            thisTest.Verify("c", result2);
            List<Object> result3 = new List<Object> { 1.3 };
            thisTest.Verify("d", result3);
            List<Object> result5 = new List<Object> { 1.5, 1.7 };
            thisTest.Verify("e1", result5);
            List<Object> result6 = new List<Object> { 3.0, 3.2 };
            thisTest.Verify("f", result6);
            List<Object> result7 = new List<Object> { 3.6, 3.8 };
            thisTest.Verify("g", result7);
            List<Object> result8 = new List<Object> { 3.8, 4.0 };
            thisTest.Verify("h", result8);
        }

        [Test]
        [Category("SmokeTest")]
        public void T17_SimpleRangeExpression_3()
        {
            string src = @"a=x[0];b=x[1];c=x[2];d=x[3];e1=x[4];f=x[5];g=x[6];h=x[7];i=x[8];j=x[9];k=x[10];
x=[Imperative]
{
	a = 1..2.2..~0.2;
	b = 1..2..#3;
	c = 2.3..2..#3;
	d = 1.2..1.4..~0.2;
	e1 = 0.9..1..0.1;
	f = 0.9..0.99..~0.01;
	g = 0.8..0.9..~0.1;
	h = 0.8..0.9..0.1;
	i = 0.9..1.1..0.1;
	j = 1..0.9..-0.05;
	k = 1.2..1.3..~0.1;
    return [a,b,c,d,e1,f,g,h,i,j,k];
}";
            ExecutionMirror mirror = thisTest.RunScriptSource(src);
            List<Object> result = new List<Object> { 1, 1.2, 1.4, 1.6, 1.8, 2, 2.2 };
            thisTest.Verify("a", result);
            List<Object> result1 = new List<Object> { 1, 1.5, 2 };
            thisTest.Verify("b", result1);
            List<Object> result2 = new List<Object> { 2.3, 2.15, 2 };
            thisTest.Verify("c", result2);
            List<Object> result3 = new List<Object> { 1.2, 1.4 };
            thisTest.Verify("d", result3);
            List<Object> result5 = new List<Object> { 0.9, 1 };
            thisTest.Verify("e1", result5);
            List<Object> result6 = new List<Object> { 0.9, 0.91, 0.92, 0.93, 0.94, 0.95, 0.96, 0.97, 0.98, 0.99 };
            thisTest.Verify("f", result6);
            List<Object> result7 = new List<Object> { 0.8, 0.9 };
            thisTest.Verify("g", result7);
            List<Object> result8 = new List<Object> { 0.8, 0.9 };
            thisTest.Verify("h", result8);
            List<Object> result9 = new List<Object> { 0.9, 1, 1.1 };
            thisTest.Verify("i", result9);
            List<Object> result10 = new List<Object> { 1, 0.95, 0.9 };
            thisTest.Verify("j", result10);
            List<Object> result11 = new List<Object> { 1.2, 1.3 };
            thisTest.Verify("k", result11);
        }

        [Test]
        [Category("SmokeTest")]
        public void T18_SimpleRangeExpression_4()
        {
            string src = @"a=i[0];b=i[1];c=i[2];d=i[3];e1=i[4];f=i[5];g=i[6];h=i[7];
i = [Imperative]
{
	a = 2.3..2.6..0.3;
	b = 4.3..4..-0.3;
	c= 3.7..4..0.3;
	d = 4..4.3..0.3;
	e1 = 3.2..3.3..0.3;
	f = 0.4..1..0.1;
	g = 0.4..0.45..0.05;
	h = 0.4..0.45..~0.05; 
	g = 0.4..0.6..~0.05;
    return [a,b,c,d,e1,f,g,h];
}";
            ExecutionMirror mirror = thisTest.RunScriptSource(src);
            List<Object> result = new List<Object> { 2.3, 2.6 };
            thisTest.Verify("a", result);
            List<Object> result1 = new List<Object> { 4.3, 4 };
            thisTest.Verify("b", result1);
            List<Object> result2 = new List<Object> { 3.7, 4 };
            thisTest.Verify("c", result2);
            List<Object> result3 = new List<Object> { 4, 4.3 };
            thisTest.Verify("d", result3);
            List<Object> result5 = new List<Object> { 3.2 };
            thisTest.Verify("e1", result5);
            List<Object> result6 = new List<Object> { 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1 };
            thisTest.Verify("f", result6);
            List<Object> result7 = new List<Object> { 0.4, 0.45, 0.5, 0.55, 0.6 };
            thisTest.Verify("g", result7);
            List<Object> result8 = new List<Object> { 0.4, 0.45 };
            thisTest.Verify("h", result8);
        }

        [Test]
        [Category("SmokeTest")]
        public void T19_SimpleRangeExpression_5()
        {
            string src = @"b=x[0];c=x[1];d=x[2];e1=x[3];f=x[4];g=x[5];h=x[6];i=x[7];
x=[Imperative]
{
	//a = 0.1..0.2..#1; //giving error
	b = 0.1..0.2..#2;
	c = 0.1..0.2..#3;
	d = 0.1..0.1..#4;
	e1 = 0.9..1..#5;
	f = 0.8..0.89..#3;
	g = 0.9..0.8..#3;
	h = 0.9..0.7..#5;
	i = 0.6..1..#4;
    return [b,c,d,e1,f,g,h,i];
}
";
            ExecutionMirror mirror = thisTest.RunScriptSource(src);
            //List<Object> result = new List<Object>() { 0.1 };
            //thisTest.Verify("a", result);
            List<Object> result1 = new List<Object> { 0.1, 0.2 };
            thisTest.Verify("b", result1);
            List<Object> result2 = new List<Object> { 0.1, 0.15, 0.2 };
            thisTest.Verify("c", result2);
            List<Object> result3 = new List<Object> { 0.1, 0.1, 0.1, 0.1 };
            thisTest.Verify("d", result3);
            List<Object> result5 = new List<Object> { 0.9, 0.925, 0.95, 0.975, 1 };
            thisTest.Verify("e1", result5);
            List<Object> result6 = new List<Object> { 0.8, 0.845, 0.89 };
            thisTest.Verify("f", result6);
            List<Object> result7 = new List<Object> { 0.9, 0.85, 0.8 };
            thisTest.Verify("g", result7);
            List<Object> result8 = new List<Object> { 0.9, 0.85, 0.8, 0.75, 0.7 };
            thisTest.Verify("h", result8);
            List<Object> result9 = new List<Object> { 0.6, 0.73333333333333328, 0.8666666666666667, 1.0 };
            thisTest.Verify("i", result9);
        }

        [Test]
        [Category("SmokeTest")]
        public void T20_RangeExpressionsUsingPowerOperator()
        {
            string src = @"e1=i[0];f=i[1];
	def power : double (a:double,b:int) 
	{
        return = [Imperative]
        {
		    temp = 1;
		    while( b > 0 )
		    {
			    temp = temp * a;
			    b = b - 1;
		    }
		    return = temp;
        }
	}
i=[Imperative]
{
	a = 3;
	b = 2; 
	c = power(2,3);
	d = b..a;
	e1 = b..c..power(2,1);
	f = power(1.0,1)..power(2,2)..power(0.5,1);   
	/*h = power(0.1,2)..power(0.2,2)..~power(0.1,2);
	i = power(0.1,1)..power(0.2,1)..~power(0.1,1);         has not been implemented yet
	j = power(0.4,1)..power(0.45,1)..~power(0.05,1);
	k = power(1.2,1)..power(1.4,1)..~power(0.2,1);
	l = power(1.2,1)..power(1.3,1)..~power(0.1,1); 
	m = power(0.8,1)..power(0.9,1)..~power(0.1,1);
	n = power(0.08,1)..power(0.3,2)..~power(0.1,2); */
    return [e1, f];
}";
            ExecutionMirror mirror = thisTest.RunScriptSource(src);
            List<Object> result = new List<Object> { 2, 4, 6, 8 };
            thisTest.Verify("e1", result);
            List<Object> result1 = new List<Object> { 1.0, 1.5, 2.0, 2.5, 3.0, 3.5, 4.0 };
            thisTest.Verify("f", result1);
            /*List<Object> result2 = new List<Object>() { 0.01,0.02,0.03,0.04 };
            thisTest.Verify("h", result2);
            List<Object> result3 = new List<Object>() { 0.1, 0.2 };
            thisTest.Verify("i", result3);
            List<Object> result5 = new List<Object>() { 0.4,0.45 };
            thisTest.Verify("j", result5);
            List<Object> result6 = new List<Object>() { 1.2,1.4 };
            thisTest.Verify("k", result6);
            List<Object> result7 = new List<Object>() { 1.2,1.3 };
            thisTest.Verify("l", result7);
            List<Object> result8 = new List<Object>() { 0.8,0.9 };
            thisTest.Verify("m", result8);
            List<Object> result9 = new List<Object>() { 0.08, 0.09 };
            thisTest.Verify("n", result9);*/
        }

        [Test]
        [Category("SmokeTest")]
        public void T21_RangeExpressionsUsingEvenFunction()
        {
            string src = @"c=x[0];d=x[1];e1=x[2];f=x[3];g=x[4];
	def even : int (a : int) 
	{	
        return = [Imperative]
        {
		    if(( a % 2 ) > 0 )
			    return = a + 1;
		
		    else 
			    return = a;
        }
	}
x=[Imperative]
{
	x = 1..3..1;
	y = 1..9..2;
	z = 11..19..2;
	c = even(x); // 2,2,4
	d = even(x)..even(c)..(even(0)+0.5); // {2,2,4}
	e1 = even(y)..even(z)..1; // {2,4,6,8,10} .. {12,14,16,18,20}..1
	f = even(e1[0])..even(e1[1]); // even({2,3,4,5,6,7,8,9,10,11,12} ..even({4,5,6,7,8,9,10,11,12,13,14})
   /*  {2,4,4,6,6,8,8,10,10,12,12} .. {4,6,6,8,8,10,10,12,12,14,14}
*/ 
	g = even(y)..even(z)..f[0][1];  // {2,4,6,8,10} .. {12,14,16,18,20} .. 3
    return [c,d,e1,f,g];
}
";
            ExecutionMirror mirror = thisTest.RunScriptSource(src);
            Object[] c = new Object[] { 2, 2, 4 };
            Object[] d = new Object[] { new Object[] { 2.000000 }, new Object[] { 2.000000 }, new Object[] { 4.000000 } };
            Object[][] e1 = new Object[][] { new Object[] { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 }, new Object[] { 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 }, new Object[] { 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 }, new Object[] { 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18 }, new Object[] { 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 } };
            Object[][] f = new Object[][] { new Object[] { 2, 3, 4 }, new Object[] { 4, 5, 6 }, new Object[] { 4, 5, 6 }, new Object[] { 6, 7, 8 }, new Object[] { 6, 7, 8 }, new Object[] { 8, 9, 10 }, new Object[] { 8, 9, 10 }, new Object[] { 10, 11, 12 }, new Object[] { 10, 11, 12 }, new Object[] { 12, 13, 14 }, new Object[] { 12, 13, 14 } };
            Object[][] g = { new Object[] { 2, 5, 8, 11 }, new Object[] { 4, 7, 10, 13 }, new Object[] { 6, 9, 12, 15 }, new Object[] { 8, 11, 14, 17 }, new Object[] { 10, 13, 16, 19 } };
            thisTest.Verify("c", c);
            thisTest.Verify("d", d);
            thisTest.Verify("e1", e1);
            thisTest.Verify("f", f);
            thisTest.Verify("g", g);
            //List<Object> result = new List<Object>()  {2,2,4};
            // thisTest.Verify("d", result);
        }

        [Test]
        [Category("SmokeTest")]
        public void TA01_RangeExpressionWithIntegerIncrement()
        {
            string src = @"a1=i[0];a2=i[1];
i = [Imperative]
{
	a1 = 1..5..2;
	a2 = 12.5..20..2;
    return [a1, a2];
}
";
            ExecutionMirror mirror = thisTest.RunScriptSource(src);
            List<Object> result = new List<Object> { 1, 3, 5 };
            thisTest.Verify("a1", result);
            List<Object> result1 = new List<Object> { 12.5, 14.5, 16.5, 18.5 };
            thisTest.Verify("a2", result1);
        }

        [Test]
        [Category("SmokeTest")]
        public void TA02_RangeExpressionWithDecimalIncrement()
        {
            string src = @"a1=i[0];a2=i[1];
i=[Imperative]
{
	a1 = 2..9..2.7;
	a2 = 10..11.5..0.3;
     return [a1, a2];
}";
            ExecutionMirror mirror = thisTest.RunScriptSource(src);
            List<Object> result = new List<Object> { 2, 4.7, 7.4 };
            thisTest.Verify("a1", result);
            List<Object> result1 = new List<Object> { 10, 10.3, 10.6, 10.9, 11.2, 11.5 };
            thisTest.Verify("a2", result1);
        }

        [Test]
        [Category("SmokeTest")]
        public void TA03_RangeExpressionWithNegativeIncrement()
        {
            string src = @"a=i[0];b=i[1];c=i[2];
i=[Imperative]
{
	a = 10..-1..-2;
	b = -2..-10..-1;
	c = 10..3..-1.5;
     return [a, b, c];
}
";
            ExecutionMirror mirror = thisTest.RunScriptSource(src);
            List<Object> result = new List<Object> { 10, 8, 6, 4, 2, 0 };
            thisTest.Verify("a", result);
            List<Object> result1 = new List<Object> { -2, -3, -4, -5, -6, -7, -8, -9, -10 };
            thisTest.Verify("b", result1);
            List<Object> result2 = new List<Object> { 10, 8.5, 7, 5.5, 4 };
            thisTest.Verify("c", result2);
        }

        [Test]
        [Category("SmokeTest")]
        public void TA04_RangeExpressionWithNullIncrement()
        {
            string src = @"a=i[0];b=i[1];
i=[Imperative]
{
	a = 1..5..null;
	b = 0..6..(null);
    return [a,b];
}";
            ExecutionMirror mirror = thisTest.RunScriptSource(src);
            thisTest.Verify("a", null);
            thisTest.Verify("b", null);
        }

        [Test]
        [Category("SmokeTest")]
        public void TA05_RangeExpressionWithBooleanIncrement()
        {
            string src = @"a=i[0];b=i[1];
i=[Imperative]
{
	a = 2.5..6..(true);
	b = 3..7..false;
return [a,b];
}
";
            ExecutionMirror mirror = thisTest.RunScriptSource(src);
            thisTest.Verify("a", null);
            thisTest.Verify("b", null);
        }

        [Test]
        [Category("SmokeTest")]
        public void TA06_RangeExpressionWithIntegerTildeValue()
        {
            string src = @"a=i[0];b=i[1];
i=[Imperative]
{
	a = 1..10..~4;
	b = -2.5..10..~5;
    return [a,b];
}";
            ExecutionMirror mirror = thisTest.RunScriptSource(src);
            List<Object> result = new List<Object> { 1, 5.5, 10 };
            thisTest.Verify("a", result);
            List<Object> result1 = new List<Object> { -2.5, 1.6666666666666666666666666667, 5.8333333333333333333333333334, 10 };
            thisTest.Verify("b", result1);
        }

        [Test]
        [Category("SmokeTest")]
        public void TA07_RangeExpressionWithDecimalTildeValue()
        {
            string code = @"
a=i[0];b=i[1];
i=[Imperative]
{
	a = 0.2..0.3..~0.2; //divide by zero error
	b = 6..13..~1.3;
    return [a,b];
}
";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            object[] expectedResult = { 0.2, 0.3 };
            object[] expectedResult2 = { 6.0, 7.4, 8.8, 10.2, 11.6, 13.0 };
            thisTest.Verify("a", expectedResult, 0);
            thisTest.Verify("b", expectedResult2, 0);
        }

        [Test]
        [Category("SmokeTest")]
        public void TA08_RangeExpressionWithNegativeTildeValue()
        {
            string src = @"a=i[0];b=i[1];
i=[Imperative]
{
	a = 3..1..~-0.5;
	b = 18..13..~-1.3;
    return [a,b];
}";
            ExecutionMirror mirror = thisTest.RunScriptSource(src);
            List<Object> result = new List<Object> { 3, 2.5, 2, 1.5, 1 };
            thisTest.Verify("a", result);
            List<Object> result1 = new List<Object> { 18, 16.75, 15.5, 14.25, 13 };
            thisTest.Verify("b", result1);
        }

        [Test]
        [Category("SmokeTest")]
        public void TA09_RangeExpressionWithNullTildeValue()
        {
            string src = @"a=i[0];b=i[1];
i=[Imperative]
{
	a = 1..5..~null;
	b = 5..2..~(null);
    return [a,b];
}";
            ExecutionMirror mirror = thisTest.RunScriptSource(src);
            thisTest.Verify("a", null);
            thisTest.Verify("b", null);
        }

        [Test]
        [Category("SmokeTest")]
        public void TA10_RangeExpressionWithBooleanTildeValue()
        {
            string src = @"a=i[0];b=i[1];
i=[Imperative]
{
	a = 1..3..(true);
	b = 2..2..false;
    return [a,b];
}";
            ExecutionMirror mirror = thisTest.RunScriptSource(src);
            thisTest.Verify("a", null);
            thisTest.Verify("b", null);
        }

        [Test]
        [Category("SmokeTest")]
        public void TA11_RangeExpressionWithIntegerHashValue()
        {
            string src = @"a=i[0];b=i[1];c=i[2];
i=[Imperative]
{
	a = 1..3.3..#5;
	b = 3..3..#3;
	c = 3..3..#1;
    return [a,b,c];
}";
            ExecutionMirror mirror = thisTest.RunScriptSource(src);
            List<Object> result = new List<Object> { 1, 1.575, 2.150, 2.725, 3.3 };
            thisTest.Verify("a", result);
            List<Object> result2 = new List<Object> { 3, 3, 3 };
            thisTest.Verify("b", result2);
            List<Object> result1 = new List<Object> { 3 };
            thisTest.Verify("c", result1);
        }

        [Test]
        [Category("SmokeTest")]
        public void TA12_RangeExpressionWithDecimalHashValue()
        {
            string src = @"a=i[0];b=i[1];
i=[Imperative]
{
	a = 1..7..#2.5;
	b = 2..10..#2.4;
    return [a,b];
}";
            ExecutionMirror mirror = thisTest.RunScriptSource(src);
            TestFrameWork.VerifyBuildWarning(ProtoCore.BuildData.WarningID.InvalidRangeExpression);
        }

        [Test]
        [Category("SmokeTest")]
        public void TA13_RangeExpressionWithNegativeHashValue()
        {
            string src = @"a=[Imperative]
{
	return 7.5..-2..#-9;
}";
            ExecutionMirror mirror = thisTest.RunScriptSource(src);
            thisTest.Verify("a", null);
        }

        [Test]
        [Category("SmokeTest")]
        public void TA14_RangeExpressionWithNullHashValue()
        {
            string src = @"a=[Imperative]
{
	return 2..10..#null;
}";
            ExecutionMirror mirror = thisTest.RunScriptSource(src);
            thisTest.Verify("a", null);
        }

        [Test]
        [Category("SmokeTest")]
        public void TA15_RangeExpressionWithBooleanHashValue()
        {
            string src = @"a=i[0];b=i[1];
i=[Imperative]
{
	b = 12..12..#false;
	a = 12..12..#(true);
    return [a,b];
}";
            ExecutionMirror mirror = thisTest.RunScriptSource(src);
            thisTest.Verify("a", null);
            thisTest.Verify("b", null);
        }

        [Test]
        [Category("SmokeTest")]
        public void TA16_RangeExpressionWithIncorrectLogic_1()
        {
            string src = @"a=[Imperative]
{
	return 5..1..2;
}";
            ExecutionMirror mirror = thisTest.RunScriptSource(src);
            thisTest.Verify("a", null);
        }

        [Test]
        [Category("SmokeTest")]
        public void TA17_RangeExpressionWithIncorrectLogic_2()
        {
            string src = @"a=[Imperative]
{
	return 5.5..10.7..-2;
}";
            ExecutionMirror mirror = thisTest.RunScriptSource(src);
            thisTest.Verify("a", null);
        }

        [Test]
        [Category("SmokeTest")]
        public void TA18_RangeExpressionWithIncorrectLogic_3()
        {
            string src = @"a=i[0];b=i[1];c=i[2];
i=[Imperative]
{
	a = 7..7..5;
	b = 8..8..~3;
	c = 9..9..#1;
    return [a, b, c];
}";
            ExecutionMirror mirror = thisTest.RunScriptSource(src);
            List<Object> result1 = new List<Object> { 7 };
            thisTest.Verify("a", result1);
            List<Object> result2 = new List<Object> { 8 };
            thisTest.Verify("b", result2);
            List<Object> result3 = new List<Object> { 9 };
            thisTest.Verify("c", result3);
        }

        [Test]
        [Category("SmokeTest")]
        public void TA19_RangeExpressionWithIncorrectLogic_4()
        {
            string src = @"a=[Imperative]
{
	return null..8..2;
}";
            ExecutionMirror mirror = thisTest.RunScriptSource(src);
            thisTest.Verify("a", null);
        }

        [Test]
        [Category("SmokeTest")]
        public void TA21_Defect_1454692()
        {
            string code = @"
x=[Imperative]
{
	x = 0;
	b = 0..3; //{ 0, 1, 2, 3 }
	for( y in b )
	{
		x = y + x;
	}
	return x;
}	";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("x", 6, 0);
        }

        [Test]
        [Category("SmokeTest")]
        public void TA21_Defect_1454692_2()
        {
            string code = @"
def length : int (pts : double[])
{
    numPts = [Imperative]
    {
        counter = 0;
        for(pt in pts)
        {
            counter = counter + 1;
        }
        
        return = counter;
    }
    return = numPts;
}
    
arr = 0.0..3.0;//{0.0,1.0,2.0,3.0};
num = length(arr);
	";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("num", 4, 0);
        }

        [Test]
        [Category("DSDefinedClass_Ported")]
        [Category("SmokeTest")]
        public void TA21_Defect_1454692_3()
        {
            string code = @"

def length (pts : double[] )
{
	numPts = [Imperative]
	{
		counter = 0;
		for(pt in pts)
		{
			counter = counter + 1;
		}
			
		return = counter;
	}
	return = numPts;
}
    
arr = 0.0..3.0;
len = length(arr);
	";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("len", 4, 0);
        }


        [Test]
        [Category("SmokeTest")]
        public void TA21_Defect_1454692_4()
        {
            string code = @"
def foo(i : int[])
{
    count = 0;
	count = [Imperative]
	{
	    for ( x in i )
		{
		    count = count + 1;
		}
		return = count;
	}
	return = count;
}	
    
arr = 0.0..3.0;//{0.0,1.0,2.0,3.0};
c=i[0];x=i[1];
i=[Imperative]
{
	x = 0;
	b = 0..3; //{ 0, 1, 2, 3 }
	for( y in arr )
	{
		x = y + x;
	}
	x1 = 0..3;
	c = foo(x1);
    return [c, x];
}
	";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            thisTest.Verify("c", 4);
            thisTest.Verify("x", 6);
        }

        [Test]
        public void T25_RangeExpression_WithDefaultDecrement()
        {
            // 1467121
            string code = @"
a=5..1;
";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Object[] a = new Object[] { 5, 4, 3, 2, 1 };
            thisTest.Verify("a", a);

        }

        [Test]
        public void T25_RangeExpression_WithDefaultDecrement_1467121()
        {
            // 1467121
            string code = @"
a=5..1;
b=-5..-1;
c=1..0.5;
d=1..-0.5;
";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Object[] a = new Object[] { 5, 4, 3, 2, 1 };
            thisTest.Verify("a", a);
        }

        [Test]
        public void T25_RangeExpression_WithDefaultDecrement_nested_1467121_2()
        {
            // 1467121
            string code = @"
a=(5..1).. (1..5);
";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Object[][] a = new Object[][] { new Object[] { 5, 4, 3, 2, 1 }, new Object[] { 4, 3, 2 }, new Object[] { 3 }, new Object[] { 2, 3, 4 }, new Object[] { 1, 2, 3, 4, 5 } };
            //thisTest.Verify("a", a);
        }


        [Test]
        public void T26_RangeExpression_Function_tilda_1457845()
        {
            // 1467121
            string code = @"
x=i[0];a=i[1];b=i[2];f=i[3];g=i[4];h=i[5];j=i[6];k=i[7];l=i[8];m=i[9];
	def square : double ( x: double ) 
	{
		return = x * x;
	}
i=[Imperative]
{
	x = 0.1; 
	a = 0..2..~0.5;
	b = 0..0.1..~square(0.1);
	f = 0..0.1..~x;      
	g = 0.2..0.3..~x;    
	h = 0.3..0.2..~-0.1; 
	
	j = 0.8..0.5..~-0.3;
	k = 0.5..0.8..~0.3; 
	l = 0.2..0.3..~0.0;
	m = 0.2..0.3..~1/2; // division
    return [x,a,b,f,g,h,j,k,l,m]; 
}
";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Object[] a = new Object[] { 0.000000, 0.500000, 1.000000, 1.500000, 2.000000 };
            Object[] b = new Object[] { 0.000000, 0.010000, 0.020000, 0.030000, 0.040000, 0.050000, 0.060000, 0.070000, 0.080000, 0.090000, 0.100000 };
            Object[] f = new Object[] { 0.000000, 0.100000 };
            Object[] g = new Object[] { 0.200000, 0.300000 };

            Object[] h = new Object[] { 0.300000, 0.200000 };
            Object[] j = new Object[] { 0.800000, 0.500000 };
            Object[] k = new Object[] { 0.500000, 0.800000 };
            Object l = null;
            Object[] m = new Object[] { 0.200000, 0.300000 };

            thisTest.Verify("x", 0.100000);
            thisTest.Verify("a", a);
            thisTest.Verify("b", b);
            thisTest.Verify("f", f);
            thisTest.Verify("g", g);
            thisTest.Verify("h", h);
            thisTest.Verify("j", j);
            thisTest.Verify("k", k);
            thisTest.Verify("l", l);
            thisTest.Verify("m", m);

        }

        [Test]
        public void T26_RangeExpression_Function_tilda_multilanguage_1457845_2()
        {
            // 1467121
            string code = @"
x=i[0];a=i[1];b=i[2];f=i[3];g=i[4];h=i[5];j=i[6];k=i[7];l=i[8];m=i[9];
def square : double ( x: double ) 
{
    return = x * x;
}
i = [Associative]
{
    return [Imperative]
    {
        x = 0.1; 
        a = 0..2..~0.5;
        b = 0..0.1..~square(0.1);
        f = 0..0.1..~x;      
        g = 0.2..0.3..~x;    
        h = 0.3..0.2..~-0.1; 
        
        j = 0.8..0.5..~-0.3;
        k = 0.5..0.8..~0.3; 
        l = 0.2..0.3..~0.0;
        m = 0.2..0.3..~1/2; // division 
        return [x, a, b, f, g, h, j, k, l, m];
    }
}
";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Object[] a = new Object[] { 0.000000, 0.500000, 1.000000, 1.500000, 2.000000 };
            Object[] b = new Object[] { 0.000000, 0.010000, 0.020000, 0.030000, 0.040000, 0.050000, 0.060000, 0.070000, 0.080000, 0.090000, 0.100000 };
            Object[] f = new Object[] { 0.000000, 0.100000 };
            Object[] g = new Object[] { 0.200000, 0.300000 };
            Object[] h = new Object[] { 0.300000, 0.200000 };
            Object[] j = new Object[] { 0.800000, 0.500000 };
            Object[] k = new Object[] { 0.500000, 0.800000 };
            Object l = null;
            Object[] m = new Object[] { 0.200000, 0.300000 };
            thisTest.Verify("x", 0.100000);
            thisTest.Verify("a", a);
            thisTest.Verify("b", b);
            thisTest.Verify("f", f);
            thisTest.Verify("g", g);
            thisTest.Verify("h", h);
            thisTest.Verify("j", j);
            thisTest.Verify("k", k);
            thisTest.Verify("l", l);
            thisTest.Verify("m", m);
        }

        [Test]
        public void T26_RangeExpression_Function_tilda_associative_1457845_3()
        {
            // 1467121
            string code = @"
x=i[0];a=i[1];b=i[2];f=i[3];g=i[4];h=i[5];j=i[6];k=i[7];l=i[8];m=i[9];
[Associative]
{
	def square : double ( x: double ) 
	{
		return = x * x;
	}
}
i=[Imperative]
{
	x = 0.1; 
	a = 0..2..~0.5;
	b = 0..0.1..~square(0.1);
	f = 0..0.1..~x;      
	g = 0.2..0.3..~x;    
	h = 0.3..0.2..~-0.1; 
	
	j = 0.8..0.5..~-0.3;
	k = 0.5..0.8..~0.3; 
	l = 0.2..0.3..~0.0;
	m = 0.2..0.3..~1/2; // division 
    return [x, a, b, f, g, h, j, k, l, m];
}
	
";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Object[] a = new Object[] { 0.000000, 0.500000, 1.000000, 1.500000, 2.000000 };
            Object[] b = null;
            Object[] f = new Object[] { 0.000000, 0.100000 };
            Object[] g = new Object[] { 0.200000, 0.300000 };
            Object[] h = new Object[] { 0.300000, 0.200000 };
            Object[] j = new Object[] { 0.800000, 0.500000 };
            Object[] k = new Object[] { 0.500000, 0.800000 };
            Object l = null;
            Object[] m = new Object[] { 0.200000, 0.300000 };
            thisTest.Verify("x", 0.100000);
            thisTest.Verify("a", a);
            thisTest.Verify("b", b);
            thisTest.Verify("f", f);
            thisTest.Verify("g", g);
            thisTest.Verify("h", h);
            thisTest.Verify("j", j);
            thisTest.Verify("k", k);
            thisTest.Verify("l", l);
            thisTest.Verify("m", m);
        }

        [Test]
        public void T27_RangeExpression_Function_Associative_1463472()
        {
            string code = @"
z1;
def twice : double( a : double )
{
    return = 2 * a;
}
[Associative]
{
	z1 = 1..twice(4)..twice(1);
}
";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Object[] a = new Object[] { 1.000000, 3.000000, 5.000000, 7.000000 };
            thisTest.Verify("z1", a);
        }

        [Test]
        public void T27_RangeExpression_Function_Associative_1463472_2()
        {

            string code = @"
c;
def twice : int []( a : double )
{
    c=1..a;
    return = c;
}
[Associative]
{
    d=1..4;
    c=twice(4);
}
";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Object[] c = new Object[] { 1, 2, 3, 4 };
            thisTest.Verify("c", c);
        }

        [Test]
        public void T27_RangeExpression_Function_return_1463472()
        {
            string code = @"
c;
def twice : int []( a : double )
{
    c=1..a;
    return = c;
}
[Associative]
{
d=1..4;
c=twice(4);
//	z1 = 1..twice(4)..twice(1);
}
";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Object[] c = new Object[] { 1, 2, 3, 4 };
            thisTest.Verify("c", c);
        }

        [Test]
        public void T27_RangeExpression_Function_Associative_replication()
        {
            string code = @"
z1;
def twice : int[]( a : int )
{
    c=2*(1..a);
    return = c;
}
[Associative]
{
    d=[1,2,3,4];
	z1=twice(d);
//	z1 = 1..twice(4)..twice(1);
}
";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Object[][] c = new Object[][] { new Object[] { 2 }, new Object[] { 2, 4 }, new Object[] { 2, 4, 6 }, new Object[] { 2, 4, 6, 8 } };
            thisTest.Verify("z1", c);
        }

        [Test]
        public void Regress_1467127()
        {
            string code = @"
i = 1..6..#10;";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            double step = 5.0 / 9.0;
            object[] values = new object[10];
            values[9] = 6.0;
            for (int i = 0; i < 9; ++i)
            {
                values[i] = 1.0 + step * i;
            }
            thisTest.Verify("i", values);
        }

        [Test]
        public void TA22_Range_Expression_floating_point_conversion_1467127()
        {
            string code = @"
a = 1..6..#10;
b = 0.1..0.6..#10;
c = 0.01..-0.6..#10;
d= -0.1..0.06..#10;
	";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Object[] a = new Object[] { 1.000000, 1.555556, 2.111111, 2.666667, 3.222222, 3.777778, 4.333333, 4.888889, 5.444444, 6.000000 };
            Object[] b = new Object[] { 0.100000, 0.155556, 0.211111, 0.266667, 0.322222, 0.377778, 0.433333, 0.488889, 0.544444, 0.600000 };
            Object[] c = new Object[] { 0.010000, -0.057778, -0.125556, -0.193333, -0.261111, -0.328889, -0.396667, -0.464444, -0.532222, -0.600000 };
            Object[] d = new Object[] { -0.100000, -0.082222, -0.064444, -0.046667, -0.028889, -0.011111, 0.006667, 0.024444, 0.042222, 0.060000 };
            thisTest.Verify("a", a);
            thisTest.Verify("b", b);
            thisTest.Verify("c", c);
            thisTest.Verify("d", d);
        }

        [Test]
        public void TA22_Range_Expression_floating_point_conversion_1467127_2()
        {
            string code = @"
a = 0..7..~0.75;
b = 0.1..0.7..~0.075;
c = 0.01..-7..~0.75;
d= -0.1..7..~0.75; 
e = 1..-7..~1;
	";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Object[] a = new Object[] { 0.000000, 0.777778, 1.555556, 2.333333, 3.111111, 3.888889, 4.666667, 5.444444, 6.222222, 7.000000 };
            Object[] b = new Object[] { 0.100000, 0.175000, 0.250000, 0.325000, 0.400000, 0.475000, 0.550000, 0.625000, 0.700000 };
            Object[] c = new Object[] { 0.010000 };
            Object[] d = new Object[] { -0.100000, 0.688889, 1.477778, 2.266667, 3.055556, 3.844444, 4.633333, 5.422222, 6.211111, 7.000000 };
            Object[] e = new Object[] { 1 };
            thisTest.Verify("a", a);
            thisTest.Verify("b", b);
            thisTest.Verify("c", c);
            thisTest.Verify("d", d);
            thisTest.Verify("e", e);
        }

        [Test]
        [Category("SmokeTest")]
        public void TA23_Defect_1466085_Update_In_Range_Expr()
        {
            string code = @"
y = 1;
y1 = 0..y;
y = 2;
z1 = y1; 
";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Object[] v = new Object[] { 0, 1, 2 };
            thisTest.Verify("z1", v);
        }

        [Test]
        [Category("SmokeTest")]
        public void TA23_Defect_1466085_Update_In_Range_Expr_2()
        {
            string code = @"
a = 0;
b = 10;
c = 2;
y1 = a..b..c;
a = 7;
b = 14;
c = 7;
";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);
            Object[] v = new Object[] { 7, 14 };
            thisTest.Verify("y1", v);
        }

        [Test]
        [Category("SmokeTest")]
        public void TA23_Defect_1466085_Update_In_Range_Expr_3()
        {
            string code = @"
def foo ( x : int[] )
{
    return = Count(x);
}
a = 0;
b = 10;
c = 2;
y1 = a..b..c;
z1 = foo ( y1 );
z2 = Count( y1 );
c = 5;
";
            ExecutionMirror mirror = thisTest.RunScriptSource(code);

            thisTest.Verify("z1", 3);
            thisTest.Verify("z2", 3);
        }

        [Test]
        [Category("DSDefinedClass_Ported")]
        [Category("SmokeTest")]
        public void IndexingIntoClassInstanceByRangeExpr()
        {
            string code = @"
import(""FFITarget.dll"");
x = (ClassFunctionality.ClassFunctionality(1..3))[0];
z = x.IntVal;
";
            thisTest.VerifyRunScriptSource(code, "DNL-1467618 Regression : Use of the array index after replicated constructor yields complier error now");
            thisTest.Verify("z", 1);
        }

        [Test]
        public void TA24_1467454_negative_case()
        {
            string code = @"
b = 10.0;
a = 0.0;
d1 = a..b..c;
d2 = c..b..c;
d3 = a..c..b;
d4 = c..a..c;
d5 = c..2*c..c;
";
            Object n1 = null;
            thisTest.VerifyRunScriptSource(code, "");
            thisTest.Verify("d1", n1);
            thisTest.Verify("d2", n1);
            thisTest.Verify("d3", n1);
            thisTest.Verify("d4", n1);
            thisTest.Verify("d5", n1);
            thisTest.VerifyBuildWarningCount(1);
        }

        [Test]
        public void TA24_1467454_negative_case_2()
        {
            string code = @"
b = 10.0;
a = 0.0;
d1;d2;d3;d4;d5;
[Imperative]
{
    d1 = a..b..c;
    d2 = c..b..c;
    d3 = a..c..b;
    d4 = c..a..c;
    d5 = c..2*c..c;
}
";
            Object n1 = null;
            thisTest.VerifyRunScriptSource(code, "");
            thisTest.Verify("d1", n1);
            thisTest.Verify("d2", n1);
            thisTest.Verify("d3", n1);
            thisTest.Verify("d4", n1);
            thisTest.Verify("d5", n1);
            thisTest.VerifyBuildWarningCount(1);
        }

        [Test]
        public void RegressMagn5111()
        {
            string code = @"x = 0..0..360/0;";
            thisTest.RunScriptSource(code);
            thisTest.Verify("x", null);
            thisTest.VerifyRuntimeWarningCount(1);
        }

        [Test]
        public void RegressMagn5111_02()
        {
            string code = @"x = 360/0..0..0;";
            thisTest.RunScriptSource(code);
            thisTest.Verify("x", null);
            thisTest.VerifyRuntimeWarningCount(1);
        }

        [Test]
        public void RegressMagn5111_03()
        {
            string code = @"x = 0..360/0..0;";
            thisTest.RunScriptSource(code);
            thisTest.Verify("x", null);
            thisTest.VerifyRuntimeWarningCount(1);
        }

        [Test]
        public void RangeExpression_Infinity()
        {
            // Crash when step range expression come to infinity.
            // http://adsk-oss.myjetbrains.com/youtrack/issue/MAGN-5111

            string code = @"x = 0..0..360/[0,0];";
            thisTest.RunScriptSource(code);
            thisTest.Verify("x", new object[] { null, null });
            thisTest.VerifyRuntimeWarningCount(2);
        }


        [Test]
        [Category("SmokeTest")]
        [Category("Failure")]
        public void TestStepZero()
        {
            string src = @"
a = 0;
b = 0..10..a;
";
            ExecutionMirror mirror = thisTest.RunScriptSource(src);
            TestFrameWork.VerifyRuntimeWarning(ProtoCore.Runtime.WarningID.InvalidArguments);
            thisTest.VerifyRuntimeWarningCount(1);
            thisTest.Verify("b", null);
        }

        [Test]
        [Category("SmokeTest")]
        public void AlphabetRangeImperative()
        {
            string src = @"
a1 = i[0];
a2 = i[1];
a3 = i[2];
a4 = i[3];
a5 = i[4];
a6 = i[5];
a7 = i[6];
a8 = i[7];
a9 = i[8];
a10 = i[9];
i = [Imperative]
{
	a1 = ""a""..""c""..1;
    a2 = ""A""..""E""..1;
    a3 = ""A""..""E""..2;
    a4 = ""a""..""e""..3;
    a5 = ""e""..""a""..1;
    a6 = ""z""..""v""..1;
    a7 = ""z""..""v""..4;
    a8 = ""a""..""z""..3;
    a9 = ""A""..""D"";
    a10 = ""o""..""q"";
    return [a1, a2, a3, a4, a5, a6, a7, a8, a9, a10];
}
";
            ExecutionMirror mirror = thisTest.RunScriptSource(src);
            List<Object> result = new List<Object> { "a", "b", "c" };
            thisTest.Verify("a1", result);

            result = new List<Object> { "A", "B", "C", "D", "E" };
            thisTest.Verify("a2", result);

            result = new List<Object> { "A", "C", "E" };
            thisTest.Verify("a3", result);

            result = new List<Object> { "a", "d" };
            thisTest.Verify("a4", result);

            result = new List<Object> { "e", "d", "c", "b", "a" };
            thisTest.Verify("a5", result);

            result = new List<Object> { "z", "y", "x", "w", "v" };
            thisTest.Verify("a6", result);

            result = new List<Object> { "z", "v" };
            thisTest.Verify("a7", result);

            result = new List<Object> { "a", "d", "g", "j", "m", "p", "s", "v", "y" };
            thisTest.Verify("a8", result);

            result = new List<Object> { "A", "B", "C", "D" };
            thisTest.Verify("a9", result);

            result = new List<Object> { "o", "p", "q" };
            thisTest.Verify("a10", result);
        }

        [Test, Category("SmokeTest")]
        public void AlphabetRangeNegativeTestCasesImperative()
        {
            string src = @"a1;a2;a3;a4;a5;a6;a7;
[Imperative]
{
	a1 = ""ab""..""cd""..1;
    a2 = ""c""..""a""..-1;
    a3 = ""a""..""&""..1;
    a4 = ""abc""..""def""..1;
    a5 = ""a""..""z""..-10;
    a6 = ""л""..""н""..1;
    a7 = ""л""..""н"";
}
";
            thisTest.RunScriptSource(src);

            thisTest.Verify("a1", null);
            thisTest.Verify("a2", null);
            thisTest.Verify("a3", null);
            thisTest.Verify("a4", null);
            thisTest.Verify("a5", null);
            thisTest.Verify("a6", null);
            thisTest.Verify("a7", null);

            thisTest.VerifyRuntimeWarningCount(7);
        }

        [Test]
        [Category("SmokeTest")]
        public void AlphabetRangeAssociative()
        {
            string src = @"
	a1 = ""a""..""c""..1;
    a2 = ""A""..""E""..1;
    a3 = ""A""..""E""..2;
    a4 = ""a""..""e""..3;
    a5 = ""e""..""a""..1;
    a6 = ""z""..""v""..1;
    a7 = ""z""..""v""..4;
    a8 = ""a""..""z""..3;
    a9 = ""A""..""D"";
    a10 = ""o""..""q"";";

            ExecutionMirror mirror = thisTest.RunScriptSource(src);
            List<Object> result = new List<Object> { "a", "b", "c" };
            thisTest.Verify("a1", result);

            result = new List<Object> { "A", "B", "C", "D", "E" };
            thisTest.Verify("a2", result);

            result = new List<Object> { "A", "C", "E" };
            thisTest.Verify("a3", result);

            result = new List<Object> { "a", "d" };
            thisTest.Verify("a4", result);

            result = new List<Object> { "e", "d", "c", "b", "a" };
            thisTest.Verify("a5", result);

            result = new List<Object> { "z", "y", "x", "w", "v" };
            thisTest.Verify("a6", result);

            result = new List<Object> { "z", "v" };
            thisTest.Verify("a7", result);

            result = new List<Object> { "a", "d", "g", "j", "m", "p", "s", "v", "y" };
            thisTest.Verify("a8", result);

            result = new List<Object> { "A", "B", "C", "D" };
            thisTest.Verify("a9", result);

            result = new List<Object> { "o", "p", "q" };
            thisTest.Verify("a10", result);
        }

        [Test, Category("SmokeTest")]
        public void AlphabetRangeNegativeTestCasesAssociative()
        {
            string src = @"
	a1 = ""ab""..""cd""..1;
    a2 = ""c""..""a""..-1;
    a3 = ""a""..""&""..1;
    a4 = ""abc""..""def""..1;
    a5 = ""a""..""z""..-10;
    a6 = ""л""..""н""..1;
    a7 = ""л""..""н"";";

            thisTest.RunScriptSource(src);

            thisTest.Verify("a1", null);
            thisTest.Verify("a2", null);
            thisTest.Verify("a3", null);
            thisTest.Verify("a4", null);
            thisTest.Verify("a5", null);
            thisTest.Verify("a6", null);
            thisTest.Verify("a7", null);

            thisTest.VerifyRuntimeWarningCount(7);
        }

        [Test]
        [Category("SmokeTest")]
        public void AlphabetSequenceImperative()
        {
            string src = @"
a1 = i[0];
a2 = i[1];
a3 = i[2];
a4 = i[3];
a5 = i[4];
a6 = i[5];
i = [Imperative]
{
	a1 = ""a""..#3..2;
    a2 = ""A""..#3..2;
    a3 = ""I""..#4..1;
    a4 = ""z""..#5..1;
    a5 = ""A""..#3..-1;
    a6 = ""z""..#3..-1;
    return [a1, a2, a3, a4, a5, a6];
}";
            ExecutionMirror mirror = thisTest.RunScriptSource(src);
            List<Object> result = new List<Object> { "a", "c", "e" };
            thisTest.Verify("a1", result);

            result = new List<Object> { "A", "C", "E" };
            thisTest.Verify("a2", result);

            result = new List<Object> { "I", "J", "K", "L" };
            thisTest.Verify("a3", result);

            result = new List<Object> { "z" };
            thisTest.Verify("a4", result);

            result = new List<Object> { "A" };
            thisTest.Verify("a5", result);

            result = new List<Object> { "z", "y", "x" };
            thisTest.Verify("a6", result);
        }

        [Test]
        [Category("SmokeTest")]
        public void AlphabetSequenceNegativeTestCasesImperative()
        {
            string src = @"
a1 = i[0];
a2 = i[1];
a3 = i[2];
i = [Imperative]
{
	a1 = ""л""..#3..2;    
    a2 = ""I""..#-5..1;
    a3 = ""z""..#0..1;
    return [a1, a2, a3];
}";
            thisTest.RunScriptSource(src);

            thisTest.Verify("a1", null);
            thisTest.Verify("a2", null);
            thisTest.Verify("a3", new List<Object>());

            TestFrameWork.VerifyRuntimeWarning(WarningID.InvalidArguments);
        }

        [Test]
        [Category("SmokeTest")]
        public void AlphabetSequenceAssociative()
        {
            string src = @"
	a1 = ""a""..#3..2;
    a2 = ""A""..#3..2;
    a3 = ""I""..#4..1;
    a4 = ""z""..#5..1;
    a5 = ""A""..#3..-1;
    a6 = ""z""..#3..-1;";
            ExecutionMirror mirror = thisTest.RunScriptSource(src);
            List<Object> result = new List<Object> { "a", "c", "e" };
            thisTest.Verify("a1", result);

            result = new List<Object> { "A", "C", "E" };
            thisTest.Verify("a2", result);

            result = new List<Object> { "I", "J", "K", "L" };
            thisTest.Verify("a3", result);

            result = new List<Object> { "z" };
            thisTest.Verify("a4", result);

            result = new List<Object> { "A" };
            thisTest.Verify("a5", result);

            result = new List<Object> { "z", "y", "x" };
            thisTest.Verify("a6", result);
        }

        [Test]
        [Category("SmokeTest")]
        public void AlphabetSequenceNegativeTestCasesAssociative()
        {
            string src = @"
	a1 = ""л""..#3..2;    
    a2 = ""I""..#-5..1;
    a3 = ""z""..#0..1;";
            thisTest.RunScriptSource(src);

            thisTest.Verify("a1", null);
            thisTest.Verify("a2", null);
            thisTest.Verify("a3", new List<Object>());

            thisTest.VerifyRuntimeWarningCount(2);
        }

        [Test]
        [Category("RegressionTests"), Category("FailureNET6")]
        public void TestRangeExpressionOverLimit01()
        {
            string src = @"x = 1..200000000;";
            thisTest.RunScriptSource(src);
            thisTest.VerifyRuntimeWarningCount(1);
        }

        [Test]
        [Category("RegressionTests")]
        public void TestRangeExpressionOverLimit02()
        {
            string src = @"x = 1..10000000000000000;";
            thisTest.RunScriptSource(src);
            thisTest.VerifyRuntimeWarningCount(1);
        }

        [Test]
        [Category("RegressionTests"), Category("FailureNET6")]
        public void TestRangeExpressionOverLimit03()
        {
            string src = @"x = 1..10..#200000000;";
            thisTest.RunScriptSource(src);
            thisTest.VerifyRuntimeWarningCount(1);
        }

        [Test]
        [Category("RegressionTests")]
        public void TestRangeExpressionOverLimit04()
        {
            string src = @"x = 1..10..0.00000000001;";
            thisTest.RunScriptSource(src);
            thisTest.VerifyRuntimeWarningCount(1);
        }
    }
}

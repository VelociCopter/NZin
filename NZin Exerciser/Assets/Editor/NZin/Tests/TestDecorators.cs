#pragma warning disable 1718, 219, 168

using System;
using System.Collections.Generic;
using NUnit.Framework;



/// <summary>
/// Testing out general decorator capabilities
/// For the packaged "Entity" decorator chain, see the TestEntities tests
/// </summary>
[TestFixture, Timeout(1000)]
public class TestDecorators {


    #region Tester classes
    class Base : Decoratable { // zzz kill
    }
    class TypeA : Decorator<Decoratable>, Decoratable {
		public TypeA() {
		}
        public TypeA( Decorator<Decoratable> toDecorate ) : base( toDecorate ) {
        }
    }
    class TypeB : Decorator<Decoratable>, Decoratable {
        public TypeB( Decorator<Decoratable> toDecorate ) : base( toDecorate ) {
        }
    }
    #endregion

    #region Helper functions
    Decorator<Decoratable> MakeDoubleDecorator() {
		Decorator<Decoratable> dec = new TypeA();
        dec = new TypeB( dec );
        return dec;
    }
    #endregion


    #region Tests
    [Test]
    public void Single() {
        Decorator<Decoratable> dec = new TypeA();

        Assert.That( dec.HasDecoration<TypeA>() );
    }


    [Test]
    public void Assign2() {
        var dec = MakeDoubleDecorator();

        Assert.That( dec.HasDecoration<TypeA>() );
        Assert.That( dec.HasDecoration<TypeB>() );
    }


    [Test]
    public void Remove() {
        var dec = MakeDoubleDecorator();
        var count = dec.DecoratorCount;
        NUnit.Framework.Assert.That( count == 2, "Was expecting 2 decorators. found "+count );

        dec.RemoveDecoration<TypeA>();

        count = dec.DecoratorCount;
        NUnit.Framework.Assert.That( count == 1, "Was expecting 1 decorator. found "+count );
        Assert.That( !dec.HasDecoration<TypeA>() );
        Assert.That( dec.HasDecoration<TypeB>() );
    }
    [Test]
    public void RemoveAtEnd() {
        var dec = MakeDoubleDecorator();

        dec = dec.RemoveDecoration<TypeB>();

        var count = dec.DecoratorCount;
        NUnit.Framework.Assert.That( count == 1, "Was expecting 1 decorator. found "+count );
        Assert.That( dec.HasDecoration<TypeA>() );
        Assert.That( !dec.HasDecoration<TypeB>() );
    }
    [Test]
    public void RemoveTwice() {
        var dec = MakeDoubleDecorator();

        dec = dec.RemoveDecoration<TypeB>();
        var count = dec.DecoratorCount;
        NUnit.Framework.Assert.That( count == 1, "Was expecting 1 decorator. found "+count );

        dec = new TypeB( dec );
        dec = dec.RemoveDecoration<TypeB>();
        count = dec.DecoratorCount;
        NUnit.Framework.Assert.That( count == 1, "Was expecting 1 decorator. found "+count );

        Assert.That( dec.HasDecoration<TypeA>() );
        Assert.That( !dec.HasDecoration<TypeB>() );
    }


    [Test]
    public void Empty() {
        Decorator<Decoratable> dec = new TypeA();
        TypeA decA = dec.Decoration<TypeA>();

        var removed = dec.RemoveDecoration<TypeA>();

        Assert.That( removed.Equals( decA ));
        Assert.That( dec.DecoratorCount == 0 );
    }


    [Test]
    public void RemoveInvalid() {
        Decorator<Decoratable> dec = new TypeA();

        var excepted = false;
        try {
            dec = dec.RemoveDecoration<TypeB>();
        } catch( KeyNotFoundException e ) {
            excepted = true;
        }

        Assert.That( excepted );
    }


    [Test]
    public void ReAdd() {
        var dec = MakeDoubleDecorator();

        dec.RemoveDecoration<TypeA>();
        dec = new TypeA( dec );

        Assert.That( dec.HasDecoration<TypeA>() );
        Assert.That( dec.HasDecoration<TypeB>() );
    }
    #endregion
}
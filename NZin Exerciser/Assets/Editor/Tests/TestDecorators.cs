#pragma warning disable 1718, 219, 168

using System;
using System.Collections.Generic;
using NUnit.Framework;



[TestFixture, Timeout(1000)]
public class TestDecorators {


    #region Test classes
    class Base : Decoratable {
    }
    class TypeA : Decorator<Base>, Decoratable {
        public TypeA( Base toDecorate ) : base( toDecorate ) {
        }
        public TypeA( Decorator<Base> toDecorate ) : base( toDecorate ) {
        }
    }
    class TypeB : Decorator<Base>, Decoratable {
        public TypeB( Decorator<Base> toDecorate ) : base( toDecorate ) {
        }
    }
    #endregion

    #region Helper functions
    Decorator<Base> MakeDoubleDecorator() {
        Decorator<Base> dec = new TypeA( new Base( ));
        dec = new TypeB( dec );
        return dec;
    }
    #endregion


    #region Tests
    [Test]
    public void Single() {
        Decorator<Base> dec = new TypeA( new Base( ));

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
        Decorator<Base> dec = new TypeA( new Base( ));

        dec = dec.RemoveDecoration<TypeA>();

        Assert.That( dec == null );
    }


    [Test]
    public void RemoveInvalid() {
        Decorator<Base> dec = new TypeA( new Base( ));

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
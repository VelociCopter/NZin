#pragma warning disable 1718, 219, 168

using System;
using System.Collections.Generic;
using NUnit.Framework;
using NZin;



/// <summary>
/// Testing out the packaged "Entity" decorator chain
/// </summary>
[TestFixture, Timeout(1000)]
public class TestEntities {


	#region Entity Tests
	[Test]
	public void DisparatePartsStillCompare() {
		Entity entity = new Entity();
		EdA a = new EdA( entity );
		EdB b = new EdB( a ); 

		Assert.That( a == b );
	}

	[Test]
	public void CanStillCheckIndividualEquivalence() {
		Entity entity = new Entity();
		EdA a = new EdA(entity);
		EdB b = new EdB(a);

		Assert.That(a.CId != b.CId);
	}

	[Test]
	public void PureDisposableGetsDisposeCall() {
		Disposable<Decoratable> pure = new Disposable<Decoratable>();

		bool didGetDisposedCall = false;
		Action<Disposable<Decoratable>> onDisposed = ( pb ) => {
			didGetDisposedCall = true;
		};
		pure.Disposed += onDisposed;

		Assert.That( pure.IsDisposed );
		Assert.That( didGetDisposedCall );
	}

	[Test]
	public void FactoryEntitiesHaveDisposableBehavior() {
		Entity entity = new Entity();
		var didGetCallback = false;
		Action<Entity> wrappedDisposedCallback = ( e ) => {
			didGetCallback = true;
		};

		entity.Dispose();

		Assert.That( entity.IsDisposed );
		Assert.That( didGetCallback );
	}
	#endregion


	#region Tester classes
	class EdA : Entity {
		public EdA( Entity a )
			:base( a ) {
		}
	}
	class EdB : Entity {
		public EdB( Entity b )
			:base( b ) {
		}
	}
	class PureBase : Decoratable {
	}
	#endregion

	#region Helper functions

	#endregion
}
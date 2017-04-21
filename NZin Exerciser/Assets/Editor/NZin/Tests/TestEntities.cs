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
		Entity a = new EdA();
		Entity b = new EdB( a );

		Assert.That( a == b );
	}

	[Test]
	public void CanStillCheckIndividualEquivalence() {
		EdA a = new EdA();
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

		pure.Dispose();

		Assert.That( pure.IsDisposed );
		Assert.That( didGetDisposedCall );
	}

	[Test]
	public void StandardEntitiesHaveDisposableBehavior() {
		Entity entity = new Entity();
		var didGetCallback = false;
		Action<Entity> wrappedDisposedCallback = ( e ) => {
			didGetCallback = true;
		};
		entity.Disposed += wrappedDisposedCallback;

		entity.Dispose();

		Assert.That( entity.IsDisposed );
		Assert.That( didGetCallback );
	}
	#endregion


	#region Tester classes
	class EdA : Entity {
	}
	class EdB : Entity {
		public EdB( Entity e )
			:base( e ) {
		}
	}
	#endregion

	#region Helper functions

	#endregion
}
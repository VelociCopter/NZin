#pragma warning disable 1718, 219, 168

using System;
using System.Collections.Generic;
using NUnit.Framework;



/// <summary>
/// Testing out the packaged "Entity" decorator chain
/// </summary>
[TestFixture, Timeout(1000)]
public class TestEntities {


	#region Tester classes
	class TypeA : Decorator<Decoratable>, Decoratable {
		public TypeA() {
		}
		public TypeA(Decorator<Decoratable> toDecorate) : base(toDecorate) {
		}
	}
	class TypeB : Decorator<Decoratable>, Decoratable {
		public TypeB(Decorator<Decoratable> toDecorate) : base(toDecorate) {
		}
	}
	#endregion

	#region Helper functions
	Decorator<Decoratable> MakeDoubleDecorator() {
		Decorator<Decoratable> dec = new TypeA();
		dec = new TypeB(dec);
		return dec;
	}
	#endregion

}
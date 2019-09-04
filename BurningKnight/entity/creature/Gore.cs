using System;
using BurningKnight.entity.component;
using BurningKnight.level;
using BurningKnight.physics;
using Lens.entity;
using Lens.graphics;
using Microsoft.Xna.Framework;

namespace BurningKnight.entity.creature {
	public class Gore : Entity, CollisionFilterEntity {
		public bool ShouldCollide(Entity entity) {
			return entity is Level || entity is DestroyableLevel || entity is Chasm;
		}

		private float vz;

		public override void AddComponents() {
			base.AddComponents();
			
			AddComponent(new ZComponent());
			AddComponent(new ShadowComponent(RenderShadow));

			vz = 1;
		}

		public override void Render() {
			Graphics.Color = ColorUtils.GrayColor;
			base.Render();
			Graphics.Color = ColorUtils.WhiteColor;
		}

		private void RenderShadow() {
			GraphicsComponent.Render(true);
		}

		private bool did;
		private bool zdid;

		public override void Update(float dt) {
			base.Update(dt);

			if (!zdid) {
				var z = GetComponent<ZComponent>();

				vz -= dt * 4;
				z.Z = Math.Max(z.Z + vz, 0);

				if (z.Z <= 0) {
					zdid = true;

					if (TryGetComponent<RectBodyComponent>(out var bd)) {
						bd.Body.LinearVelocity *= 0.5f;
					}
				}
			}

			if (did) {
				return;
			}

			var b = GetComponent<RectBodyComponent>().Body;

			if (b.LinearVelocity.Length() < 4) {
				b.LinearVelocity = Vector2.Zero;
				RemoveComponent<RectBodyComponent>();
				did = true;
			} else {
				Position += b.LinearVelocity * (dt * 1.5f);
			}
		}
	}
}
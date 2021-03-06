using BurningKnight.assets;
using BurningKnight.entity;
using BurningKnight.entity.component;
using BurningKnight.entity.creature;
using BurningKnight.entity.creature.drop;
using BurningKnight.entity.events;
using BurningKnight.entity.item;
using Lens.entity;
using Lens.util.file;
using Lens.util.math;
using Microsoft.Xna.Framework;

namespace BurningKnight.level.entities {
	public class Safe : SolidProp {
		private bool broken;
		
		public Safe() {
			Sprite = "safe";
		}

		protected override Rectangle GetCollider() {
			return new Rectangle(0, 6, 19, 17);
		}

		public override void AddComponents() {
			base.AddComponents();

			Width = 19;
			Height = 23;
			
			AddComponent(new HealthComponent {
				RenderInvt = true,
				InitMaxHealth = Rnd.Int(2, 5)
			});
			
			AddComponent(new ExplodableComponent());
			AddComponent(new ShadowComponent());

			var drops = new DropsComponent();
			AddComponent(drops);
			drops.Add("bk:safe");
		}

		public override void PostInit() {
			base.PostInit();
			
			if (GetComponent<HealthComponent>().HasNoHealth) {
				Break(false);
			}
		}

		public void Break(bool spawnLoot = true) {
			if (broken) {
				return;
			}

			broken = true;
			
			GetComponent<SliceComponent>().Sprite = CommonAse.Props.GetSlice("safe_broken");
			GetComponent<HealthComponent>().RenderInvt = false;

			if (spawnLoot) {
				GetComponent<DropsComponent>().SpawnDrops();
			}
		}

		public override bool HandleEvent(Event e) {
			if (!broken) {
				if (e is HealthModifiedEvent ev) {
					if (ev.Type != DamageType.Explosive) {
						ev.Amount = 0;

						return true;
					}

					ev.Amount = -1;
				} else if (e is DiedEvent) {
					Break();
					return true;
				}
			}

			return base.HandleEvent(e);
		}

		public override void Load(FileReader stream) {
			base.Load(stream);
			broken = stream.ReadBoolean();
		}

		public override void Save(FileWriter stream) {
			base.Save(stream);
			stream.WriteBoolean(broken);
		}
	}
}
using BurningKnight.entity.level.features;
using BurningKnight.entity.level.painters;
using BurningKnight.util;
using BurningKnight.util.geometry;

namespace BurningKnight.entity.level.rooms.regular {
	public class BrokeLineRoomDef : RegularRoomDef {
		public override void Paint(Level Level) {
			base.Paint(Level);
			var F = Terrain.RandomFloor();
			var Fl = Random.Chance(30) ? Terrain.WALL : (Random.Chance(50) ? Terrain.CHASM : Terrain.LAVA);
			Painter.Fill(Level, this, Terrain.WALL);

			if (Fl == Terrain.LAVA) {
				Painter.Fill(Level, this, 1, Terrain.RandomFloor());
				F = Random.Chance(40) ? Terrain.WATER : Terrain.DIRT;
			}

			Painter.Fill(Level, this, 1, F);
			Painter.Fill(Level, this, 2, Fl);
			var El = Random.Chance(50);

			if (El) {
				Painter.FillEllipse(Level, this, 3, Terrain.RandomFloor());
				Painter.FillEllipse(Level, this, 3, F);
			}
			else {
				Painter.Fill(Level, this, 3, Terrain.RandomFloor());
				Painter.Fill(Level, this, 3, F);
			}


			var S = false;

			if (Random.Chance(50)) {
				Painter.Set(Level, new Point(GetWidth() / 2 + Left, Top + 2), F);
				S = true;
			}

			if (Random.Chance(50)) {
				Painter.Set(Level, new Point(GetWidth() / 2 + Left, Bottom - 2), F);
				S = true;
			}

			if (Random.Chance(50)) {
				Painter.Set(Level, new Point(Left + 2, GetHeight() / 2 + Top), F);
				S = true;
			}

			if (Random.Chance(50) || !S) Painter.Set(Level, new Point(Right - 2, GetHeight() / 2 + Top), F);

			Fl = Random.Chance(30) ? Terrain.WALL : (Random.Chance(50) ? Terrain.CHASM : Terrain.LAVA);

			if (Random.Chance(50)) {
				if (El)
					Painter.FillEllipse(Level, this, 4, Fl);
				else
					Painter.Fill(Level, this, 4, Fl);


				if (Random.Chance(50)) {
					Fl = Random.Chance(30) ? Terrain.WALL : (Random.Chance(50) ? Terrain.CHASM : Terrain.LAVA);

					if (El)
						Painter.FillEllipse(Level, this, 5, Fl);
					else
						Painter.Fill(Level, this, 5, Fl);
				}
			}
			else if (Random.Chance(50)) {
				F = Terrain.RandomFloor();

				if (Fl == Terrain.LAVA) F = Terrain.DIRT;

				if (El) {
					Painter.FillEllipse(Level, this, 4, Terrain.RandomFloor());
					Painter.FillEllipse(Level, this, 4, F);
				}
				else {
					Painter.FillEllipse(Level, this, 4, Terrain.RandomFloor());
					Painter.Fill(Level, this, 4, F);
				}
			}

			if (Random.Chance(50)) {
				var Flr = Terrain.RandomFloor();

				if (Random.Chance(50)) {
					Painter.Fill(Level, new Rect(Left + 1, Top + 1, Left + 4, Top + 4), Flr);
					Painter.Fill(Level, new Rect(Right - 3, Top + 1, Right, Top + 4), Flr);
					Painter.Fill(Level, new Rect(Left + 1, Bottom - 3, Left + 4, Bottom), Flr);
					Painter.Fill(Level, new Rect(Right - 3, Bottom - 3, Right, Bottom), Flr);
				}
				else {
					Painter.FillEllipse(Level, new Rect(Left + 1, Top + 1, Left + 4, Top + 4), Flr);
					Painter.FillEllipse(Level, new Rect(Right - 3, Top + 1, Right, Top + 4), Flr);
					Painter.FillEllipse(Level, new Rect(Left + 1, Bottom - 3, Left + 4, Bottom), Flr);
					Painter.FillEllipse(Level, new Rect(Right - 3, Bottom - 3, Right, Bottom), Flr);
				}
			}

			foreach (LDoor Door in Connected.Values()) Door.SetType(LDoor.Type.REGULAR);
		}

		public override int GetMinWidth() {
			return 8;
		}

		public override int GetMinHeight() {
			return 8;
		}
	}
}
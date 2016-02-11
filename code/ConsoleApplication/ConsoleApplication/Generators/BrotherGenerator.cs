using System;
using System.Collections.Generic;
using System.Linq;
using libmusic;
using Midi;

namespace ConsoleApplication.Generators
{
	class BrotherGenerator : IMusicGenerator
	{
		private static Pitch[] Pitches;

		private static readonly Dictionary<Pitch, Pitch[]> Second = new Dictionary<Pitch, Pitch[]>()
		{
            {
                Pitch.A3, new[] {Pitch.F4, Pitch.C4, Pitch.D4, Pitch.E4, Pitch.A4}
            },
            {
                Pitch.B3, new[] {Pitch.D4, Pitch.F4, Pitch.G4, Pitch.B4}
            },
            {
				Pitch.C3, new[] {Pitch.F4, Pitch.A3, Pitch.G4, Pitch.E4, Pitch.C4}
			},
			{
				Pitch.D3, new[] {Pitch.F4, Pitch.A3, Pitch.G3, Pitch.B3, Pitch.D4}
			},
            {
                Pitch.E3, new[] {Pitch.B3, Pitch.C3, Pitch.G4, Pitch.A4, Pitch.E4}
            },
            {
				Pitch.F3, new[] {Pitch.A3, Pitch.C3, Pitch.D3, Pitch.F4}
			},
			{
				Pitch.G3, new[] {Pitch.C3, Pitch.D3, Pitch.B3, Pitch.E3, Pitch.G4}
			},

            // When using 'C' as a start note, the following notes are x distance from 'C'
            // A = -3, ASharp = -2, B = -1, C = +0, CSharp = +1, D = +2, DSharp = +3, E = +4, F = +5, FSharp = +6, G = +7, GSharp = +8
            // The notes used in a scale from the starting note are: -3, -1, +0, +2, +4, +5, +7
            // -5 and +7 are the same note with one octave diffance, +0, +12 and -12 are also the same note.
		};



		private InfoObject info;
		Random random = new Random();
		private float beat = 0;

		/// <summary>
		/// Setup method called by framework while also passing needed info in an object.
		/// 
		/// If Setup gets called multiple times it is an error and the IMusicGenerator may 
		/// throw an Exception or fail to function correctly
		/// </summary>
		/// <param name="infoObject">An object to interact with the framework</param>
		public void Setup(InfoObject infoObject)
		{
			Pitches = Second.Keys.ToArray();

			info = infoObject;
		}

		/// <summary>
		/// Deadline for next call to generatemusic. 
		/// </summary>
		/// <returns>Last time to call GenerateMusic</returns>
		public float Deadline()
		{
			return beat;
		}



		/// <summary>
		/// Generates Music
		/// </summary>
		/// <returns>A collection of music notes</returns>
		public ICollection<LibNoteMessage> GenerateMusic()
		{
			int remaining = 8;
			List<LibNoteMessage> messages = new List<LibNoteMessage>();

			Console.WriteLine("", beat/4);

			while (remaining > 0)
			{
				if (remaining != 8) Console.Write(" ");

				int[] sizes = new[] {1, 2, 3, 4, 8}.Where(x => x <= remaining).ToArray();

				int size = Math.Min(remaining, sizes[random.Next(sizes.Length)]);

				float lenght = size/2f;
				float ofset = (8 - remaining)/2f;

				int state = random.Next(Pitches.Length);
				messages.Add(new NoteOnOff(Pitches[state], 80, beat + ofset, lenght));

				Console.Write($"{$"({ofset}->{ofset + lenght})".PadRight(remaining == 8 ? 8 : 10)} {(Pitches[state]).ToString().Replace("Sharp", "#").PadRight(3)}".PadRight(14));

				Pitch[] second = Second[Pitches[state]];
				int index = random.Next(second.Length + 1);

				const int extra = 10;

				if (index >= second.Length)
				{
					Console.Write("".PadRight(extra));
                }
				else
				{
					messages.Add(new NoteOnOff(second[index], 80, beat + ofset, lenght));

					Console.Write($"->{second[index]}".Replace("Sharp", "#").PadRight(extra));
				}

				remaining -= size;
			}

			beat += 4;
			Console.WriteLine();


			
			return messages;
		}
	}
}

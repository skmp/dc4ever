using System;
using System.Collections.Generic;
using System.Text;

using Tao.Sdl;
using SdlDotNet;
using Tao.OpenGl;

namespace DC4Ever
{
	public static unsafe partial class emu
	{
		static uint* TAmem = (uint*)dc.mmgr.AllocMem(0x200);
		static uint TaRead(uint addr, int size)
		{
			return 0;
		}
		
		static void TaWrite(uint addr, uint data, int size)
		{
			if (size == 4)
				TAmem[(addr & 0x1ff)>>2] = data;
			return;
		}

		struct Ta_listheader
		{
			public uint listtype;
			public uint striplength;
			public uint clipmode;
			public uint modifier;
			public uint modifier_mode;
			public uint colour_type;
			public uint texture;
			public uint specular;
			public uint shading;
			public uint uv_format;
			public uint vertex_count;
		}
		static uint total_count = 0;
		static Ta_listheader curListheader = new Ta_listheader();

		static void RenderStart()
		{
			clc_pvr_renderdone = 1747627;//just give us soem time to actualy render the data
										 // it takes 1/120th of the second to render em .. lol
		}

		static void ProccessTaSQWrite(uint addr)
		{
			uint *data = &TAmem[addr & 0x1ff];
			uint cmd = (data[0] >> 29) & 0x7;
			switch (cmd)
			{
				case 0:
					WriteLine("TA - end of list," + total_count.ToString() + " vertexes");
					if (curListheader.vertex_count != 0)
					{
						WriteLine("TA Warning : Command 7 end missing");
						curListheader.vertex_count = 0;
						Gl.glEnd();
					}
					total_count = 0;
					//RenderStart();
					break;

				case 1:
					WriteLine("TA - USER_CLIP");
					break;

				case 2:
					WriteLine("TA - ??? 2");
					break;

				case 3:
					WriteLine("TA - ??? 3");
					break;

				case 4:
					//total_count=0;
					curListheader.listtype = (data[0] >> 24) & 0x7;
					curListheader.striplength = (data[0] >> 18) & 0x3;
					curListheader.clipmode = (data[0] >> 16) & 0x3;
					curListheader.modifier = (data[0] >> 7) & 0x1;
					curListheader.modifier_mode = (data[0] >> 6) & 0x1;
					curListheader.colour_type = (data[0] >> 4) & 0x3;
					curListheader.texture = (data[0] >> 3) & 0x1;
					curListheader.specular = (data[0] >> 2) & 0x1;
					curListheader.shading = (data[0] >> 1) & 0x1;
					curListheader.uv_format = data[0] & 0x1;
					curListheader.vertex_count = 0;
					//WriteLine("TA -Start POLYGON / MODIFIER_VOLUME:" + curListheader.texture.ToString());

					break;

				case 5:
					WriteLine("TA - SPRITE");
					//total_count = 0;
					break;

				case 6:
					WriteLine("TA - ??? 6");
					break;

				case 7:
					//WriteLine("TA - VERTEX");
					total_count++;
					if (curListheader.vertex_count==0)
						Gl.glBegin(Gl.GL_TRIANGLE_STRIP);

					if (curListheader.texture == 0)
					{
					
						#region not textured triagle strip
						float* pos = (float*)&data[1];
						//WriteLine("Vertex Data :" + pos[0].ToString() + ";" + pos[1].ToString() + ";" + pos[2].ToString());
						switch (curListheader.colour_type)
						{
							case 0://rgb
								byte* col_b = (byte*)&data[4];
								Gl.glColor4b(col_b[0], col_b[1], col_b[2], col_b[3]);
								break;

							case 1://float
								float* col_f = (float*)&data[4];
								Gl.glColor4f(col_f[0], col_f[1], col_f[2], col_f[3]);
								break;

							case 2://?

								break;
						}
						Gl.glVertex3f(pos[0], pos[1], pos[2] - 100);
						if ((data[0] & (1 << 28)) != 0)
						{
							Gl.glEnd();
							curListheader.vertex_count = 0;
						}
						else
							curListheader.vertex_count++;
						#endregion
					}
					else
					{
						#region textured triagle strip
						float* pos = (float*)&data[1];
						//WriteLine("Vertex Data :" + pos[0].ToString() + ";" + pos[1].ToString() + ";" + pos[2].ToString());
						switch (curListheader.colour_type)
						{
							case 0://rgb
								byte* col_b = (byte*)&data[4];
								Gl.glColor4b(col_b[0], col_b[1], col_b[2], col_b[3]);
								WriteLine("ARGB-b " + col_b[0].ToString() + " " + col_b[1].ToString() + " " + col_b[2].ToString() + " " + col_b[3].ToString() + " ");
								break;

							case 1://float
								float* col_f = (float*)&data[4];
								Gl.glColor4f(col_f[0], col_f[1], col_f[2], col_f[3]);
								WriteLine("ARGB-f " + col_f[0].ToString() + " " + col_f[1].ToString() + " " + col_f[2].ToString() + " " + col_f[3].ToString() + " ");
								break;

							case 2://?

								break;
						}
						Gl.glVertex3f(pos[0], pos[1], pos[2] - 100);
						if ((data[0] & (1 << 28)) != 0)
						{
							if (curListheader.vertex_count != 0)
							{
								Gl.glEnd();
								curListheader.vertex_count = 0;
							}
						}
						else
							curListheader.vertex_count++;
						#endregion

						WriteLine("Vertex Data :" + pos[0].ToString() + ";" + pos[1].ToString() + ";" + pos[2].ToString());
						WriteLine("Textures not suported");
						
					}

					break;
			}
		}

		static Surface screen = null;
		static void initOpenGL()
		{
			//Sdl.SDL_Init(Sdl.SDL_INIT_VIDEO);
			int width = 640;
			int height = 480;
			
			screen=Video.SetVideoModeWindowOpenGL(640, 480, true);
			Events.Poll();
			Gl.glMatrixMode(Gl.GL_MODELVIEW);
			Gl.glLoadIdentity();
			Gl.glViewport(0, 0, width, height);
			Gl.glClearColor(0.0f, 0.0f, 0.0f, 0.0f);		// This Will Clear The Background Color To Black
			Gl.glClearDepth(1.0);				// Enables Clearing Of The Depth Buffer
			Gl.glDepthFunc(Gl.GL_LESS);				// The Type Of Depth Test To Do
			Gl.glEnable(Gl.GL_DEPTH_TEST);			// Enables Depth Testing
			Gl.glShadeModel(Gl.GL_SMOOTH);			// Enables Smooth Color Shading

			Gl.glMatrixMode(Gl.GL_PROJECTION);
			Gl.glLoadIdentity();				// Reset The Projection Matrix
//					gluPerspective(45.0f,(GLfloat)width/(GLfloat)height,1.0f,100.0f);	// Calculate The Aspect Ratio Of The Window

			Gl.glMatrixMode(Gl.GL_MODELVIEW);
			Gl.glLoadIdentity();
			//		glOrtho(0, width, height, 0, 0.1, 100.0);
			Gl.glOrtho(0, width, height, 0, 0, 1024.0);
			Events.Poll();
		}
		public static void DoEvents()
		{
			System.Windows.Forms.Application.DoEvents();
			Events.Poll();
		}
	}

}

﻿/* Copyright (c) 2011 Nathanael Jones. See license.txt for your rights. */
using System;
using System.Collections.Generic;
using System.Text;
using ImageResizer.Resizing;
using System.Drawing.Imaging;
using System.Drawing;
using ImageResizer.Util;
using System.Globalization;

namespace ImageResizer.Plugins.SimpleFilters {
    public class SimpleFilters : BuilderExtension, IPlugin, IQuerystringPlugin {
        public IPlugin Install(Configuration.Config c) {
            c.Plugins.add_plugin(this);
            return this;
        }

        public bool Uninstall(Configuration.Config c) {
            c.Plugins.remove_plugin(this);
            return true;
        }

        public IEnumerable<string> GetSupportedQuerystringKeys() {
            return new string[] { "filter", "s.grayscale", "s.sepia", "s.alpha", "s.brightness", "s.contrast", "s.saturation", "s.invert" };
        }


        protected override RequestedAction PostCreateImageAttributes(ImageState s) {
            if (s.copyAttibutes == null) return RequestedAction.None;

            if (!s.settings.WasOneSpecified((string[])GetSupportedQuerystringKeys())) return RequestedAction.None;

            List<float[][]> filters = new List<float[][]>();

            string filter = s.settings["filter"];
            if (!string.IsNullOrEmpty(filter)) {
                int valuesStart = filter.IndexOf('(');
                string valStr = null;
                double[] values = null;
                if (valuesStart > -1) {
                    valStr = filter.Substring(valuesStart);
                    filter = filter.Substring(0, valuesStart);
                    values = Util.Utils.parseList(valStr, 0);
                }

                if ("grayscale".Equals(filter, StringComparison.OrdinalIgnoreCase)) filters.Add(GrayscaleFlat());
                if ("sepia".Equals(filter, StringComparison.OrdinalIgnoreCase)) filters.Add(Sepia());
                if (values != null && values.Length == 1) {
                    if ("alpha".Equals(filter, StringComparison.OrdinalIgnoreCase)) filters.Add(Alpha((float)values[0]));
                    if ("brightness".Equals(filter, StringComparison.OrdinalIgnoreCase)) filters.Add(Brightness((float)values[0]));
                }
            }
            if ("true".Equals(s.settings["s.grayscale"], StringComparison.OrdinalIgnoreCase)) filters.Add(GrayscaleNTSC());
            if ("flat".Equals(s.settings["s.grayscale"], StringComparison.OrdinalIgnoreCase)) filters.Add(GrayscaleFlat());
            if ("y".Equals(s.settings["s.grayscale"], StringComparison.OrdinalIgnoreCase)) filters.Add(GrayscaleY());
            if ("ry".Equals(s.settings["s.grayscale"], StringComparison.OrdinalIgnoreCase)) filters.Add(GrayscaleRY());
            if ("ntsc".Equals(s.settings["s.grayscale"], StringComparison.OrdinalIgnoreCase)) filters.Add(GrayscaleNTSC());
            if ("bt709".Equals(s.settings["s.grayscale"], StringComparison.OrdinalIgnoreCase)) filters.Add(GrayscaleBT709());

            if ("true".Equals(s.settings["s.sepia"], StringComparison.OrdinalIgnoreCase)) filters.Add(Sepia());
            if ("true".Equals(s.settings["s.invert"], StringComparison.OrdinalIgnoreCase)) filters.Add(Invert());

            string alpha = s.settings["s.alpha"];
            string brightness = s.settings["s.brightness"];
            string contrast = s.settings["s.contrast"];
            string saturation = s.settings["s.saturation"];

            double temp = 0;
            if (!string.IsNullOrEmpty(alpha) && double.TryParse(alpha, Utils.floatingPointStyle,NumberFormatInfo.InvariantInfo,out temp)) filters.Add(Alpha((float)temp));
            if (!string.IsNullOrEmpty(brightness) && double.TryParse(brightness, Utils.floatingPointStyle, NumberFormatInfo.InvariantInfo, out temp)) filters.Add(Brightness((float)temp));
            if (!string.IsNullOrEmpty(contrast) && double.TryParse(contrast, Utils.floatingPointStyle, NumberFormatInfo.InvariantInfo, out temp)) filters.Add(Contrast((float)temp));
            if (!string.IsNullOrEmpty(saturation) && double.TryParse(saturation, Utils.floatingPointStyle, NumberFormatInfo.InvariantInfo, out temp)) filters.Add(Saturation((float)temp));


            if (filters.Count == 0) return RequestedAction.None;
            if (filters.Count == 1) s.copyAttibutes.SetColorMatrix(new ColorMatrix(filters[0]));
            else {
                //Multiple all the filters
                float[][] first = filters[0];

                for (int i = 1; i < filters.Count; i++) {
                    first = Multiply(first, filters[i]);
                }
                s.copyAttibutes.SetColorMatrix(new ColorMatrix(first));

            }


            return RequestedAction.None;
        }

        private static float[][] Multiply(float[][] f1, float[][] f2) {
            float[][] X = new float[5][];
            for (int d = 0; d < 5; d++)
                X[d] = new float[5];
            int size = 5;
            float[] column = new float[5];
            for (int j = 0; j < 5; j++) {
                for (int k = 0; k < 5; k++) {
                    column[k] = f1[k][j];
                }
                for (int i = 0; i < 5; i++) {
                    float[] row = f2[i];
                    float s = 0;
                    for (int k = 0; k < size; k++) {
                        s += row[k] * column[k];
                    }
                    X[i][j] = s;
                }
            }
            return X;
        }

        static float[][] Sepia() {
            //from http://www.techrepublic.com/blog/howdoi/how-do-i-convert-images-to-grayscale-and-sepia-tone-using-c/120
            return (new float[][]{   
                    new float[] {0.393f,0.349f,0.272f, 0, 0},
                    new float[] {0.769f,0.686f,0.534f, 0, 0},
                    new float[] {0.189f, 0.168f,0.131f, 0, 0},
                    new float[] {     0,      0,      0, 1, 0},
                    new float[] {     0,      0,      0, 0, 0}});

        }

        /*Y = 0.299 * R + 0.587 * G + 0.114 * B
I = 0.596 * R - 0.274 * G - 0.322 * B
Q = 0.212 * R - 0.523 * G + 0.311 * B
         */
        /// <summary>
        /// Not yet implemented
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        static float[][] Grayscale(Color c) {
            float r = (float)c.R / 255.0F;
            float g = (float)c.G / 255.0F;
            float b = (float)c.B / 255.0F;
            //Could do a grayscale filter on a color
            return Grayscale(r, g, b);

        }
        static float[][] Grayscale(float r, float g, float b) {
            return (new float[][]{   
                                  new float[]{r,r,r,0,0},
                                  new float[]{g,g,g,0,0},
                                  new float[]{b,b,b,0,0},
                                  new float[]{0,0,0,1,0},
                                  new float[]{0,0,0,0,1}});

        }

        static float[][] GrayscaleFlat() {
            return Grayscale(0.5f, 0.5f, 0.5f);
        }

        static float[][] GrayscaleBT709() {
            return Grayscale(0.2125f, 0.7154f, 0.0721f);
        }
        static float[][] GrayscaleRY() {
            return Grayscale(0.5f, 0.419f, 0.081f);
        }
        static float[][] GrayscaleY() {
            return Grayscale(0.229f, 0.587f, 0.114f);
        }

        static float[][] GrayscaleNTSC() {
            return GrayscaleY();
        }

        static float[][] Invert() {
            return (new float[][]{   
                                    new float[]{-1,0,0,0,0},
                                  new float[]{0,-1,0,0,0},
                                  new float[]{0,0,-1,0,0},
                                  new float[]{0,0,0,1,0},
                                  new float[]{1,1,1,0,1}});

        }






        static float[][] Alpha(float alpha) {
            //http://www.codeproject.com/KB/GDI-plus/CsTranspTutorial2.aspx
            return (new float[][]{
                                  new float[]{1,0,0,0,0},
                                  new float[]{0,1,0,0,0},
                                  new float[]{0,0,1,0,0},
                                  new float[]{0,0,0,alpha,0},
                                  new float[]{0,0,0,0,1}});
        }
        static float[][] Contrast(float c) {


            c++; //Stop at -1

            float factorT = 0.5f * (1.0f - c); 

            return (new float[][]{
                                  new float[]{c,0,0,0,0},
                                  new float[]{0,c,0,0,0},
                                  new float[]{0,0,c,0,0},
                                  new float[]{0,0,0,1,0},
                                  new float[]{factorT,factorT,factorT,0,1}});
        }

        static float[][] Brightness(float factor) {
            return new float[][]{
                                  new float[]{1,0,0,0,0},
                                  new float[]{0,1,0,0,0},
                                  new float[]{0,0,1,0,0},
                                  new float[]{0,0,0,1,0},
                                  new float[]{factor,factor,factor,0,1}};

        }
        /// <summary>
        /// Saturation is between -1 and infinity
        /// </summary>
        /// <param name="saturation"></param>
        /// <returns></returns>
        static float[][] Saturation(float saturation) {
        http://www.bobpowell.net/imagesaturation.htm

            saturation = Math.Max(0, saturation + 1); //Stop at -1

            //saturation = Math.Max(Math.Min(saturation, 0), 1);

            float SaturationComplement = 1.0f - saturation;
            float SaturationComplementR = 0.3086f * SaturationComplement;
            float SaturationComplementG = 0.6094f * SaturationComplement;
            float SaturationComplementB = 0.0820f * SaturationComplement;
            return new float[][]{
                               new float[]{SaturationComplementR + saturation,  SaturationComplementR,  SaturationComplementR,  0.0f, 0.0f},
                               new float[]{SaturationComplementG,  SaturationComplementG + saturation,  SaturationComplementG,  0.0f, 0.0f},
                               new float[]{SaturationComplementB,  SaturationComplementB,  SaturationComplementB + saturation,  0.0f, 0.0f},
                               new float[]{0.0f,  0.0f,  0.0f,  1.0f,  0.0f},
                               new float[]{0.0f,  0.0f,  0.0f,  0.0f,  1.0f}};

        }



    }
}
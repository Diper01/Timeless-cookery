using UnityEngine;
using System.Collections.Generic;
using TMPro; // Убедитесь, что у вас есть библиотека RTLTextMeshPro

[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/RTLUILabel")]
public class RTLUILabel : UILabel
{
    // Новый текстовый атрибут для RTL текста
   /* private string rtlText;

    // Переопределение текста
    public override string text
    {
        get { return mText; }
        set
        {
            if (mText == value) return;

            mText = value;
            // Обработка текста для RTL
            rtlText = ProcessRTLText(mText);
            MarkAsChanged();
            ProcessAndRequest();

            if (autoResizeBoxCollider) ResizeCollider();
        }
    }

    // Метод для обработки текста с RTL
    private string ProcessRTLText(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        // Пример обработки текста справа налево. Это можно заменить реальной реализацией.
        char[] charArray = input.ToCharArray();
        System.Array.Reverse(charArray);
        return new string(charArray);
    }

    // Переопределение метода OnFill для использования rtlText
    public override void OnFill(List<Vector3> verts, List<Vector2> uvs, List<Color> cols)
    {
        if (!isValid) return;

        int offset = verts.Count;
        Color col = color;
        col.a = finalAlpha;

        if (premultipliedAlphaShader) col = NGUITools.ApplyPMA(col);

        string text = processedText;
        if (!string.IsNullOrEmpty(rtlText)) text = rtlText;

        int start = verts.Count;

        UpdateNGUIText();

        NGUIText.tint = col;
        NGUIText.Print(text, verts, uvs, cols);
        NGUIText.bitmapFont = null;
        NGUIText.dynamicFont = null;

        // Center the content within the label vertically
        Vector2 pos = ApplyOffset(verts, start);

        // Effects don't work with packed fonts
        if (packedFontShader) return;

        // Apply an effect if one was requested
        if (effectStyle != Effect.None)
        {
            int end = verts.Count;
            pos.x = mEffectDistance.x;
            pos.y = mEffectDistance.y;

            ApplyShadow(verts, uvs, cols, offset, end, pos.x, -pos.y);

            if ((effectStyle == Effect.Outline) || (effectStyle == Effect.Outline8))
            {
                offset = end;
                end = verts.Count;

                ApplyShadow(verts, uvs, cols, offset, end, -pos.x, pos.y);

                offset = end;
                end = verts.Count;

                ApplyShadow(verts, uvs, cols, offset, end, pos.x, pos.y);

                offset = end;
                end = verts.Count;

                ApplyShadow(verts, uvs, cols, offset, end, -pos.x, -pos.y);

                if (effectStyle == Effect.Outline8)
                {
                    offset = end;
                    end = verts.Count;

                    ApplyShadow(verts, uvs, cols, offset, end, -pos.x, 0);

                    offset = end;
                    end = verts.Count;

                    ApplyShadow(verts, uvs, cols, offset, end, pos.x, 0);

                    offset = end;
                    end = verts.Count;

                    ApplyShadow(verts, uvs, cols, offset, end, 0, pos.y);

                    offset = end;
                    end = verts.Count;

                    ApplyShadow(verts, uvs, cols, offset, end, 0, -pos.y);
                }
            }
        }

        if (NGUIText.symbolStyle == NGUIText.SymbolStyle.NoOutline)
            for (int i = 0, imax = cols.Count; i < imax; i++)
                if (cols[i].r == -1f) cols[i] = Color.white;

        if (onPostFill != null)
            onPostFill(this, offset, verts, uvs, cols);
    }*/
}

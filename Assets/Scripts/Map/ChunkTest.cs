using UnityEngine;

public class ChunkTest : MonoBehaviour
{
    public Sprite Sprite;
    public Chunk Chunk;
    public MeshTexture Texture;

    public void Start()
    {
        Chunk.Create(0, 0, 16, 16, 0);
        Chunk.DoneLoading();

        for (int x = 0; x < Chunk.Width; x++)
        {
            for (int y = 0; y < Chunk.Height; y++)
            {
                Texture.SetTile(Sprite, x, y);
            }
        }
    }
}
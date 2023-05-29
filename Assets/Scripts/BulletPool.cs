using Services.Factory;

public class BulletPool
{
    private readonly Bullet[] _bullets = new Bullet[Constants.CountSpawnBullets];
    
    public BulletPool(IGameFactory factory)
    {
        for (int i = 0; i < _bullets.Length; i++)
        {
            _bullets[i] = factory.CreateBullet();
            _bullets[i].gameObject.SetActive(false);
        }
    }

    public Bullet[] GetBullets() =>
        _bullets;
}
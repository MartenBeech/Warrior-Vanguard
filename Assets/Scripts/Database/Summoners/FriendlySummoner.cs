public static class FriendlySummoner {
    public static int maxHealth = 42;
    public static int currentHealth = 42;

    public static void GainHealth(int health) {
        currentHealth += health;
        if (currentHealth > maxHealth) {
            currentHealth = maxHealth;
        }
    }
    
    public static void LoseHealth(int health) {
        currentHealth -= health;
        if (currentHealth < 0) {
            LevelManager.isAlive = false;
            SceneLoader.LoadGameOver();
        }
    }

    public static void GainMaxHealth(int health) {
        maxHealth += health;
    }

    public static void LoseMaxHealth(int health) {
        maxHealth -= health;
    }
}
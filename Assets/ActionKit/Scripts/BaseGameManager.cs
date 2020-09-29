using UnityEngine;
using System.Collections;
using InaneGames;

public class BaseGameManager : MonoBehaviour {
	private static int K_ENEMIES = 0;
	public delegate void OnPickUpPowerup(BasePowerup pow);
	public static event OnPickUpPowerup onPickUpPowerup;
	public static void pickUpPowerup(BasePowerup pow)
	{
		if(onPickUpPowerup!=null)
		{
			onPickUpPowerup(pow);	
		}
	}
	public delegate void OnEnemiesCleared();
	public static event OnEnemiesCleared onEnemiesCleared;
	public static void enemiesCleared()
	{
		if(onEnemiesCleared!=null)
		{
			onEnemiesCleared();	
		}
	}
	public delegate void OnFloatText(string str, GameObject obj,Color color);
	public static event OnFloatText onFloatText;
	public static void floatText(string str, GameObject obj,Color color)
	{
		if(onFloatText!=null)
		{
			onFloatText(str,obj,color);	
		}
	}
	public delegate void OnSuckedIntoPortal(GameObject go);
	public static event OnSuckedIntoPortal onSuckedIntoPortal;
	public static void suckedIntoPortal(GameObject go)
	{
		if(onSuckedIntoPortal!=null)
		{
			onSuckedIntoPortal(go);	
		}
	}

	public delegate void OnEnemyHit(GameObject go);
	public static event OnEnemyHit onEnemyHit;
	public static void enemyHit(GameObject go)
	{
		if(onEnemyHit!=null)
		{
			onEnemyHit(go);	
		}
	}
	public delegate void OnUpgrade(int level);
	public static event OnUpgrade onUpgrade;
	public static void upgradeLevel(int level)
	{
		if(onUpgrade!=null)
		{
			onUpgrade(level);	
		}
	}

	public delegate void OnSetNomRounds(int rounds);
	public static event OnSetNomRounds onSetNomRounds;
	public static void setNomRounds(int rounds)
	{
		if(onSetNomRounds!=null)
		{
			onSetNomRounds(rounds);	
		}
	}
	public delegate void OnUseButton(UnityEngine.UI.Button button);
	public static event OnUseButton onUseButton;
	public static void useButton(UnityEngine.UI.Button button)
	{
		if(onUseButton!=null)
		{
			onUseButton(button);	
		}
	}


	public delegate void OnEnterExploreRoom();
	public static event OnEnterExploreRoom onEnterExploreRoom;
	public static void enterExploreRoom()
	{
		if(onEnterExploreRoom!=null)
		{
			onEnterExploreRoom();	
		}
	}

	public delegate void OnEnterBattleRoom();
	public static event OnEnterBattleRoom onEnterBattleRoom;
	public static void enterBattleRoom()
	{
		if(onEnterBattleRoom!=null)
		{
			onEnterBattleRoom();	
		}
	}

	public delegate void OnNextRound(int round);
	public static event OnNextRound onNextRound;
	public static void nextRound(int round)
	{
		if(onNextRound!=null)
		{
			onNextRound(round);	
		}
	}

	public delegate void OnAddEnemy();
	public static event OnAddEnemy onAddEnemy;
	
	public delegate void OnRemoveEnemy();
	public static event OnRemoveEnemy onRemoveEnemy;

	public static void setNomEnemies(int nomEnemies)	
	{
		K_ENEMIES = nomEnemies;
		K_NOM_KILLED=0;
	}


	private static int K_NOM_KILLED=0;
	private static int K_NOM_TO_KILL = 0;

	public static void addEnemyToKill()
	{
		K_NOM_TO_KILL++;
	}
	public static void setNomEnemiesToKill(int nomToKill)
	{
		K_NOM_KILLED=0;
		K_NOM_TO_KILL = nomToKill;
	}

	public static int getNomKilled()
	{
		return K_NOM_KILLED;
	}
	public static int getNomToKill()
	{
		return K_NOM_TO_KILL;
	}

	public static int addEnemy()
	{
		K_ENEMIES++;
		if(onAddEnemy!=null)
		{
			onAddEnemy();
		}
		return K_ENEMIES;
	}
	public static void removeEnemy()
	{
		K_ENEMIES--;
		K_NOM_KILLED++;

		if(onRemoveEnemy!=null)
		{
			onRemoveEnemy();
		}
	}
	public static int getNomEnemies()
	{
		return K_ENEMIES;
	}


	public delegate void OnGameStart();
	public static event OnGameStart onGameStart;
	public static void startGame()
	{
		if(onGameStart!=null)
		{
			onGameStart();	
		}
	}
	
	public delegate void OnGamePause(bool pause);
	public static event OnGamePause onGamePause;
	public static void pauseGame(bool pause)
	{
		if(onGamePause!=null)
		{
			onGamePause(pause);	
		}
	}
	public delegate void OnGameOver(bool victory);
	public static event OnGameOver onGameOver;
	public static void gameover(bool victory)
	{
		if(onGameOver!=null)
		{
			onGameOver(victory);	
		}
	}
	public delegate void OnPlayerDie();
	public static event OnPlayerDie onPlayerDie;
	public static void playerDie()
	{
		if(onPlayerDie!=null)
		{
			onPlayerDie();	
		}
	}
	public delegate void OnEnterBossRoom();
	public static event OnEnterBossRoom onEnterBossRoom;
	public static void enterBossRoom()
	{
		if(onEnterBossRoom!=null)
		{
			onEnterBossRoom	();	
		}
	}

	public delegate void LoadNextLevel();
	public static event LoadNextLevel onLoadNextLevel;
	public static void loadNextLevel()
	{
		if(onLoadNextLevel!=null)
		{
			onLoadNextLevel();	
		}
	}
	public delegate void OnPlayerLand();
	public static event OnPlayerLand onPlayerLand;
	public static void playerLand()
	{
		if(onPlayerLand!=null)
		{
			onPlayerLand();	
		}
	}
	
	public delegate void OnPlayerJump();
	public static event OnPlayerJump onPlayerJump;
	public static void playerJump()
	{
		if(onPlayerJump!=null)
		{
			onPlayerJump();	
		}
	}

	public delegate void OnPlayerFireGun(Weapon weapon);
	public static event OnPlayerFireGun onPlayerFireGun;
	public static void playerFireGun(Weapon weapon)
	{
		if(onPlayerFireGun!=null)
		{
			onPlayerFireGun(weapon);	
		}
	}
	public delegate void OnPlayerHit(float normalizedHealth);
	public static event OnPlayerHit onPlayerHit;
	public static void playerHit(float normalizedHealth)
	{
		if(onPlayerHit!=null)
		{
			onPlayerHit(normalizedHealth);	
		}
	}

	public delegate void OnDamagableDestroy(GameObject go);
	public static event OnDamagableDestroy onDamagableDestroy;
	public static void damagableDestroy(GameObject go)
	{
		if(onDamagableDestroy!=null)
		{
			onDamagableDestroy(go);	
		}
	}

	public delegate void OnEnemyDie(GameObject go);
	public static event OnEnemyDie onEnemyDie;
	public static void enemeyDie(GameObject go)
	{
		//removeEnemy();
		if(onEnemyDie!=null)
		{
			onEnemyDie(go);	
		}
	}	
	


	public delegate void OnSetActiveGameObject(GameObject go);
	public static event OnSetActiveGameObject onSetActiveGameObject;
	public static void setActiveGameObject(GameObject go)
	{
		if(onSetActiveGameObject!=null)
		{
			onSetActiveGameObject(go);	
		}
	}	
	public delegate void OnEnemySpawn();
	public static event OnEnemySpawn onEnemySpawn;
	public static void enemySpawn()
	{
		addEnemy();
		if(onEnemySpawn!=null)
		{
			onEnemySpawn();	
		}
	}	

}

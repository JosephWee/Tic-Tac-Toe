#!/usr/bin/env python
# coding: utf-8

# In[1]:


import numpy as np
import pandas as pd

dfGames = pd.read_csv('TicTacToeGames20230428.csv')
dfData = pd.read_csv('TicTacToeData20230428.csv')

print(dfGames.head())
print(dfData.head())


# In[2]:


grpGames = pd.DataFrame(dfGames.groupby(by=['Description','Status'])['InstanceId'].count())
grpGames.reset_index(inplace=True)
grpGames.rename(columns={"InstanceId": "Count"}, inplace=True)
print(type(grpGames))
print(grpGames)
print(grpGames.index)
print(grpGames.columns)
print(grpGames.shape)
#print()
#print(grpGames["Status"])
print()
print(list(grpGames["Description"].unique()))


# In[3]:


gameSetups = []
gameSetups.append('UnitTest 1P Valid Test - ComputerPlayerV1')
gameSetups.append('UnitTest 1P Valid Test - ComputerPlayerV2')
gameSetups.append('UnitTest 1P Valid Test - ComputerPlayerV3')
gameSetups.append('UnitTest 2P Valid Test - P1: ComputerPlayerV1 P2: ComputerPlayerV1')
gameSetups.append('UnitTest 2P Valid Test - P1: ComputerPlayerV1 P2: ComputerPlayerV2')
gameSetups.append('UnitTest 2P Valid Test - P1: ComputerPlayerV2 P2: ComputerPlayerV1')
gameSetups.append('UnitTest 2P Valid Test - P1: ComputerPlayerV2 P2: ComputerPlayerV2')
gameSetups.append('UnitTest 2P Valid Test - P1: ComputerPlayerV2 P2: ComputerPlayerV3')
gameSetups.append('UnitTest 2P Valid Test - P1: ComputerPlayerV3 P2: ComputerPlayerV2')
gameSetups.append('UnitTest 2P Valid Test - P1: ComputerPlayerV3 P2: ComputerPlayerV3')
#gameSetups.append('Web Api - ComputerPlayerV3')

dfGames = grpGames[grpGames["Description"].apply(lambda d: d in gameSetups)]
print(dfGames)
print()
print()

newGameSetup = []
newStatus = []
newCount = []
for i in range(len(gameSetups)):
    gameSetup = gameSetups[i]
    for status in range(1,4):
        dfResult = dfGames[(dfGames["Description"].str.fullmatch(gameSetup)) & (dfGames["Status"] == status)]
        if dfResult.shape[0] == 0:
            print(f"{gameSetup}:{status}:{0}")
            newGameSetup.append(gameSetup)
            newStatus.append(status)
            newCount.append(0)

#print(newGameSetup)
#print(newStatus)
#print(newCount)

newRows = pd.DataFrame(data={"Description": newGameSetup, "Status": newStatus, "Count": newCount}, index=range(len(newGameSetup)))
#print(newRows)
dfGames = pd.concat([dfGames, newRows])
dfGames.sort_values(by=["Description","Status"], inplace=True)
dfGames.reset_index(drop=True,inplace=True)
print(dfGames)


# In[5]:


import matplotlib.pyplot as plt

xpos = [1,2,3]
for i in range(len(gameSetups)):
    gameSetup = gameSetups[i]
    dfColumnChart = dfGames[dfGames["Description"].str.fullmatch(gameSetup)]
    #print(dfColumnChart)
    count = dfColumnChart["Count"].values
    countList = [count[0],count[2],count[1]]
    plt.bar(x=xpos,height=countList)
    plt.xticks(ticks=xpos,labels=["Player1","Draw","Player2"])
    plt.title(gameSetup)
    plt.show()
    plt.savefig('TicTacToeGameOutcomeByGameSetup' + gameSetup.replace(' ','_').replace('UnitTest_','') + '.png')




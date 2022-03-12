
# for i in range(60):
# 	
# 	elemento = i % 2 
# 
# 	print (str(i) + "    " + str(elemento))
# 
# 
# elemento = list()
# 
# for k in range(200):
# 	elemento.append(1)
# 
# 
# print(len(elemento))
# 



f = open("D:\\INTERNET\\Wiki-Andresito-07-WORK\\RunningImages2\\Assets\\TryWriteThree\\readingFile.txt", "r")



lista0 = list()
lista1 = list()
lista2 = list()
lista3 = list()

for i in range(2728 * 4):
	
	if(i < 2728):
		lista0.append(f.readline().split("\n")[0])

	elif(i < 2728 * 2):
		lista1.append(f.readline().split("\n")[0])
	
	elif(i < 2728 * 3):
		lista2.append(f.readline().split("\n")[0])

	elif(i < 2728 * 4):
		lista3.append(f.readline().split("\n")[0])


f.close()


for i in range(len(lista0)):
	print(str(lista0[i]) + "	" + str(lista1[i]) + "    " + str(lista2[i]) + "    " + str(lista3[i]))



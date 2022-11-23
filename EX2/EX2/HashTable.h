#pragma once
#include <iostream>
#include <string>
using namespace std;



template <typename T, typename K>
class HashTable
{
protected:

	enum state { empty, full, deleted };
	template <typename U, typename V>
	class Item
	{
	public:
		U data;
		V key;
		state flag;
		Item() {}
		Item(U d, V  k, state f)
		{
			data = d; key = k; flag = f;
		}
	};


	int size;
	Item<T, K>* arr;
	template <typename T, typename K>
	bool HashTable<T, K>::checkIfPrime(int n)
	{
		for (int i = 2; i <= 10; i++)
		{
			if (n % i == 0)
			{
				return false;
			}
		}
		return true;
	}
	template <typename T, typename K>
	int HashTable<T, K>::prime(int n)
	{
		if (n == 1 || n == 0)
		{
			return n;
		}
		while (!checkIfPrime(n))
		{
			n++;
		}
		return n;
	}
	template <typename T, typename K>
	int HashTable<T, K>::hash(K key, int i)
	{
		if (table != NULL)
		{
			int a = (h1(key) + i * h2(key)) % size;
			return a;
		}
		return -1;
	}
	template <typename T, typename K>
	virtual int HashTable<T, K>::h1(K key) = 0;
	template <typename T, typename K>
	virtual int HashTable<T, K>::h2(K key) = 0;
public:
	template <typename T, typename K>
	HashTable<T, K>::HashTable(int m = 10)
	{
		if (m >= 0)
		{
			size = prime(m);
			table = new Item<U, V>[size];
		}
		else
		{
			table = NULL;
			size = 0;
		}
	}
	template <typename T, typename K>
	HashTable<T, K>::~HashTable()
	{
		if (table != NULL)
		{
			delete table;
		}
	}
	template <typename T, typename K>
	void HashTable<T, K>::add(K& key, T& dat)
	{
		if (table != NULL)
		{
			int i = 0;
			while (i != size)
			{
				int index = hash(key, i);
				if (table[index].flag == empty || table[index].flag == deleted)
				{
					table[index].key = key;
					table[index].data = dat;
					table[index].flag = full;
					return;
				}
				i++;
			}
		}
		return;
	}
	template <typename T, typename K>
	int HashTable<T, K>::remove(K key)
	{
		if (table != NULL)
		{
			int index = search(key);
			if (index != -1)
			{
				table[index].flag == deleted;
			}
		}
	}
	template <typename T, typename K>
	int HashTable<T, K>::search(K key)
	{
		if (table != NULL)
		{
			int i = 0;
			while (i != size)
			{
				int index = hash(key, i);
				if (table[index].flag == full && table[index].key = key)
				{
					return index;
				}
				i++;
			}
		}
		return -1;
	}
	template <typename T, typename K>
	T* HashTable<T, K>::entryData(K i)
	{
		int ind = search(i);
		if (ind == -1)
			return NULL;
		else
			return &(arr[ind].data);
	}
	template <typename T, typename K>
	void HashTable<T, K>::print()
	{
		for (int i = 0; i < m; i++)
		{
			if (table[i].flag == full)
			{
				cout << "subject" << i << ": " << table[i].data << endl;
			}
			else
			{
				cout << "subject" << i << ": " << (table[i].flag == empty ? "empty" : "deleted") << endl;
			}
		}
			
	}
};

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PQueue<TElement, TPriority> where TPriority : IComparable{

    List<Tuple<TPriority, TElement>> minHeap;

    public PQueue(){
        this.minHeap = new List<Tuple<TPriority, TElement>>();
    }

    public bool IsEmpty(){
        return minHeap.Count == 0 ? true : false;
    }

    private void HeapifyDown(int i){
        int n = minHeap.Count;
        Tuple<TPriority, TElement> temp;
        
        while(i < n){
            int ss, l, r;
            l = 2*i + 1;
            ss = l;
            r = 2*i + 2;

            if(l < n && r < n && minHeap[l].Item1.CompareTo(minHeap[r].Item1) > 0){
                ss = r;
            }
            if(ss < n && minHeap[i].Item1.CompareTo(minHeap[ss].Item1) > 0){
                temp = minHeap[i];
                minHeap[i] = minHeap[ss];
                minHeap[ss] = temp;
                i = ss;
            }
            else{
                break;
            }
        }
    }

    private void HeapifyUp(){
        int i, p;
        Tuple<TPriority, TElement> child, parent, temp;
        i = minHeap.Count - 1;
        while(i>0){
            p = (i-1)/2;
            child = minHeap[i];
            parent = minHeap[p];
            if(parent.Item1.CompareTo(child.Item1) > 0){
                temp = minHeap[i];
                minHeap[i] = minHeap[p];
                minHeap[p] = temp;
                i = p;
            }
            else{
                break;
            }
        }
    }

    public Tuple<TPriority, TElement> DeQueue(){
        Tuple<TPriority, TElement> min;
        Tuple<TPriority, TElement> last;
        if(minHeap.Count > 0){
            min = minHeap[0];
            last = minHeap[minHeap.Count - 1];
            minHeap.RemoveAt(minHeap.Count - 1);
            if(minHeap.Count > 0){
                minHeap[0] = last;
                HeapifyDown(0);
            }
            return min;
        }
        return null;
    }

    public void EnQueue(Tuple<TPriority, TElement> item) {
        minHeap.Add(item);
        HeapifyUp();
    }
}
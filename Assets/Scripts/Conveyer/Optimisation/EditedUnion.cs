using UnityEngine;
using System.Collections.Generic;

public class EditedUnion{
    int width;
    int height;
    Cell[,] cells;
    HashSet<Cell> representatives;
    
    public EditedUnion(int width, int height){
        this.width = width;
        this.height = height;
        cells = new Cell[width, height];
        for(int i = 0; i < width; i++){
            for(int j = 0; j < height; j++){
                cells[i, j] = null;
            }
        }
    }
    
    //methods for gettings data
    public int getWidth(){
        return width;
    }
    public int getHeight(){
        return height;
    }
    public Cell getCell(Vector2Int pos){
        return cells[pos.x, pos.y];
    }
    
    /**
     * Get the representative of a cell
     * @param pos : position of the cell
     * @return the representative of the cell
     */
    public Cell getRepresentative(Vector2Int pos,List<Cell> visited = null){
        if(visited == null){
            visited = new List<Cell>();
        }
        Cell cell = cells[pos.x, pos.y];
        if(cell == null){
            return null;
        }
        while(cell.next != cell){
            if(visited.Contains(cell)){
                return cell;
            }
            visited.Add(cell);
            return getRepresentative(cell.next.position, visited);
        }
        return cell;
    }
    
    public List<Cell> getRepresentatives(){
        return new List<Cell>(representatives);
    }
    
    /**
     * Add a cell to the union
     * @param pos : position of the cell
     * @param rotation : rotation of the cell (top = 0, right = 1, bottom = 2, left = 3)
     */
    public void addCell(Vector2Int pos, int rotation){
        Cell cell = new Cell();
        cells[pos.x, pos.y] = cell; //TODO : test si ca marche
        cell.position = pos;
        cell.next = getNextValid(pos, rotation);
        cell.rotation = rotation;
        cell.previous = getPrevCells(pos, rotation);
        
        calculateRepresentative();
    }

    /**
     * Remove a cell from the union
     * @param pos : position of the cell
     */
    public void removeCell(Vector2Int pos){
        if(cells[pos.x, pos.y] == null){
            return;
        }
        Cell cell = cells[pos.x, pos.y];
        
        if(cell.previous != null){
            foreach(Cell c in cell.previous){
                c.next = c.next;
                representatives.Add(c);
            }
        }
        
        if(cell.next != null){
            cell.next.previous.Remove(cell);
        }
        
        cells[pos.x, pos.y] = null;
        
        calculateRepresentative();
    }
    
    /**
     * Replace a cell in the union
     * @param pos : position of the cell
     * @param rotation : rotation of the cell (top = 0, right = 1, bottom = 2, left = 3)
     */
    public void replace(Vector2Int pos, int rotation){
        removeCell(pos);
        addCell(pos, rotation);
    }
    
    private HashSet<Cell> getPrevCells(Vector2Int pos,int rotation){
        HashSet<Cell> prev = new HashSet<Cell>();
        if(pos.y +1 < height && cells[pos.x, pos.y + 1] != null && cells[pos.x, pos.y + 1].rotation == 2){
            prev.Add(cells[pos.x, pos.y + 1]);
            cells[pos.x, pos.y + 1].next = cells[pos.x, pos.y];
        }
        if(rotation == 1 && pos.x +1 < width && cells[pos.x + 1, pos.y] != null){
            prev.Add(cells[pos.x + 1, pos.y]);
            cells[pos.x + 1, pos.y].next = cells[pos.x, pos.y];
        }
        if(rotation == 2 && pos.y -1 >= 0 && cells[pos.x, pos.y - 1] != null){
            prev.Add(cells[pos.x, pos.y - 1]);
            cells[pos.x, pos.y - 1].next = cells[pos.x, pos.y];
        }
        if(rotation == 3 && pos.x -1 >= 0 && cells[pos.x - 1, pos.y] != null){
            prev.Add(cells[pos.x - 1, pos.y]);
            cells[pos.x - 1, pos.y].next = cells[pos.x, pos.y];
        }
        return prev;
    }
    private Cell getNextValid(Vector2Int pos,int rotation){
        Vector2Int next = pos;
        switch(rotation){
            case 0:
                next.y++;
                break;
            case 1:
                next.x++;
                break;
            case 2:
                next.y--;
                break;
            case 3:
                next.x--;
                break;
        }
        
        if(next.x < 0 || next.x >= width || next.y < 0 || next.y >= height){
            return cells[pos.x, pos.y];
        }
        if(cells[next.x, next.y] == null){
            return cells[pos.x, pos.y];
        }
        return cells[next.x, next.y];
    }

    private void calculateRepresentative(){
        representatives = new HashSet<Cell>();
        for(int i = 0; i < width; i++){
            for(int j = 0; j < height; j++){
                Cell representative = getRepresentative(new Vector2Int(i, j));
                if(representative != null){
                    representatives.Add(representative);
                }
            }
        }
    }
}

public class Cell{
    public Vector2Int position;
    public int rotation;
    public Cell next;
    public HashSet<Cell> previous;
}

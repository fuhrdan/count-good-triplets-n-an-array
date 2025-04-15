//*****************************************************************************
//** 2179. Count Good Triplets in an Array                          leetcode **
//*****************************************************************************

typedef struct
{
    int n;
    int* c;
} BinaryIndexedTree;

BinaryIndexedTree* createBIT(int n)
{
    BinaryIndexedTree* tree = (BinaryIndexedTree*)malloc(sizeof(BinaryIndexedTree));
    tree->n = n;
    tree->c = (int*)calloc(n + 1, sizeof(int));
    return tree;
}

void freeBIT(BinaryIndexedTree* tree)
{
    if (tree)
    {
        free(tree->c);
        free(tree);
    }
}

int lowbit(int x)
{
    return x & -x;
}

void updateBIT(BinaryIndexedTree* tree, int x, int delta)
{
    while (x <= tree->n)
    {
        tree->c[x] += delta;
        x += lowbit(x);
    }
}

int queryBIT(BinaryIndexedTree* tree, int x)
{
    int s = 0;
    while (x > 0)
    {
        s += tree->c[x];
        x -= lowbit(x);
    }
    return s;
}

long long goodTriplets(int* nums1, int nums1Size, int* nums2, int nums2Size)
{
    int* pos = (int*)malloc((nums2Size + 1) * sizeof(int));
    for (int i = 0; i < nums2Size; ++i)
    {
        pos[nums2[i]] = i + 1;
    }

    BinaryIndexedTree* tree = createBIT(nums2Size);
    long long ans = 0;

    for (int i = 0; i < nums1Size; ++i)
    {
        int num = nums1[i];
        int p = pos[num];
        int left = queryBIT(tree, p);
        int right = nums2Size - p - (queryBIT(tree, nums2Size) - queryBIT(tree, p));
        ans += (long long)left * right;
        updateBIT(tree, p, 1);
    }

    free(pos);
    freeBIT(tree);
    return ans;
}
using System.Diagnostics;
using System.Text;

namespace _2021
{
  internal class Day14
  {
	/*
	private static string PolymerTemplate = "NNCB";
	private static Dictionary<string, string> TokenMap = new()
	{
		{ "CH", "CBH" },
		{ "HH", "HNH" },
		{ "CB", "CHB" },
		{ "NH", "NCH" },
		{ "HB", "HCB" },
		{ "HC", "HBC" },
		{ "HN", "HCN" },
		{ "NN", "NCN" },
		{ "BH", "BHH" },
		{ "NC", "NBC" },
		{ "NB", "NBB" },
		{ "BN", "BBN" },
		{ "BB", "BNB" },
		{ "BC", "BBC" },
		{ "CC", "CNC" },
		{ "CN", "CCN" }
	};
	*/
	
	private static string PolymerTemplate = "SVKVKCCBNHNSOSCCOPOC";
	private static Dictionary<string, string> TokenMap = new()
	{
	  { "KK", "KBK" },
	  { "CS", "CPS" },
	  { "VV", "VOV" },
	  { "KO", "KSO" },
	  { "PO", "PNO" },
	  { "PH", "PKH" },
	  { "BV", "BOV" },
	  { "VH", "VVH" },
	  { "PF", "PPF" },
	  { "HB", "HBB" },
	  { "OB", "OVB" },
	  { "FC", "FFC" },
	  { "OS", "OHS" },
	  { "NB", "NPB" },
	  { "SH", "SSH" },
	  { "KV", "KKV" },
	  { "SO", "SCO" },
	  { "NP", "NBP" },
	  { "NV", "NFV" },
	  { "CP", "COP" },
	  { "KS", "KNS" },
	  { "FP", "FBP" },
	  { "VN", "VVN" },
	  { "NC", "NSC" },
	  { "FH", "FNH" },
	  { "CB", "CVB" },
	  { "PV", "PBV" },
	  { "NH", "NBH" },
	  { "NF", "NHF" },
	  { "PC", "PBC" },
	  { "NO", "NNO" },
	  { "CN", "CPN" },
	  { "KF", "KBF" },
	  { "VF", "VSF" },
	  { "CC", "CKC" },
	  { "CF", "CNF" },
	  { "PS", "PSS" },
	  { "NK", "NNK" },
	  { "PB", "PHB" },
	  { "BP", "BOP" },
	  { "FK", "FNK" },
	  { "BO", "BSO" },
	  { "OH", "OCH" },
	  { "VB", "VSB" },
	  { "VP", "VFP" },
	  { "FO", "FVO" },
	  { "KB", "KCB" },
	  { "SK", "SHK" },
	  { "CO", "CHO" },
	  { "HV", "HHV" },
	  { "SV", "SBV" },
	  { "BF", "BOF" },
	  { "SS", "SKS" },
	  { "VK", "VSK" },
	  { "HS", "HBS" },
	  { "HF", "HPF" },
	  { "PK", "PFK" },
	  { "BS", "BOS" },
	  { "BB", "BOB" },
	  { "VC", "VPC" },
	  { "OP", "OFP" },
	  { "NS", "NPS" },
	  { "SB", "SCB" },
	  { "NN", "NKN" },
	  { "HC", "HSC" },
	  { "HH", "HBH" },
	  { "FN", "FPN" },
	  { "OO", "OVO" },
	  { "VO", "VNO" },
	  { "ON", "OPN" },
	  { "FV", "FKV" },
	  { "HK", "HSK" },
	  { "FS", "FVS" },
	  { "HO", "HVO" },
	  { "PN", "PBN" },
	  { "KH", "KBH" },
	  { "CH", "CCH" },
	  { "KP", "KSP" },
	  { "BH", "BOH" },
	  { "BK", "BBK" },
	  { "FB", "FHB" },
	  { "VS", "VSS" },
	  { "HP", "HOP" },
	  { "SP", "SPP" },
	  { "OV", "OFV" },
	  { "OF", "OHF" },
	  { "OC", "OVC" },
	  { "KN", "KHN" },
	  { "BC", "BFC" },
	  { "BN", "BFN" },
	  { "CK", "CKK" },
	  { "SN", "SPN" },
	  { "SF", "SKF" },
	  { "KC", "KCC" },
	  { "SC", "SCC" },
	  { "HN", "HVN" },
	  { "OK", "OOK" },
	  { "FF", "FVF" },
	  { "CV", "CVV" },
	  { "PP", "PVP" },
	};
	
	private IEnumerable<string> SplitToTokens(string polymer)
	{
	  for (int i = 0; i < polymer.Length - 1; i++)
		yield return polymer.Substring(i, 2);
	}


	public class Node
	{
	  public Node(string name, bool isRootNode = false)
	  {
		Name = name;
		IsRootNode = isRootNode;
	  }
	  public string Name { get; set; } = "";
	  public bool IsRootNode { get; set; }

	  public Dictionary<char, long> LeafData = new();

	  public override string ToString()
	  {
		return Name;
	  }

	  public void PopulateLeafDataFrom(Node child)
	  {
		var left = child.Name[0];
		var right = child.Name[1];

		if (!LeafData.ContainsKey(left))
		  LeafData.Add(left, 0);

		if (!LeafData.ContainsKey(right))
		  LeafData.Add(right, 0);

		LeafData[right] += 1;
	  }

	  public void PropagateLeafDataFrom(Node child)
	  {
		foreach (var item in child.LeafData)
		{
		  if (!LeafData.ContainsKey(item.Key))
			LeafData.Add(item.Key, 0);
		  if (!LeafData.ContainsKey(item.Key))
			LeafData.Add(item.Key, 0);


		  LeafData[item.Key] += item.Value;
		}
	  }
	}

	public void Run()
	{
	  var sw = new Stopwatch();
	  sw.Start();
	  var root = new Node(PolymerTemplate, true);

	  Expand(root, 41); // Equal to 40

	  var ordered = root.LeafData.OrderBy(x => x.Value);
	  foreach (var item in ordered)
	  {
		Console.WriteLine($"{item.Key}: {item.Value}");
	  }
	  sw.Stop();

	  Console.WriteLine();
	  Console.WriteLine($"Result is: {ordered.Last().Value - ordered.First().Value}");
	  Console.WriteLine();
	  Console.WriteLine($"Elapsed: {sw.ElapsedMilliseconds}ms");
	}

	private Dictionary<string, Dictionary<int, Node>> _nodeMap = new();

	private void Expand(Node parent, int depth)
	{
	  if (depth == 0)
		return;

	  string[] splitMappedToken;
	  var mappedToken = parent.Name;

	  if (TokenMap.ContainsKey(parent.Name))
		mappedToken = TokenMap[parent.Name];

	  splitMappedToken = SplitToTokens(mappedToken).ToArray();

	  for (int i = 0; i < splitMappedToken.Count(); i++)
	  {
		var token = splitMappedToken[i];
		var rootLeftmostPath = i == 0 && parent.IsRootNode;

		Dictionary<int, Node> nodeMap;
		if (!_nodeMap.TryGetValue(token, out nodeMap))
		{
		  nodeMap = new();
		  _nodeMap[token] = nodeMap;
		}

		var nextDepth = depth - 1;
		var nodeFound = nodeMap.TryGetValue(depth, out var node);

		if (nodeFound && node.LeafData.Any())
		{
		  parent.PropagateLeafDataFrom(node);
		  continue;
		}

		node = new(token);

		if (nextDepth != 0)
		  nodeMap[depth] = node;

		Expand(node, nextDepth);

		// First leaf data should only be included when propagated up the leftmost path of the tree
		if (rootLeftmostPath)
		{
		  if (node.LeafData.ContainsKey(parent.Name[0]))
		  {
			node.LeafData[parent.Name[0]] += 1;
		  }
		}

		if (nextDepth == 0)
		  parent.PopulateLeafDataFrom(node);
		else
		  parent.PropagateLeafDataFrom(node);

	  }
	}
  }
}

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
                _name = name;
                _isRootNode = isRootNode;
            }
            public string _name { get; set; } = "";
            public bool _isRootNode { get; set;}

            public Dictionary<char, long> _leafData = new();
            public bool _isLeftmostLeafParent { get; set; } = false;

            public override string ToString()
            {
                return _name;
            }

            internal void PopulateLeafDataFrom(Node child)
            {
                var left = child._name[0];
                var right = child._name[1];

                if (!_leafData.ContainsKey(left))
                    _leafData.Add(left, 0);

                if (!_leafData.ContainsKey(right))
                    _leafData.Add(right, 0);

                _leafData[right] += 1;
            }

            internal void PropagateLeafDataFrom(Node child, int depth, bool leftmostPath)
            {
                foreach (var item in child._leafData)
                {
                    if (!_leafData.ContainsKey(item.Key))
                        _leafData.Add(item.Key, 0);
                    if (!_leafData.ContainsKey(item.Key))
                        _leafData.Add(item.Key, 0);

                    _leafData[item.Key] += item.Value;
                }
            }
        }

        public void Run()
        {
            var sw = new Stopwatch();
            sw.Start();
            var root = new Node(PolymerTemplate, true);

            Expand(root, 41); // Equal to 40
            root._leafData[PolymerTemplate[0]] += 1; // Add the leftmost leaf of the tree
            
            var ordered = root._leafData.OrderBy(x => x.Value);
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

        private void Expand(Node parent, int depth, bool propagatedLeftmostPath = true)
        {
            if (depth == 0)
                return;

            string[] splitMappedToken;
            var mappedToken = parent._name;

            if (TokenMap.ContainsKey(parent._name))
                mappedToken = TokenMap[parent._name];

            splitMappedToken = SplitToTokens(mappedToken).ToArray();

            for (int i = 0; i < splitMappedToken.Count(); i++)
            {
                var token = splitMappedToken[i];
                var leftMostPath = i == 0 && propagatedLeftmostPath;

                Dictionary<int, Node> nodeMap;
                if(!_nodeMap.TryGetValue(token, out nodeMap))
                {
                    nodeMap = new();
                    _nodeMap[token] = nodeMap;
                }

                var nextDepth = depth - 1;
                var nodeFound = nodeMap.TryGetValue(depth, out var node);
                
                if (nodeFound && node._leafData.Any())
                {
                    parent.PropagateLeafDataFrom(node, depth, leftMostPath);
                    continue;
                }

                node = new(token);

                if (nextDepth != 0)
                    nodeMap[depth] = node;
                
                Expand(node, nextDepth, leftMostPath);

                if (node._isLeftmostLeafParent) // May have been set in Expand() -> PopulateLeafDataFrom()
                    nodeMap.Remove(depth);

                if (nextDepth == 0)
                    parent.PopulateLeafDataFrom(node);
                else
                    parent.PropagateLeafDataFrom(node, depth, leftMostPath);
            }
        }
    }
}

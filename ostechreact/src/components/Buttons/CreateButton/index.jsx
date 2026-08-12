import { PlusIcon } from "@phosphor-icons/react";

export const CreateButton = ({ entity, onCreate }) => {
    return (
        <button
            type="button"
            className="flex items-center gap-2 px-4 py-2 rounded-md bg-[#03346E] text-[#E2E2B6] font-medium hover:bg-[#6EACDA] hover:text-[#021526] transition-colors"
            onClick={onCreate}
        >
            <PlusIcon size={22} />
            Create {entity}
        </button>
    );
}
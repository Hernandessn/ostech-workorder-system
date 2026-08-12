import { PencilSimpleIcon, TrashIcon } from "@phosphor-icons/react";

export const ActionsButtons = ({ onEdit, onDelete }) => {
    return (
        <div className="flex gap-2">
            <button
                type="button"
                className="p-2 rounded-md bg-[#03346E] text-[#E2E2B6] hover:bg-[#6EACDA] hover:text-[#021526] transition-colors"
                onClick={onEdit}
            >
                <PencilSimpleIcon size={22} />
            </button>
            <button
                type="button"
                className="p-2 rounded-md bg-[#03346E] text-[#E2E2B6] hover:bg-red-600 hover:text-white transition-colors"
                onClick={onDelete}
            >
                <TrashIcon size={22} />
            </button>
        </div>
    );
}